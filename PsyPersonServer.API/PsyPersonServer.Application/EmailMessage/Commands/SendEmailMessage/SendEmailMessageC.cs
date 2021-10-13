using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.EmailMessage.Commands.SendEmailMessage
{
    public class SendEmailMessageC : IRequest<bool>
    {
        public string ReceiverMailAddress { get; set; }
        public string EmailMessage { get; set; }
        public string ReceiverFullName { get; set; }
        public string LetterHeader { get; set; }
        public bool IsHTML { get; set; }
    }
}
