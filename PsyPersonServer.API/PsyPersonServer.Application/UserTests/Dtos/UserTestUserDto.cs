using PsyPersonServer.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.UserTests.Dtos
{
    public class UserTestUserDto : UserDto
    {
        public string Status { get; set; }
        public int AmountAllUserTests { get; set; }
        public int AmountTestedUserTests { get; set; }
        public int AmountPendingUserTests { get; set; }
    }
}
