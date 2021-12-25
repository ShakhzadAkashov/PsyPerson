using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Models.EmailMessage
{
    public class EmailMessageSettings
    {
        public string HostName { get; set; }
        public string SenderAddress { get; set; }
        public string SenderPswd { get; set; }
        public string MessageDisplayName { get; set; }
    }
}
