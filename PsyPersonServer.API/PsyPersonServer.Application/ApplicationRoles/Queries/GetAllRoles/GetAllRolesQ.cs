using MediatR;
using PsyPersonServer.Application.ApplicationRoles.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.ApplicationRoles.Queries.GetAllRoles
{
    public class GetAllRolesQ : IRequest<PagedResponse<RoleDto>>
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
        public string Name { get; set; }
    }
}
