using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Permissions.Commands.AssignPermissionsToRole
{
    public class AssignPermissionsToRoleCh : IRequestHandler<AssignPermissionsToRoleC, bool>
    {
        public AssignPermissionsToRoleCh(RoleManager<ApplicationRole> roleManager, ILogger<AssignPermissionsToRoleCh> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<AssignPermissionsToRoleCh> _logger;

        public async Task<bool> Handle(AssignPermissionsToRoleC request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(request.RoleId);

                if (role == null)
                    throw new Exception("Role Not Found!");

                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var claim in roleClaims)
                    await _roleManager.RemoveClaimAsync(role, claim);

                var selectedClaims = request.RoleClaims.Where(c => c.IsSelected).ToList();

                foreach (var claim in selectedClaims)
                    await _roleManager.AddClaimAsync(role, new Claim("Permission", claim.DisplayValue));

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Assign Permissions To Role: {request.RoleName} failed {ex}", ex);
                throw ex;
            }
            
        }
    }
}
