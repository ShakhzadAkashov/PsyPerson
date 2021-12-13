using MediatR;
using PsyPersonServer.Application.Tests.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Tests.Queries.GetTestsByUserId
{
    public class GetTestsByUserIdForLookupTableQ : IRequest<PagedResponse<TestDto>>
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
        public string UserId { get; set; }
    }
}
