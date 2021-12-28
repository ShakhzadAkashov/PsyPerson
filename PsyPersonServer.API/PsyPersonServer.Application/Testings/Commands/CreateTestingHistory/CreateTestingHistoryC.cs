using MediatR;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Commands.CreateTestingHistory
{
    public class CreateTestingHistoryC : IRequest<bool>
    {
        public double TestScore { get; set; }
        public TestResultStatusEnum ResultStatus { get; set; }
        public UserTest UserTest { get; set; }
        public IEnumerable<TestQuestionDto> TestQuestionList { get; set; }
        public bool? IsChecked { get; set; }
    }
}
