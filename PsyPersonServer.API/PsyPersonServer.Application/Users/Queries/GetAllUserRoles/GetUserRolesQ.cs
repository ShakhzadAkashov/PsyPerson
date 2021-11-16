using MediatR;
using PsyPersonServer.Application.ApplicationRoles.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Users.Queries.GetAllUserRoles
{
    public class GetUserRolesQ : IRequest<PagedResponse<RoleDto>>
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
        public string UserId { get; set; }
    }
}
