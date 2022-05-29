using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Dtos
{
    public class UserTestingHistDto
    {
        public string TestName { get; set; }
        public DateTime TestedDate { get; set; }
        public double TestScore { get; set; }
        public string ResultStatus { get; set; }
    }
}
