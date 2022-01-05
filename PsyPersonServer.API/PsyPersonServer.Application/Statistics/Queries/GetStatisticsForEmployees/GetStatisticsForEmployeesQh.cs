using MediatR;
using PsyPersonServer.Application.Statistics.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Statistics.Queries.GetStatisticsForEmployees
{
    public class GetStatisticsForEmployeesQh : IRequestHandler<GetStatisticsForEmployeesQ, StatisticDto>
    {
        public GetStatisticsForEmployeesQh(IUserTestRepository userTestRepository)
        {
            _userTestRepository = userTestRepository;
        }

        private readonly IUserTestRepository _userTestRepository;

        public async Task<StatisticDto> Handle(GetStatisticsForEmployeesQ request, CancellationToken cancellationToken)
        {

            var userTests = _userTestRepository.GetUserTestsByUserId(request.UserId).Result;
            
            throw new NotImplementedException();
        }
    }
}
