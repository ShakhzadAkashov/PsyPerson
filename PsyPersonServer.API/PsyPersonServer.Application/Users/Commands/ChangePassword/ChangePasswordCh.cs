using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Users.Commands.ChangePassword
{
    public class ChangePasswordCh : IRequestHandler<ChangePasswordC, IdentityResult>
    {
        public ChangePasswordCh(UserManager<ApplicationUser> userManager, ILogger<ChangePasswordCh> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ChangePasswordCh> _logger;

        public async Task<IdentityResult> Handle(ChangePasswordC request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);

                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
                    return result;
                }
                else
                {
                    _logger.LogError($"User: {request.UserId} Not Founded");
                    throw new Exception("User Not Founded!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Change Password for UserId: {request.UserId} failed {ex}", ex);
                throw ex;
            }
        }
    }
}
