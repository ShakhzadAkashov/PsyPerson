using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.ApplicationRoles.Commands.CreateRole
{
    public class CreateRoleCh : IRequestHandler<CreateRoleC, IdentityResult>
    {
        public CreateRoleCh(RoleManager<ApplicationRole> roleManager, ILogger<CreateRoleCh> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<CreateRoleCh> _logger;
        public async Task<IdentityResult> Handle(CreateRoleC request, CancellationToken cancellationToken)
        {
            var role = new ApplicationRole
            {
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.Now
            };

            try
            {
                var result = await _roleManager.CreateAsync(role);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create Role failed {ex}", ex);
                throw ex;
            }
        }
    }
}
