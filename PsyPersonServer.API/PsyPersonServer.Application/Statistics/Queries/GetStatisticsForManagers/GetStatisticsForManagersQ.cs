using MediatR;
using PsyPersonServer.Application.Statistics.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Statistics.Queries.GetStatisticsForManager
{
    public class GetStatisticsForManagersQ : IRequest<List<StatisticDto>>
    {
    }
}
