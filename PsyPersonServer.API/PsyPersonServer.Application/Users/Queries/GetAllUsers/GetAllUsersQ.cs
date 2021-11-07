using MediatR;
using PsyPersonServer.Application.Users.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Users.Queries
{
    public class GetAllUsersQ : IRequest<PagedResponse<UserDto>>
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
    }
}
