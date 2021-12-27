using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Permissions.Dtos
{
    public class RolePermissionsDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<CheckBoxDto> RoleClaims { get; set; }
    }
}
