using MediatR;
using PsyPersonServer.Application.Statistics.Dtos;
using PsyPersonServer.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Statistics.Queries.GetUserTestingHistoryStatistics
{
    public class GetUserTestingHistoryStatisticsQ : IRequest<UserTestingReportDto>
    {
        public string UserId { get; set; }
    }
}
