using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Syggestions.Dtos
{
    public class SuggestionDto
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
