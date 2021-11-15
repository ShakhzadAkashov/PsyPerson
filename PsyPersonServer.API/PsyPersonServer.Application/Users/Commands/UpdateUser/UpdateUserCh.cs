using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Users.Commands.UpdateUser
{
    class UpdateUserCh : IRequestHandler<UpdateUserC, IdentityResult>
    {
        public UpdateUserCh(UserManager<ApplicationUser> userManager, ILogger<UpdateUserCh> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UpdateUserCh> _logger;
        public async Task<IdentityResult> Handle(UpdateUserC request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            //var removedRoles = await _userManager.GetRolesAsync(user);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Patronymic = request.Patronymic;
            user.Email = request.Email;
            user.UserName = request.UserName;
            user.PhoneNumber = request.PhoneNumber;

            try
            {
                //await _userManager.RemoveFromRolesAsync(user, removedRoles);
                //await _userManager.AddToRoleAsync(user,request.Role);
                var result = await _userManager.UpdateAsync(user);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update User: {user.UserName} failed {ex}", ex);
                throw ex;
            }
        }
    }
}
