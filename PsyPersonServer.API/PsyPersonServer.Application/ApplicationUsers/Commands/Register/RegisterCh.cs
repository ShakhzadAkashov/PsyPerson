using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PsyPersonServer.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace PsyPersonServer.Application.ApplicationUsers.Commands.Register
{
    public class RegisterCh : IRequestHandler<RegisterC, IdentityResult>
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterCh> _logger;
        public RegisterCh(UserManager<ApplicationUser> userManager, ILogger<RegisterCh> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IdentityResult> Handle(RegisterC request, CancellationToken cancellationToken)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Patronymic = request.Patronymic,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, request.Password);
                //await _userManager.AddToRoleAsync(applicationUser, request.Role);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"User Register failed {ex}",ex);
                throw ex;
            }
        }
    }
}
