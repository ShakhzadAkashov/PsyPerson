using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Application.Testings.Commands.CreateTestingHistory;
using PsyPersonServer.Application.Testings.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.Tests;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Testings.Commands.CheckSecondLevelDifficultTypeTesting
{
    public class CheckSecondLevelDifficultTypeTestingCh : IRequestHandler<CheckSecondLevelDifficultTypeTestingC, CheckTestingResponseDto>
    {
        public CheckSecondLevelDifficultTypeTestingCh(ILogger<CheckSecondLevelDifficultTypeTestingCh> logger, IMediator mediator, ITestRepository testRepository, IUserTestRepository userTestRepository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _mediator = mediator;
            _testRepository = testRepository;
            _userTestRepository = userTestRepository;
            _userManager = userManager;
        }

        private ILogger<CheckSecondLevelDifficultTypeTestingCh> _logger;
        private readonly IMediator _mediator;
        private readonly ITestRepository _testRepository;
        private readonly IUserTestRepository _userTestRepository;
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
                else 
                {
                    return new CheckTestingResponseDto();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Check Second Level Difficult Type Testing for test: {request.TestForTesting.Test.Id} and for User: {request.UserId} failed {ex}", ex);
                throw ex;
            }
        }
    }
}
