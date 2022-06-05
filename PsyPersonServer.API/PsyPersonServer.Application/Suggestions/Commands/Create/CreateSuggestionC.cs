using MediatR;
using PsyPersonServer.Application.Syggestions.Dtos;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Suggestions.Commands.Create
{
    public class CreateSuggestionC : IRequest<SuggestionDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double RangeFrom { get; set; }
        public double RangeTo { get; set; }
        public TestResultStatusEnum Status { get; set; }
        public SuggestionSelectTypeEnum SelectionType { get; set; }
    }
}
