using AutoMapper;
using MediatR;
using PsyPersonServer.Application.UserTests.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.UserTests.Queries.GetUserTests
{
    public class GetUserTestsQh : IRequestHandler<GetUserTestsQ, PagedResponse<UserTestDto>>
    {
        public GetUserTestsQh(IUserTestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly IUserTestRepository _repository;
        private readonly IMapper _mapper;

        public async Task<PagedResponse<UserTestDto>> Handle(GetUserTestsQ request, CancellationToken cancellationToken)
        {
            var userTests = await _repository.GetUserTests(request.Page, request.ItemPerPage, request.UserId);
            return new PagedResponse<UserTestDto>(userTests.Data.Select(x => _mapper.Map<UserTestDto>(x)), userTests.Total);
        }
    }
}
