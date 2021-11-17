using MediatR;
using Microsoft.AspNetCore.Identity;
using PsyPersonServer.Application.ApplicationRoles.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Users.Commands.AssignRoleToUser
{
    public class AssignRoleToUserC : IRequest<IdentityResult>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
