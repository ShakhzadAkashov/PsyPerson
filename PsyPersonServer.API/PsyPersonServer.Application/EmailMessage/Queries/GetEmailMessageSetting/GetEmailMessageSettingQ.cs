using MediatR;
using PsyPersonServer.Application.EmailMessage.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.EmailMessage.Queries.GetEmailMessageSetting
{
    public class GetEmailMessageSettingQ : IRequest<EmailMessageSettingDto>
    {
    }
}
