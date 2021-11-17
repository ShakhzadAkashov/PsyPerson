using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Users.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCh : IRequestHandler<RemoveRoleFromUserC, IdentityResult>
    {
        public RemoveRoleFromUserCh(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager, ILogger<RemoveRoleFromUserCh> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ILogger<RemoveRoleFromUserCh> _logger;
        public async Task<IdentityResult> Handle(RemoveRoleFromUserC request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            var role = await _roleManager.FindByIdAsync(request.RoleId);

            try
            {
                var result = await _userManager.RemoveFromRoleAsync(user,role.Name);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Remove Role: {role.Name} from User: {user.UserName} failed {ex}", ex);
                throw ex;
            }
        }
    }
}
