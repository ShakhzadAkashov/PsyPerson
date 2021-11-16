using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public async Task<List<ApplicationRole>> GetUserRoles(string userId)
        {
            var roles = new List<ApplicationRole>();
            var user = await _userManager.FindByIdAsync(userId);
            var roleNames = await _userManager.GetRolesAsync(user);

            foreach (var i in roleNames)
            {
                var role =  await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == i);

                if (role != null)
                    roles.Add(role);
            }

            return roles;
        }
    }
}
