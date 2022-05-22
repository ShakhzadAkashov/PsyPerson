using AutoMapper;
using MediatR;
using PsyPersonServer.Application.Tests.Dtos;
using PsyPersonServer.Domain.DapperRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Tests.Queries.GetDapperTess
{
    public class GetDapperTestsQh : IRequestHandler<GetDapperTestsQ, IEnumerable<TestDto>>
    {
        public GetDapperTestsQh(IDapperTestRepository dapperTestRepository, IMapper mapper)
        {
            _dapperTestRepository = dapperTestRepository;
            _mapper = mapper;
        }

        private readonly IDapperTestRepository _dapperTestRepository;
        private readonly IMapper _mapper;

        public async Task<IEnumerable<TestDto>> Handle(GetDapperTestsQ request, CancellationToken cancellationToken)
        {
            var tests = await _dapperTestRepository.GetTests();
            var result = tests.Select(x => _mapper.Map<TestDto>(x)).ToList();
            return result;
        }
    }
}
