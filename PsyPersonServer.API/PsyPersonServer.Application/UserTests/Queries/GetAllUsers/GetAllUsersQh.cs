using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Application.Users.Dtos;
using PsyPersonServer.Application.UserTests.Commands.CountStatus;
using PsyPersonServer.Application.UserTests.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Repositories;
using PsyPersonServer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.UserTests.Queries.GetAllUsers
{
    public class GetAllUsersQh : IRequestHandler<GetAllUsersQ, PagedResponse<UserTestUserDto>>
    {
        public GetAllUsersQh(UserManager<ApplicationUser> userManager, IMapper mapper, IUserTestRepository userTestRepository, IMediator mediator, IUserTestingHistoryRepository historyRepo)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userTestRepository = userTestRepository;
            _mediator = mediator;
            _historyRepo = historyRepo;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserTestRepository _userTestRepository;
        private readonly IMediator _mediator;
        private readonly IUserTestingHistoryRepository _historyRepo;

        public async Task<PagedResponse<UserTestUserDto>> Handle(GetAllUsersQ request, CancellationToken cancellationToken)
        {
            var u = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(request.UserName))
            {
                u = u.Where(x => x.UserName.Contains(request.UserName));
            }

            var total = await u.CountAsync(cancellationToken);

            if (request.Page > 0 && request.ItemPerPage > 0)
            {
                u = u.OrderByDescending(x => x.DateBirthday)
                    .Skip((request.Page - 1) * request.ItemPerPage)
                    .Take(request.ItemPerPage);
            }

            var users = await u.Select(s => _mapper.Map<UserTestUserDto>(s)).ToListAsync(); ;

            foreach (var i in users)
            {
                var userTests = _userTestRepository.GetUserTestsByUserId(i.Id).Result;

                i.AmountAllUserTests = userTests.Count();
                i.AmountTestedUserTests = userTests.Where(x => x.IsTested == true).Count();
                i.AmountPendingUserTests = userTests.Where(x => x.IsTested == false).Count();

                var testScoreList = new List<decimal>();

                foreach (var j in userTests)
                {
                    var lastTestingHist = await _historyRepo.GetTestingHistoryByUserTestId(j.Id);
                    var lastTestingObj = lastTestingHist.OrderByDescending(x => x.TestedDate).FirstOrDefault();

                    var lastTestingScore = lastTestingObj == null ? 0 : lastTestingObj.TestScore;
                    testScoreList.Add(Convert.ToDecimal(lastTestingScore));
                }

                decimal score = default;

                for(int k =0; k< testScoreList.Count; k++)
                {
                    score += testScoreList[k];
                }

                var status = await _mediator.Send(new CountStatusC
                {
                    TestScoreList = new List<decimal> { score /(testScoreList.Count == 0 ? 1 : testScoreList.Count) }
                });
                i.Status = status.ToString();
            }

            return new PagedResponse<UserTestUserDto>(users,total);
        }
    }
}
