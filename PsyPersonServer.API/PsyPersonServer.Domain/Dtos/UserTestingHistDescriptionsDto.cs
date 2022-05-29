using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Dtos
{
    public class UserTestingHistDescriptionsDto
    {
        public string TestName { get; set; }
        public Guid UserTestId { get; set; }
        public List<UserTestingHistDto> HistoryDesriptionList { get; set; }
    }
}
