using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordC : IRequest<IdentityResult>
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
        public bool IsOwner { get; set; }
    }
}
