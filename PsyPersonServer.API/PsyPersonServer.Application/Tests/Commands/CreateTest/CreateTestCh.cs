using AutoMapper;
using MediatR;
using PsyPersonServer.Application.Tests.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Tests.Commands.CreateTest
{
    public class CreateTestCh : IRequestHandler<CreateTestC, TestDto>
    {
        public CreateTestCh(ITestRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly ITestRepository _repository;
        private readonly IMapper _mapper;

        public async Task<TestDto> Handle(CreateTestC request, CancellationToken cancellationToken)
        {
            var testResultList = request.TestResultList.Select(x => _mapper.Map<TestResult>(x)).ToList();
            var test = await _repository.Create(request.Name, request.Description,request.ImgPath, request.TestType, testResultList);

            return _mapper.Map<TestDto>(test);
        }
    }
}
