using AutoMapper;
using MediatR;
using PsyPersonServer.Application.Tests.Dtos;
using PsyPersonServer.Application.Tests.Queries.GetTestsByUserId;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Tests.Queries.GetTestsByUserIdForLookupTable
{
    public class GetTestsByUserIdForLookupTableQh : IRequestHandler<GetTestsByUserIdForLookupTableQ, PagedResponse<TestDto>>
    {
        public GetTestsByUserIdForLookupTableQh(IMapper mapper, ITestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private readonly IMapper _mapper;
        private readonly ITestRepository _repository;

        public async Task<PagedResponse<TestDto>> Handle(GetTestsByUserIdForLookupTableQ request, CancellationToken cancellationToken)
        {
            var tests = await _repository.GetTestsByUserId(request.Page, request.ItemPerPage, request.UserId,request.Name);
            return new PagedResponse<TestDto>(tests.Data.Select(x => _mapper.Map<TestDto>(x)),tests.Total);
        }
    }
}
