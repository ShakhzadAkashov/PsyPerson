using MediatR;
using PsyPersonServer.Application.Testings.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Commands.CheckFirstLevelDifficultTypeTesting
{
    public class CheckFirstLevelDifficultTypeTestingC : IRequest<CheckTestingResponseDto>
    {
        public TestForTestingDto TestForTesting { get; set; }
        public string UserId { get; set; }
    }
}
