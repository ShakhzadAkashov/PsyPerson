using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Suggestions.Commands.Remove
{
    public class RemoveSuggestionC : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
