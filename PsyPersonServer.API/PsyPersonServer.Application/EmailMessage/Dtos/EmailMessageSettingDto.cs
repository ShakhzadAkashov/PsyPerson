using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.EmailMessage.Dtos
{
    public class EmailMessageSettingDto
    {
        public Guid Id { get; set; }
        public string HostName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderPswd { get; set; }
        public string MessageDisplayName { get; set; }
    }
}
