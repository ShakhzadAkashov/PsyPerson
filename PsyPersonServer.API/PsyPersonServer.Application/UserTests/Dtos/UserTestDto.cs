using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.UserTests.Dtos
{
    public class UserTestDto
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsTested { get; set; }
        public DateTime AssignedDate { get; set; }
        public string UserId { get; set; }
        public Guid TestId { get; set; }
        public Test Test { get; set; }
        public List<UserTestingHistoryDto> UserTestingHistoryList { get; set; }
        public UserTestingHistoryDto LastUserTestingHistoryDto { get; set; }
    }
}
