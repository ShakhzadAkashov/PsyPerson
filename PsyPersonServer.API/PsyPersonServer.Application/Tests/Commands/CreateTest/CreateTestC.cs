using MediatR;
using PsyPersonServer.Application.Tests.Dtos;
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
    }
}
