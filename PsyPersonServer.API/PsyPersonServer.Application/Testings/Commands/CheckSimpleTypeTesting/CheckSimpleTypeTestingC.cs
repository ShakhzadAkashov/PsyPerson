using MediatR;
using PsyPersonServer.Application.Testings.Dtos;
using PsyPersonServer.Application.TestQuestions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Commands.CheckSimpleTypeTesting
{
    public class CheckSimpleTypeTestingC : IRequest<CheckTestingResponseDto>
    {
        public TestForTestingDto TestForTesting { get; set; }
        public string UserId { get; set; }
    }
}
