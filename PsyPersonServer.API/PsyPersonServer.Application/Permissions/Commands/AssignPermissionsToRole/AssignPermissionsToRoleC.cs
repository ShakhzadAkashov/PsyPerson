using MediatR;
using PsyPersonServer.Application.Permissions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Permissions.Commands.AssignPermissionsToRole
{
    public class AssignPermissionsToRoleC : RolePermissionsDto, IRequest<bool>
    {
    }
}
