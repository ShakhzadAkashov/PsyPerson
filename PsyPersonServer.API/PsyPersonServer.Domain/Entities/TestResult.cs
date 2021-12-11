using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PsyPersonServer.Domain.Entities
{
    public class TestResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double RangeFrom { get; set; }
        public double RangeTo { get; set; }
        public TestResultStatusEnum Status { get; set; }
        public Guid TestId { get; set; }
        [ForeignKey("TestId")]
        public Test TestFk { get; set; }
    }
}
