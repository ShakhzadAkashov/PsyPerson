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

namespace PsyPersonServer.Application.Testings.Commands.CheckFirstLevelDifficultTypeTesting
{
    public class CheckFirstLevelDifficultTypeTestingCh : IRequestHandler<CheckFirstLevelDifficultTypeTestingC, CheckTestingResponseDto>
    {
        public CheckFirstLevelDifficultTypeTestingCh(ILogger<CheckFirstLevelDifficultTypeTestingCh> logger, IMediator mediator, ITestRepository testRepository, IUserTestRepository userTestRepository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _mediator = mediator;
            _testRepository = testRepository;
            _userTestRepository = userTestRepository;
            _userManager = userManager;
        }

        private ILogger<CheckFirstLevelDifficultTypeTestingCh> _logger;
        private readonly IMediator _mediator;
        private readonly ITestRepository _testRepository;
        private readonly IUserTestRepository _userTestRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public async Task<CheckTestingResponseDto> Handle(CheckFirstLevelDifficultTypeTestingC request, CancellationToken cancellationToken)
        {
            var test = await _testRepository.GetTestById(request.TestForTesting.Test.Id);
            var userTest = await _userTestRepository.GetUserTest(request.UserId, request.TestForTesting.Test.Id);
            var user = await _userManager.FindByIdAsync(request.UserId);

            try
            {
                //Check 1LD Type Testing

                double ball = 0;

                foreach (var i in request.TestForTesting.TestQuestionList)
                {
                    ball += i.SelectedAnswer.Score.Value;

                    foreach (var j in i.Answers)
                    {
                        if (i.SelectedAnswer.Id == j.Id)
                        {
                            j.IsCorrect = true;
                        }
                    }
                }

                ball /= request.TestForTesting.TestQuestionList.Count();

                //Check 1LD Type Testing

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
                    TestQuestionList = request.TestForTesting.TestQuestionList
                });

                //Send Message to email

                string message = $"Тест был перепройден на {ball:0.0}%";
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
                _logger.LogError($"Check First Level Difficult Type Testing for test: {request.TestForTesting.Test.Id} and for User: {request.UserId} failed {ex}", ex);
                throw ex;
            }
        }
    }
}
