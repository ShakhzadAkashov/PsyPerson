using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Application.Users.Dtos;
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
        public GetAllUsersQh(UserManager<ApplicationUser> userManager, IMapper mapper, IUserTestRepository userTestRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userTestRepository = userTestRepository;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserTestRepository _userTestRepository;

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

                i.Status = "Not Found";
                i.AmountAllUserTests = userTests.Count();
                i.AmountTestedUserTests = userTests.Where(x => x.IsTested == true).Count();
                i.AmountPendingUserTests = userTests.Where(x => x.IsTested == false).Count();
            }

            return new PagedResponse<UserTestUserDto>(users,total);
        }
    }
}
