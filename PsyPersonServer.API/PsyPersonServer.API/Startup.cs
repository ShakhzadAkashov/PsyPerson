using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PsyPersonServer.Application.ApplicationUsers.Commands.Register;
using PsyPersonServer.Application.Permissions.Filters;
using PsyPersonServer.Domain.DapperRepositories;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.ApplicationSettings;
using PsyPersonServer.Domain.Models.EmailMessage;
using PsyPersonServer.Domain.Models.Tests;
using PsyPersonServer.Domain.Repositories;
using PsyPersonServer.Infrastructure;
using PsyPersonServer.Infrastructure.DapperRepositories;
using PsyPersonServer.Infrastructure.Repositories;
using PsyPersonServer.Infrastructure.SeedData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Custom Auth Policy and Handler
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //Custom Auth Policy and Handler

            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
            services.Configure<EmailMessageSettings>(Configuration.GetSection("EmailMessageSettings"));
            services.Configure<UploadTestQuestionsFromFileSettings>(Configuration.GetSection("TestQuestionsFromFileSettings"));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PsyPersonServer.API", Version = "v1" });
            });

            services.AddDbContext<DBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<DBContext>()
                .AddDefaultTokenProviders();

            // Dapper
            services.AddSingleton<DapperContext>();
            services.AddTransient<IDapperUserTestingHistoryRepository, DapperUserTestingHistoryRepository>();

            services.AddTransient<IUserRepository,UserRepository>();
            services.AddTransient<ITestRepository,TestRepository>();
            services.AddTransient<ITestQuestionRepository,TestQuestionRepository>();
            services.AddTransient<IUserTestRepository, UserTestRepository>();
            services.AddTransient<IUserTestingHistoryRepository, UserTestingHistoryRepository>();
            services.AddTransient<IEmailMessageRepository, EmailMessageRepository>();


            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });

            services.AddCors();
            services.AddMediatR(typeof(RegisterCh).Assembly);
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            //Jwt Authontication

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PsyPersonServer.API v1"));
            }
            if (env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PsyPersonServer.API v1"));
            }

            app.UseCors(build =>
                build.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString())
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Seed Data to Db
            SeedData.EnsurePopulated(app);
        }
    }
}
