using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.TestQuestions.Dtos
{
    public class TestQuestionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid TestId { get; set; }
        public List<TestQuestionAnswerDto> Answers { get; set; }
        public int AmountCorrectAnswers { get; set; }
        public TestQuestionAnswerDto? SelectedAnswer { get; set; }
        public string CustomAnswer {get;set;}
    }
}
