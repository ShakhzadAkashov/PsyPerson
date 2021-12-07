using MediatR;
using PsyPersonServer.Application.Testings.Dtos;
using PsyPersonServer.Application.TestQuestions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Commands.CheckSimpleTypeTesting
{
    public class CheckSimpleTypeTestingC : IRequest<double>
    {
        public TestForTestingDto TestForTesting { get; set; }
    }
}
