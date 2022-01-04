using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Users.Dtos
{
    public class BlockAndUnBlockUserResponseDto
    {
        public bool IsBlocked { get; set; }
        public bool Result { get; set; }
    }
}
