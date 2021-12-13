using PsyPersonServer.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.UserTests.Dtos
{
    public class UserTestUserDto : UserDto
    {
        public List<UserTestDto> UserTestList { get; set; }
        public string Status { get; set; }
    }
}
