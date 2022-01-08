using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Application.EmailMessage.Commands.SendEmailMessage;
using PsyPersonServer.Application.Testings.Commands.CreateTestingHistory;
using PsyPersonServer.Application.Testings.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.Tests;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Testings.Commands.CheckSecondLevelDifficultTypeTesting
{
    public class CheckSecondLevelDifficultTypeTestingCh : IRequestHandler<CheckSecondLevelDifficultTypeTestingC, CheckTestingResponseDto>
    {
        public CheckSecondLevelDifficultTypeTestingCh(ILogger<CheckSecondLevelDifficultTypeTestingCh> logger, IMediator mediator, ITestRepository testRepository, IUserTestRepository userTestRepository, IUserTestingHistoryRepository userTestingHistoryRepository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _mediator = mediator;
            _testRepository = testRepository;
            _userTestRepository = userTestRepository;
            _userTestingHistoryRepository = userTestingHistoryRepository;
            _userManager = userManager;
        }

        private readonly ILogger<CheckSecondLevelDifficultTypeTestingCh> _logger;
        private readonly IMediator _mediator;
        private readonly ITestRepository _testRepository;
        private readonly IUserTestRepository _userTestRepository;
        private readonly IUserTestingHistoryRepository _userTestingHistoryRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public async Task<CheckTestingResponseDto> Handle(CheckSecondLevelDifficultTypeTestingC request, CancellationToken cancellationToken)
        {
            try
            {
                var test = await _testRepository.GetTestById(request.TestForTesting.Test.Id);
                var userTest = await _userTestRepository.GetUserTest(request.UserId, request.TestForTesting.Test.Id);
                var user = await _userManager.FindByIdAsync(request.UserId);

                if (request.IsChecked == null || request.IsChecked == false)
                {
                    await _userTestRepository.Update(userTest.Id, userTest.IsActive, true, userTest.AssignedDate);

                    await _mediator.Send(new CreateTestingHistoryC
                    {
                        TestScore = 0,
                        ResultStatus = TestResultStatusEnum.Unknown,
                        UserTest = userTest,
                        TestQuestionList = request.TestForTesting.TestQuestionList,
                        IsChecked = false,
                        TestType = request.TestForTesting.Test.TestType
                    });

                    return new CheckTestingResponseDto
                    {
                        Description = "Тест был отправлен на проверку!"
                    };
                }
                else if (request.IsChecked != null && request.IsChecked == true)
                {
                    //Check 2LD Type Testing

                    double ball = 0;

                    foreach (var i in request.TestForTesting.TestQuestionList)
                    {
                        ball += i.CustomAnswerScore.Value;
                    }

                    ball /= request.TestForTesting.TestQuestionList.Count();

                    //Check 2LD Type Testing

                    var result = new CheckTestingResponseDto();
                    double score = 0.0;
                    TestResultStatusEnum status = TestResultStatusEnum.Unknown;
                    string desc = "";

                    foreach (var i in test.TestResultList)
                    {
                        if (i.RangeFrom <= ball && i.RangeTo >= ball)
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

                    await _userTestingHistoryRepository.Update(request.UserTestingHistoryId, ball, result.Status, true);

                    foreach (var i in request.TestForTesting.TestQuestionList)
                    {
                        var customAnswerId = i.CustomAnswerId.Value;
                        await _userTestingHistoryRepository.UpdateTestingHistoryCustomQuestionAnswer(customAnswerId, i.CustomAnswerScore.Value, i.CustomAnswerStatus);
                    }

                    //Send Message to email

                    string message = $"Тест был перепройден на {ball:0.0}%";
                    string firstName = user.FirstName ?? "";
                    string lastName = user.LastName ?? "";
                    string patronymic = user.Patronymic ?? "";
                    string fullName = firstName + " " + lastName + " " + patronymic;
                    string letterHeader = $"Прохождение теста: {test.Name}";

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
                
                return new CheckTestingResponseDto();  
            }
            catch (Exception ex)
            {
                _logger.LogError($"Check Second Level Difficult Type Testing for test: {request.TestForTesting.Test.Id} and for User: {request.UserId} failed {ex}", ex);
                throw ex;
            }
        }
    }
}
