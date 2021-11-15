using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.ApplicationRoles.Commands.UpdateRole
{
    public class UpdateRoleCh : IRequestHandler<UpdateRoleC, IdentityResult>
    {
        public UpdateRoleCh(RoleManager<ApplicationRole> roleManager, ILogger<UpdateRoleCh> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<UpdateRoleCh> _logger;

        public async Task<IdentityResult> Handle(UpdateRoleC request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);

            role.Name = request.Name;
            role.Description = request.Description;

            try
            {
                var result = await _roleManager.UpdateAsync(role);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update Role: {role.Name} failed {ex}", ex);
                throw ex;
            }
        }
    }
}
