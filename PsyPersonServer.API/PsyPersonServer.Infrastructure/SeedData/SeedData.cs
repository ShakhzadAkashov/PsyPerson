using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace PsyPersonServer.Infrastructure.SeedData
{
    public static class SeedData
    {
        private const string AdminUser = "Admin";
        private const string AdminPassword = "123qwe";
        private const string AdminRole = "Admin";
        private const string AdminRoleDesc = "Role for Admin";
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                UserManager<ApplicationUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                RoleManager<ApplicationRole> _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                //Seed AdminRole
                var role = await _roleManager.FindByNameAsync(AdminRole);

                if (!_roleManager.Roles.Any() || role == null)
                {
                    role = new ApplicationRole
                    {
                        Name = AdminRole,
                        Description = AdminRoleDesc,
                        CreatedDate = DateTime.Now
                    };

                    await _roleManager.CreateAsync(role);
                }

                var roleClaims = await _roleManager.GetClaimsAsync(role);

                if (roleClaims != null && roleClaims.Count > 0)
                {
                    foreach (var claim in roleClaims)
                        await _roleManager.RemoveClaimAsync(role, claim);
                }

                FieldInfo[] allClaims = typeof(Domain.Models.Permission.Permissions).GetFields();

                foreach (var i in allClaims)
                    await _roleManager.AddClaimAsync(role, new Claim("Permission", i.GetValue(i).ToString()));

                //Seed AdminUser
                var user = await _userManager.FindByNameAsync(AdminUser);

                if (!_userManager.Users.Any() || user == null)
                {
                    user = new ApplicationUser()
                    {
                        UserName = AdminUser,
                        Email = "",
                        FirstName = "",
                        LastName = "",
                        Patronymic = ""
                    };

                    await _userManager.CreateAsync(user, AdminPassword);
                    await _userManager.AddToRoleAsync(user, AdminRole);
                }
            }
        }
    }
}
