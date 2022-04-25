using MediatR;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.EmailMessage.Commands.UpdateEmailMessageSetting
{
    public class UpdateEmailMessageSettingCh : IRequestHandler<UpdateEmailMessageSettingC, bool>
    {
        public UpdateEmailMessageSettingCh(IEmailMessageRepository repository)
        {
            _repository = repository;
        }

        private readonly IEmailMessageRepository _repository;

        public async Task<bool> Handle(UpdateEmailMessageSettingC request, CancellationToken cancellationToken)
        {
            return await _repository.UpdateSetting(request.HostName, request.SenderAddress, request.SenderPswd, request.MessageDisplayName);
        }
    }
}
