using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using PsyPersonServer.Domain.Models.ApplicationSettings;
using Microsoft.Extensions.Options;

namespace PsyPersonServer.Application.ApplicationUsers.Commands.Login
{
    public class LoginCh : IRequestHandler<LoginC, object>
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationSettings _appSettings;
        private readonly ILogger<LoginCh> _logger;
        public LoginCh(UserManager<ApplicationUser> userManager, IOptions<ApplicationSettings> appSettings, ILogger<LoginCh> logger, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
            _logger = logger;
        }
        public async Task<object> Handle(LoginC request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                //Get roles assigned to user
                var roles = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                var claims = new List<Claim>();

                claims.Add(new Claim("UserID", user.Id.ToString()));
                foreach (var userRole in roles)
                {
                    claims.Add(new Claim(_options.ClaimsIdentity.RoleClaimType, userRole));

                    var role = await _roleManager.FindByNameAsync(userRole);
                    if (role != null)
                    {
                        var permissions = await _roleManager.GetClaimsAsync(role);
                        foreach (Claim permission in permissions)
                        {
                            if(!claims.Any(x => x.Type == permission.Type && x.Value == permission.Value))
                                claims.Add(permission);
                        }
                    }
                }
                
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(600),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return new { token };
            }
            else
            {
                throw new Exception("UserName or password is incorrect.");
            }
        }
    }
}
