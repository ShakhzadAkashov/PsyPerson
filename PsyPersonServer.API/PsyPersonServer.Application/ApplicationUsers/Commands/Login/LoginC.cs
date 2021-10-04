using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.ApplicationUsers.Commands.Login
{
    public class LoginC : IRequest<object>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
