using MediatR;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.EmailMessage.Commands.RemoveEmailMessageSetting
{
    public class RemoveEmailMessageSettingCh : IRequestHandler<RemoveEmailMessageSettingC, bool>
    {
        public RemoveEmailMessageSettingCh(IEmailMessageRepository emailMessageRepository, ILogger<RemoveEmailMessageSettingCh> logger)
        {
            _emailMessageRepository = emailMessageRepository;
            _logger = logger;
        }

        private readonly IEmailMessageRepository _emailMessageRepository;
        private readonly ILogger<RemoveEmailMessageSettingCh> _logger;

        public async Task<bool> Handle(RemoveEmailMessageSettingC request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _emailMessageRepository.RemoveSetting(request.Id);
                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Remove Email Message Setting, Id: {request.Id} failed. Exception: {ex}", ex);
                throw ex;
            }
        }
    }
}
