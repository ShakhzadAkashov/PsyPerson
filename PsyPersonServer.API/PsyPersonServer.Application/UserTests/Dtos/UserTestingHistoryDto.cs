using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.UserTests.Dtos
{
    public class UserTestingHistoryDto
    {
        public Guid Id { get; set; }
        public double TestScore { get; set; }
        public TestResultStatusEnum ResultStatus { get; set; }
        public DateTime TestedDate { get; set; }
        public Guid UserTestId { get; set; }
        public UserTestDto UserTest { get; set; }
        public bool? IsChecked { get; set; }
    }
}
