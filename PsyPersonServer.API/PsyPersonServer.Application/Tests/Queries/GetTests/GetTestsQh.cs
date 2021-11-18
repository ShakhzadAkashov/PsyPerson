using AutoMapper;
using MediatR;
using PsyPersonServer.Application.Tests.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Tests.Queries.GetTests
{
    public class GetTestsQh : IRequestHandler<GetTestsQ, PagedResponse<TestDto>>
    {
        public GetTestsQh(ITestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly ITestRepository _repository;
        private readonly IMapper _mapper;

        public async Task<PagedResponse<TestDto>> Handle(GetTestsQ request, CancellationToken cancellationToken)
        {
            var tests = await _repository.GetTests(request.Page, request.ItemPerPage);

            return new PagedResponse<TestDto>(tests.Data.Select(x => _mapper.Map<TestDto>(x)),tests.Total);
        }
    }
}
