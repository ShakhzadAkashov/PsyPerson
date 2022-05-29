using PsyPersonServer.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.DapperRepositories
{
    public interface IDapperUserTestingHistoryRepository
    {
        Task<IEnumerable<UserTestingHistDto>> GetUserTestingHistByUserTestId(Guid userTestId);
        Task<IEnumerable<UserTestingHistoryStatisticsDto>> GetUserTestingHistoryStatistics(string userId);
    }
}
