using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Tests.Dtos
{
    public class TestResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double RangeFrom { get; set; }
        public double RangeTo { get; set; }
        public TestResultStatusEnum Status { get; set; }
        public Guid TestId { get; set; }
        public int IdForView { get; set; }
    }
}
