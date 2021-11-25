using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PsyPersonServer.Domain.Entities
{
    public class TestQuestionAnswer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsCorrect { get; set; }
        public Guid TestQuestionId { get; set; }
        public int IdForView { get; set; }

        [ForeignKey("TestQuestionId")]
        public TestQuestion TestQuestionFk { get; set; }
    }
}
