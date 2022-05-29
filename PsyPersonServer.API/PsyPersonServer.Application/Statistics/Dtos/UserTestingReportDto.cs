using PsyPersonServer.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Statistics.Dtos
{
    public class UserTestingReportDto
    {
        public StatisticDto Statistic { get; set; }
        public List<UserTestingHistDescriptionsDto> TestingDescriptions { get; set; }
    }
}
