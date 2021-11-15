using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Users.Commands.CreateUser
{
    public class CreateUserCh : IRequestHandler<CreateUserC, IdentityResult>
    {
        public CreateUserCh(UserManager<ApplicationUser> userManager, ILogger<CreateUserCh> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CreateUserCh> _logger;
        public async Task<IdentityResult> Handle(CreateUserC request, CancellationToken cancellationToken)
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
                _logger.LogError($"Create User failed {ex}", ex);
                throw ex;
            }
        }
    }
}
