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

namespace PsyPersonServer.Application.UserTests.Queries.GetUserTestsDetails
{
    public class GetUserTestsDetailsQh : IRequestHandler<GetUserTestsDetailsQ,PagedResponse<UserTestDetailDto>>
    { 
        public GetUserTestsDetailsQh(IUserTestRepository userTestRepository, IMapper mapper)
        {
            _userTestRepository = userTestRepository;
            _mapper = mapper;
        }

        private readonly IUserTestRepository _userTestRepository;
        private readonly IMapper _mapper;

        public async Task<PagedResponse<UserTestDetailDto>> Handle(GetUserTestsDetailsQ request, CancellationToken cancellationToken)
        {
            var userTests = await _userTestRepository.GetUserTests(request.Page, request.ItemPerPage, request.UserId, request.TestName);

            var userTestDtos = userTests.Data.Select(x => _mapper.Map<UserTestDetailDto>(x)).ToList();

            foreach (var i in userTestDtos)
            { 
                i.Status = "Not Found";
            }

            return new PagedResponse<UserTestDetailDto>(userTestDtos, userTests.Total);
        }
    }
}
