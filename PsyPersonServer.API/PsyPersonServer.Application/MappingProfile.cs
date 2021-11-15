using AutoMapper;
using PsyPersonServer.Application.ApplicationRoles.Dtos;
using PsyPersonServer.Application.Users.Dtos;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<ApplicationRole, RoleDto>();
        }
    }
}
