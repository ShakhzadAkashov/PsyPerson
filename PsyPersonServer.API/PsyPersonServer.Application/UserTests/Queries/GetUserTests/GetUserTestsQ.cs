using MediatR;
using PsyPersonServer.Application.UserTests.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.UserTests.Queries.GetUserTests
{
    public class GetUserTestsQ : IRequest<PagedResponse<UserTestDto>>
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
        public string UserId { get; set; }
        public string TestName { get; set; }
    }
}
