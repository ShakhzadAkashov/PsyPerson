using AutoMapper;
using MediatR;
using PsyPersonServer.Application.EmailMessage.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.EmailMessage.Commands.CreateEmailMessageSetting
{
    public class CreateEmailMessageSettingCh : IRequestHandler<CreateEmailMessageSettingC, EmailMessageSettingDto>
    {
        public CreateEmailMessageSettingCh(IEmailMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly IEmailMessageRepository _repository;
        private readonly IMapper _mapper;

        public async Task<EmailMessageSettingDto> Handle(CreateEmailMessageSettingC request, CancellationToken cancellationToken)
        {
            var setting = await _repository.CreateSetting(request.HostName, request.SenderAddress, request.SenderPswd, request.MessageDisplayName);

            return _mapper.Map<EmailMessageSettingDto>(setting);
        }
    }
}
