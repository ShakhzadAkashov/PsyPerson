using AutoMapper;
using MediatR;
using PsyPersonServer.Application.Testings.Dtos;
using PsyPersonServer.Application.Testings.Queries.GetTestForTesting;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Application.Tests.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Testings.Queries.GetTestQuestionsForTesting
{
    public class GetTestForTestingQh : IRequestHandler<GetTestForTestingQ, TestForTestingDto>
    {
        public GetTestForTestingQh(IMapper mapper, ITestQuestionRepository testQuestionRepository, ITestRepository testRepository)
        {
            _mapper = mapper;
            _testQuestionRepository = testQuestionRepository;
            _testRepository = testRepository;
        }

        private readonly IMapper _mapper;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly ITestRepository _testRepository;
        public async Task<TestForTestingDto> Handle(GetTestForTestingQ request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.GetTestById(request.TestId);
            var testQuestions = await _testQuestionRepository.GetAllForTestingById(request.TestId);

            return new TestForTestingDto 
            {
                Test = _mapper.Map<TestDto>(test),
                TestQuestionList = testQuestions.Select(x => _mapper.Map<TestQuestionDto>(x))
            };
        }
    }
}
