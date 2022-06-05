using MediatR;
using PsyPersonServer.Application.Syggestions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Suggestions.Queries.GetById
{
    public class GetByIdQ : IRequest<SuggestionDto>
    {
        public Guid Id { get; set; }
    }
}
