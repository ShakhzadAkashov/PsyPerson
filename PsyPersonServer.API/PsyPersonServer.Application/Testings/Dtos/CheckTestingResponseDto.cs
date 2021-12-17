using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Dtos
{
    public class CheckTestingResponseDto
    {
        public double TestScore { get; set; }
        public TestResultStatusEnum Status { get; set; }
        public string Description { get; set; }
    }
}
