using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PsyPersonServer.Domain.Entities
{
    public class UserTest
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsTested { get; set; }
        public DateTime AssignedDate { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser UserFk {get;set;}
        public Guid TestId { get; set; }
        [ForeignKey("TestId")]
        public Test TestFk { get; set; }
        public List<UserTestingHistory> UserTestingHistoryList { get; set; }
    }
}
