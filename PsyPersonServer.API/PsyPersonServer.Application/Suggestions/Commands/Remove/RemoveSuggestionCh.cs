using MediatR;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Suggestions.Commands.Remove
{
    public class RemoveSuggestionCh : IRequestHandler<RemoveSuggestionC, bool>
    {
        public RemoveSuggestionCh(ISuggestionRepository repo, ILogger<RemoveSuggestionCh> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        private readonly ISuggestionRepository _repo;
        private readonly ILogger<RemoveSuggestionCh> _logger;

        public async Task<bool> Handle(RemoveSuggestionC request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _repo.Remove(request.Id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Remove Suggestion by Id: {request.Id} failed. Exception: {ex}", ex);
                throw ex;
            }
        }
    }
}
