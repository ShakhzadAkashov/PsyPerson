using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Application.EmailMessage.Commands.SendEmailMessage;
using PsyPersonServer.Application.Testings.Commands.CreateTestingHistory;
using PsyPersonServer.Application.Testings.Dtos;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.Tests;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Testings.Commands.CheckSimpleTypeTesting
{
    public class CheckSimpleTypeTestingCh : IRequestHandler<CheckSimpleTypeTestingC, CheckTestingResponseDto>
    {
        public CheckSimpleTypeTestingCh(ITestRepository testRepository, ITestQuestionRepository testQuestionRepository, IUserTestRepository userTestRepository, IMapper mapper, IMediator mediator, ILogger<CheckSimpleTypeTestingCh> logger, UserManager<ApplicationUser> userManager)
        {
            _testRepository = testRepository;
            _testQuestionRepository = testQuestionRepository;
            _userTestRepository = userTestRepository;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _userManager = userManager;
        }

        private readonly ITestRepository _testRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly IUserTestRepository _userTestRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<CheckSimpleTypeTestingCh> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public async Task<CheckTestingResponseDto> Handle(CheckSimpleTypeTestingC request, CancellationToken cancellationToken)
        {
            var t = await _testQuestionRepository.GetAllWithOnlyTruAnswersByTestId(request.TestForTesting.Test.Id);
            var testquestionsBase = t.Select(x => _mapper.Map<TestQuestionDto>(x));
            var test = await _testRepository.GetTestById(request.TestForTesting.Test.Id);
            var userTest = await _userTestRepository.GetUserTest(request.UserId, request.TestForTesting.Test.Id);
            var user = await _userManager.FindByIdAsync(request.UserId);

            try
            {
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

                //Check Simple Type Testing

                var result = new CheckTestingResponseDto();
                double score = 0.0;
                TestResultStatusEnum status = TestResultStatusEnum.Unknown;
                string desc = "";

                foreach (var i in test.TestResultList)
                {
                    if (i.RangeFrom >= ball && i.RangeTo <= ball)
                    {
                        score = ball;
                        status = i.Status;
                        desc = i.Name;
                    }
                }

                if (score != ball || string.IsNullOrEmpty(desc) || status == TestResultStatusEnum.Unknown)
                {
                    result.Description = "Unknown desc!";
                    result.Status = TestResultStatusEnum.Unknown;
                    result.TestScore = ball;
                }
                else
                {
                    result.Description = desc;
                    result.TestScore = score;
                    result.Status = status;
                }

                await _userTestRepository.Update(userTest.Id, userTest.IsActive, true, userTest.AssignedDate);

                await _mediator.Send(new CreateTestingHistoryC
                {
                    TestScore = ball,
                    ResultStatus = status,
                    UserTest = userTest,
                    TestQuestionList = request.TestForTesting.TestQuestionList,
                    IsChecked = true
                });

                //Send Message to email

                string message = $"Тест был перепройден на {ball.ToString("0.0")}%";
                string firstName = user.FirstName ?? "";
                string lastName = user.LastName ?? "";
                string patronymic = user.Patronymic ?? "";
                string fullName = firstName + " " + lastName + " " + patronymic;
                string letterHeader = $"Перепрохождение теста: {test.Name}";

                await _mediator.Send(new SendEmailMessageC
                {
                    ReceiverMailAddress = user.Email,
                    EmailMessage = message,
                    ReceiverFullName = fullName,
                    LetterHeader = letterHeader,
                    IsHTML = false
                });

                //Send Message to email

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Check Simple Type Testing for test: {request.TestForTesting.Test.Id} and for User: {request.UserId} failed {ex}", ex);
                throw ex;
            }
        }
    }
}
