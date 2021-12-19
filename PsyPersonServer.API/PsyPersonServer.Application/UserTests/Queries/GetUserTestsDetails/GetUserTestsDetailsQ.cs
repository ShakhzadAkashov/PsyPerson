using MediatR;
using PsyPersonServer.Application.UserTests.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.UserTests.Queries.GetUserTestsDetails
{
    public class GetUserTestsDetailsQ : IRequest<PagedResponse<UserTestDetailDto>>
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
        public string UserId { get; set; }
    }
}
