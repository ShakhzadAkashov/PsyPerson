using MediatR;
using PsyPersonServer.Application.UserTests.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.UserTests.Commands.CreateUserTest
{
    public class CreateUserTestC : IRequest<UserTestDto>
    {
        public string UserId { get; set; }
        public Guid TestId { get; set; }
    }
}
