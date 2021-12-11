using MediatR;
using PsyPersonServer.Application.Tests.Dtos;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Tests.Commands.CreateTest
{
    public class CreateTestC : IRequest<TestDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public TestTypeEnum TestType { get; set; }
        public List<TestResultDto> TestResultList { get; set; }
    }
}
