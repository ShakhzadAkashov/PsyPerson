using MediatR;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.TestQuestions.Commands.RemoveTestQuestion
{
    public class RemoveTestQuestionCh : IRequestHandler<RemoveTestQuestionC, bool>
    {
        public RemoveTestQuestionCh(ITestQuestionRepository testQuestionRepository, ILogger<RemoveTestQuestionCh> logger)
        {
            _testQuestionRepository = testQuestionRepository;
            _logger = logger;
        }

        private readonly ITestQuestionRepository _testQuestionRepository;
        private readonly ILogger<RemoveTestQuestionCh> _logger;

        public async Task<bool> Handle(RemoveTestQuestionC request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _testQuestionRepository.Remove(request.Id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Remove TestQuestion, Id: {request.Id} failed. Exception: {ex}", ex);
                throw ex;
            }
        }
    }
}
