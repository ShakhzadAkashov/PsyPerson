using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Entities
{
    public class Suggestion
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double RangeFrom { get; set; }
        public double RangeTo { get; set; }
        public TestResultStatusEnum Status { get; set; }
        public SuggestionSelectTypeEnum SelectionType { get; set; }
    }
}
