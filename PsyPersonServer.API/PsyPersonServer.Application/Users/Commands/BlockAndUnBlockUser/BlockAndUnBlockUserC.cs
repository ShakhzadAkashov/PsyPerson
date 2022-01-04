using MediatR;
using PsyPersonServer.Application.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Users.Commands.BlockAndUnBlockUser
{
    public class BlockAndUnBlockUserC : IRequest<BlockAndUnBlockUserResponseDto>
    {
        public string UserId { get; set; }
    }
}
