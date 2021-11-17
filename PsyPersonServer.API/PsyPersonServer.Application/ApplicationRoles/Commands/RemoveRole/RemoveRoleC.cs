using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.ApplicationRoles.Commands.RemoveRole
{
    public class RemoveRoleC : IRequest<IdentityResult>
    {
        public string RoleId { get; set; }
    }
}
