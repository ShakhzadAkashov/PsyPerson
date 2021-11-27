using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PsyPersonServer.Domain.Entities
{
    public class TestQuestion
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TestQuestionTypeEnum QuestionType { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid TestId { get; set; }

        [ForeignKey("TestId")]
        public Test TestFk { get; set; }
        public List<TestQuestionAnswer> Answers { get; set; }
    }
}
