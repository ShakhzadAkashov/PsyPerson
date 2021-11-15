using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.ApplicationRoles.Commands.UpdateRole
{
    public class UpdateRoleC : IRequest<IdentityResult>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
