using MediatR;
using PsyPersonServer.Application.Permissions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Permissions.Queries.GetRolePermissions
{
    public class GetRolePermissionsQ : IRequest<RolePermissionsDto>
    {
        public string RoleId { get; set; }
    }
}
