using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Users.Commands.RemoveUser
{
    public class RemoveUserC : IRequest<IdentityResult>
    {
        public string UserId { get; set; }
    }
}
