using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Infrastructure
{
    public class DBContext : IdentityDbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}
