using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.ApplicationRoles.Commands.RemoveRole
{
    public class RemoveRoleCh : IRequestHandler<RemoveRoleC, IdentityResult>
    {
        public RemoveRoleCh(RoleManager<ApplicationRole> roleManager, ILogger<RemoveRoleCh> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<RemoveRoleCh> _logger;
        public async Task<IdentityResult> Handle(RemoveRoleC request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.RoleId);

                if (role != null)
                {
                    var result = await _roleManager.DeleteAsync(role);
                    return result;
                }
                else
                {
                    _logger.LogError($"Role: {role} Not Found");
                    throw new Exception($"Role: {role} Not Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Remove Role failed {ex}", ex);
                throw ex;
            }
        }
    }
}
