using MediatR;
using PsyPersonServer.Application.EmailMessage.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.EmailMessage.Commands.CreateEmailMessageSetting
{
    public class CreateEmailMessageSettingC : IRequest<EmailMessageSettingDto>
    {
        public string HostName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderPswd { get; set; }
        public string MessageDisplayName { get; set; }
    }
}
