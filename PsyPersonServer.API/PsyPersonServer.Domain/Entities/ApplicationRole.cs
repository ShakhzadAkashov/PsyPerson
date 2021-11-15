using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
    }
}
