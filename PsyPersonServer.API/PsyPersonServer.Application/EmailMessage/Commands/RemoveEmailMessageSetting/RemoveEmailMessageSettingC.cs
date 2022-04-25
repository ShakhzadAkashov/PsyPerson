using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.EmailMessage.Commands.RemoveEmailMessageSetting
{
    public class RemoveEmailMessageSettingC : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
