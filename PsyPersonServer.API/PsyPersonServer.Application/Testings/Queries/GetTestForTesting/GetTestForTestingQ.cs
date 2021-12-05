using MediatR;
using PsyPersonServer.Application.Testings.Dtos;
using PsyPersonServer.Application.TestQuestions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Queries.GetTestForTesting
{
    public class GetTestForTestingQ : IRequest<TestForTestingDto>
    {
        public Guid TestId { get; set; }
    }
}
