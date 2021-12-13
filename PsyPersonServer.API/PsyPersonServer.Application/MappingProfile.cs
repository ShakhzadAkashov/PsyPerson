﻿using AutoMapper;
using PsyPersonServer.Application.ApplicationRoles.Dtos;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Application.Tests.Dtos;
using PsyPersonServer.Application.Users.Dtos;
using PsyPersonServer.Application.UserTests.Dtos;
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
            CreateMap<Test, TestDto>();
            CreateMap<TestQuestion, TestQuestionDto>();
            CreateMap<TestQuestionAnswer, TestQuestionAnswerDto>().ReverseMap();
            CreateMap<TestResult, TestResultDto>().ReverseMap();
            CreateMap<UserTest, UserTestDto>()
                .ForMember(dest => dest.Test, opt => opt.MapFrom(src => src.TestFk))
                .ReverseMap();
            CreateMap<UserTestingHistory, UserTestingHistoryDto>().ReverseMap();
            CreateMap<ApplicationUser, UserTestUserDto>();
        }
    }
}
