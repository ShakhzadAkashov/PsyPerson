﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<TestQuestionAnswer> TestQuestionAnswers { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
        public DbSet<UserTestingHistory> UserTestingHistories { get; set; }
        public DbSet<TestingHistoryQuestionAnswer> TestingHistoryQuestionAnswers { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<TestingHistoryCustomQuestionAnswer> TestingHistoryCustomQuestionAnswers { get; set; }
        public DbSet<EmailMessageSetting> EmailMessageSettings { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<UserTestingHistory>()
            //   .HasOne(t => t.UserTestFk)
            //   .WithMany()
            //   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TestingHistoryQuestionAnswer>()
                .HasOne(t => t.UserTestingHistoryFk)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
