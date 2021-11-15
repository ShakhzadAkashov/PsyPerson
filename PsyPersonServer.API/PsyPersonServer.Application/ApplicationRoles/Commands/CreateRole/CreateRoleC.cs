using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.ApplicationRoles.Commands.CreateRole
{
    public class CreateRoleC : IRequest<IdentityResult>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
