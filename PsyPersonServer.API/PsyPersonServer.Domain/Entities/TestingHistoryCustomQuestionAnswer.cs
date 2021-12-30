using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PsyPersonServer.Domain.Entities
{
    public class TestingHistoryCustomQuestionAnswer
    {
        public Guid Id {get;set;}
        public string Name { get; set; }
        public double AnswerScore { get; set; }
        public AnswerResultStatusEnum AnswerStatus { get; set; }
        public Guid UserTestingHistoryId { get; set; }
        [ForeignKey("UserTestingHistoryId")]
        public UserTestingHistory UserTestingHistoryFk { get; set; }
        public Guid TestQuestionId { get; set; }
        [ForeignKey("TestQuestionId")]
        public TestQuestion TestQuestionFk { get; set; }
    }
}
