using MediatR;
using Microsoft.AspNetCore.Identity;
using PsyPersonServer.Application.Permissions.Dtos;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Permissions.Queries.GetRolePermissions
{
    public class GetRolePermissionsQh : IRequestHandler<GetRolePermissionsQ, RolePermissionsDto>
    {
        public GetRolePermissionsQh(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        private readonly RoleManager<ApplicationRole> _roleManager;

        public async Task<RolePermissionsDto> Handle(GetRolePermissionsQ request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);

            var roleClaims = _roleManager.GetClaimsAsync(role).Result.Select(x => x.Value).ToList();
            FieldInfo[] allClaims = typeof(Domain.Models.Permission.Permissions).GetFields();
            var allPermissions = allClaims.Select(x => new CheckBoxDto { DisplayValue = x.GetValue(x).ToString() }).ToList();

            foreach (var permission in allPermissions)
            {
                if (roleClaims.Any(x => x == permission.DisplayValue))
                    permission.IsSelected = true;
            }

            var result = new RolePermissionsDto
            {
                RoleId = role.Id,
                RoleName = role.Name,
                RoleClaims = allPermissions
            };

            return result;
        }
    }
}
