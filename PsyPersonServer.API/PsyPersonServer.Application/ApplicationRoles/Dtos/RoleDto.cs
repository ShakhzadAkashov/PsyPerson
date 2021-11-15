using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.ApplicationRoles.Dtos
{
    public class RoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string NormalizedName { get; set; }
    }
}
