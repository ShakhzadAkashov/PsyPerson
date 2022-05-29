using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Dtos
{
    public class UserTestingHistoryStatisticsDto
    {
        public Guid UserTestId { get; set; }
        public string UserId { get; set; }
        public Guid TestId { get; set; }
        public string TestName { get; set; }
        public DateTime TestedDate { get; set; }
        public int TestingCount {get;set;}
    }
}
