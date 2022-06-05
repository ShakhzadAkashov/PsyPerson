using MediatR;
using PsyPersonServer.Application.Syggestions.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Suggestions.Queries.GetByStatus
{
    class GetByStatusQ : IRequest<PagedResponse<SuggestionDto>>
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
        public string Name { get; set; }
        public TestResultStatusEnum Status { get; set; }
    }
}
