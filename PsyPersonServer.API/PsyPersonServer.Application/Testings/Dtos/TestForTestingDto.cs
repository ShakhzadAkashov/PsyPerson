using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Application.Tests.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Dtos
{
    public class TestForTestingDto
    {
        public TestDto Test { get; set; }
        public IEnumerable<TestQuestionDto> TestQuestionList { get; set; }
    }
}
