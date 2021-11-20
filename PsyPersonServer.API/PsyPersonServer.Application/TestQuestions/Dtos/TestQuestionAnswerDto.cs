using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.TestQuestions.Dtos
{
    public class TestQuestionAnswerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsCorrect { get; set; }
        public Guid TestQuestionId { get; set; }
    }
}
