using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Users.Commands.RemoveUser
{
    public class RemoveUserCh : IRequestHandler<RemoveUserC, IdentityResult>
    {
        public RemoveUserCh(UserManager<ApplicationUser> userManager, ILogger<RemoveUserCh> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RemoveUserCh> _logger;
        public async Task<IdentityResult> Handle(RemoveUserC request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);

                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
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
                _logger.LogError($"Remove User failed {ex}", ex);
                throw ex;
            }
        }
    }
}
