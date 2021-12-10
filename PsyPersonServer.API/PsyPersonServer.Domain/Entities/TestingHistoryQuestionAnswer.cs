using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PsyPersonServer.Domain.Entities
{
    public class TestingHistoryQuestionAnswer
    {
        public Guid Id { get; set; }
        public bool IsMarked { get; set; }
        public Guid AnswerId { get; set; }
        [ForeignKey("AnswerId")]
        public TestQuestionAnswer TestQuestionAnswerFk { get; set; }
        public Guid UserTestingHistoryId { get; set; }
        [ForeignKey("UserTestingHistoryId")]
        public UserTestingHistory UserTestingHistoryFk { get; set; }
    }
}
