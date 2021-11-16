using MediatR;
using Microsoft.AspNetCore.Identity;
using PsyPersonServer.Application.ApplicationRoles.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Users.Commands.AssignRoleToUser
{
    public class AssignRoleToUserC : RoleDto, IRequest<IdentityResult>
    {
        public string UserId { get; set; }
    }
}
