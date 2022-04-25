using MediatR;
using PsyPersonServer.Application.EmailMessage.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.EmailMessage.Commands.UpdateEmailMessageSetting
{
    public class UpdateEmailMessageSettingC : EmailMessageSettingDto, IRequest<bool> { }
}
