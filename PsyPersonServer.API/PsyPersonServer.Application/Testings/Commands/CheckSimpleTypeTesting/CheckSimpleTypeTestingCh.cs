using AutoMapper;
using MediatR;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Testings.Commands.CheckSimpleTypeTesting
{
    public class CheckSimpleTypeTestingCh : IRequestHandler<CheckSimpleTypeTestingC, double>
    {
        public CheckSimpleTypeTestingCh(ITestRepository testRepository, ITestQuestionRepository testQuestionRepository, IMapper mapper)
        {
            _testRepository = testRepository;
            _testQuestionRepository = testQuestionRepository;
            _mapper = mapper;
        }

        private readonly ITestRepository _testRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly IMapper _mapper;

        public async Task<double> Handle(CheckSimpleTypeTestingC request, CancellationToken cancellationToken)
        {
            var t = await _testQuestionRepository.GetAllWithOnlyTruAnswersByTestId(request.TestForTesting.Test.Id);
            var testquestionsBase = t.Select(x => _mapper.Map<TestQuestionDto>(x));

            //Check Simple Type Testing

            double ball = 0;
            double b1 = 100.0 / request.TestForTesting.TestQuestionList.Count();

            foreach (var i in testquestionsBase)
            {
                foreach (var j in request.TestForTesting.TestQuestionList)
                {
                    if (i.Name == j.Name)
                    {
                        double lenght = i.Answers.Count();
                        double c = 1 / lenght;
                        double b = 0.0;

                        foreach (var ii in i.Answers)
                        {
                            foreach (var jj in j.Answers)
                            {
                                if (ii.Name == jj.Name)
                                {
                                    if (ii.IsCorrect == true && jj.IsCorrect == true)
                                    {
                                        b += 1;
                                    }
                                }
                                else if (ii.Name != jj.Name)
                                {
                                    var trueVar = i.Answers.Any(j => jj.Name == j.Name);
                                    if (trueVar == false && jj.IsCorrect == true)
                                    {
                                        b = b - c;
                                    }
                                }
                            }
                        }

                        double count = 0.0;
                        if (b > 0)
                            count = b / lenght;

                        ball += count;
                    }
                }
            }
            ball = ball * b1;

            return ball;
        }
    }
}
