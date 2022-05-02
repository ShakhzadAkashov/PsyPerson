using MediatR;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.UserTests.Commands.CountStatus
{
    public class CountStatusC : IRequest<TestResultStatusEnum>
    {
        public Guid TestId { get; set; }
        public List<decimal> TestScoreList { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}
