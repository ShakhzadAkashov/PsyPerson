using MediatR;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Tests.Commands.RemoveTest
{
    public class RemoveTestCh : IRequestHandler<RemoveTestC, bool>
    {
        public RemoveTestCh(ITestRepository testRepository, ILogger<RemoveTestCh> logger)
        {
            _testRepository = testRepository;
            _logger = logger;
        }

        private readonly ITestRepository _testRepository;
        private readonly ILogger<RemoveTestCh> _logger;

        public async Task<bool> Handle(RemoveTestC request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _testRepository.Remove(request.Id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Remove Test, Id: {request.Id} failed. Exception: {ex}", ex);
                throw ex;
            }
        }
    }
}
