using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.ApplicationUsers.Commands.ResetPassword
{
    public class ResetPasswordCh : IRequestHandler<ResetPasswordC, bool>
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ResetPasswordCh> _logger;
        public ResetPasswordCh(UserManager<ApplicationUser> userManager, ILogger<ResetPasswordCh> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<bool> Handle(ResetPasswordC request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogError($"Invalid Request, User not Found.");
                throw new Exception("Invalid Request, User not Found");
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!resetPassResult.Succeeded)
            {
                var errors = resetPassResult.Errors.Select(e => e.Description);
                _logger.LogError($"Invalid Request {errors}", errors); 
                throw new Exception($"{errors }");
            }
            return true;
        }
    }
}
