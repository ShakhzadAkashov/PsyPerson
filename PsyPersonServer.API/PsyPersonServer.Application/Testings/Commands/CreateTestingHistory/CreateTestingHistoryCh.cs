using MediatR;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Testings.Commands.CreateTestingHistory
{
    public class CreateTestingHistoryCh : IRequestHandler<CreateTestingHistoryC, bool>
    {
        public CreateTestingHistoryCh(IUserTestingHistoryRepository userTestingHistoryRepository, ILogger<CreateTestingHistoryCh> logger)
        {
            _userTestingHistoryRepository = userTestingHistoryRepository;
            _logger = logger;
        }

        private readonly IUserTestingHistoryRepository _userTestingHistoryRepository;
        private readonly ILogger<CreateTestingHistoryCh> _logger;

        public async Task<bool> Handle(CreateTestingHistoryC request, CancellationToken cancellationToken)
        {
            try
            {
                var userTestingHistory = await _userTestingHistoryRepository.Create(request.TestScore, request.ResultStatus, request.UserTest.Id);

                foreach (var i in request.TestQuestionList)
                {
                    foreach (var j in i.Answers)
                    {
                        var isMarked = j.IsCorrect ?? false;
                        await _userTestingHistoryRepository.CreateTestingHistoryQuestionAnswer(isMarked, j.Id, userTestingHistory.Id);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create Testing History for test: {request.UserTest.TestId} and for User: {request.UserTest.UserId} failed {ex}", ex);
                throw ex;
            }
        }
    }
}
