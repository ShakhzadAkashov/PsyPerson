using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Users.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserC : IRequest<IdentityResult>
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
