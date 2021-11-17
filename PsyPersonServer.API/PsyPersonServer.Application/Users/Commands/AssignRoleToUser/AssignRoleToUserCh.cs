using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Users.Commands.AssignRoleToUser
{
    public class AssignRoleToUserCh : IRequestHandler<AssignRoleToUserC, IdentityResult>
    {
        public AssignRoleToUserCh(UserManager<ApplicationUser> userManager, ILogger<AssignRoleToUserCh> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AssignRoleToUserCh> _logger;

        public async Task<IdentityResult> Handle(AssignRoleToUserC request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, request.RoleName);
                    return result;
                }
                else
                {
                    _logger.LogError($"User: {user} Not Found");
                    throw new Exception($"User: {user} Not Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Assing role to User failed {ex}", ex);
                throw ex;
            }
            
        }
    }
}
