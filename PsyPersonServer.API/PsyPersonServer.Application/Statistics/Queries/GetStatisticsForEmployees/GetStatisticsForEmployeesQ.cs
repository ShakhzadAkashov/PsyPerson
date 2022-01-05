using MediatR;
using PsyPersonServer.Application.Statistics.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Statistics.Queries.GetStatisticsForEmployees
{
    public class GetStatisticsForEmployeesQ : IRequest<StatisticDto>
    {
        public string UserId { get; set; }
    }
}
