using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.ApplicationUsers.Commands.Register
{
    public class RegisterC: IRequest<IdentityResult>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
