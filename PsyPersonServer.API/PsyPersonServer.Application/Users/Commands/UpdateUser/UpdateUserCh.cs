using MediatR;
using Microsoft.AspNetCore.Identity;
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
        public UpdateUserCh(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        private readonly UserManager<ApplicationUser> _userManager;
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
                throw ex;
            }
        }
    }
}
