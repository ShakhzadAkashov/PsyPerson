using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PsyPersonServer.Domain.Entities
{
    public class UserTestingHistory
    {
        public Guid Id { get; set; }
        public double TestScore { get; set; }
        public DateTime TestedDate { get; set; }
        public TestResultStatusEnum ResultStatus { get; set; }
        public Guid UserTestId { get; set; }
        [ForeignKey("UserTestId")]
        public UserTest UserTestFk { get; set; }
    }
}
