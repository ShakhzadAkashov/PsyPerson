using AutoMapper;
using MediatR;
using PsyPersonServer.Application.UserTests.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PsyPersonServer.Domain.Entities;
using System.Linq;
using PsyPersonServer.Application.UserTests.Commands.CountStatus;

namespace PsyPersonServer.Application.UserTests.Queries.GetUserTestsDetails
{
    public class GetUserTestsDetailsQh : IRequestHandler<GetUserTestsDetailsQ,PagedResponse<UserTestDetailDto>>
    { 
        public GetUserTestsDetailsQh(IUserTestRepository userTestRepository, IMapper mapper, IMediator mediator)
        {
            _userTestRepository = userTestRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        private readonly IUserTestRepository _userTestRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public async Task<PagedResponse<UserTestDetailDto>> Handle(GetUserTestsDetailsQ request, CancellationToken cancellationToken)
        {
            var userTests = await _userTestRepository.GetUserTests(request.Page, request.ItemPerPage, request.UserId, request.TestName);

            var userTestDtos = userTests.Data.Select(x => _mapper.Map<UserTestDetailDto>(x)).ToList();

            foreach (var i in userTestDtos)
            {
                var lastTesting = i.UserTestingHistoryList.OrderByDescending(x => x.TestedDate).FirstOrDefault();
                var status = await _mediator.Send(new CountStatusC
                {
                    TestScoreList = new List<decimal> { Convert.ToDecimal(lastTesting == null ? 0 : lastTesting.TestScore) }
                });
                i.Status = status.ToString();
            }

            return new PagedResponse<UserTestDetailDto>(userTestDtos, userTests.Total);
        }
    }
}
