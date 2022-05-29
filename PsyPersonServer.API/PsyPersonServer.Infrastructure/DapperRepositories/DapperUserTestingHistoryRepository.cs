using Dapper;
using PsyPersonServer.Domain.DapperRepositories;
using PsyPersonServer.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Infrastructure.DapperRepositories
{
    public class DapperUserTestingHistoryRepository : IDapperUserTestingHistoryRepository
    {
        public DapperUserTestingHistoryRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        private readonly DapperContext _dapperContext;
        public async Task<IEnumerable<UserTestingHistoryStatisticsDto>> GetUserTestingHistoryStatistics(string userId)
        
        {
            var query = @"select us.Id as UserTestId,t.Name as TestName,us.UserId as UserId,us.TestId as TestId, hist.TestedDate as TestedDate, Count(*) as TestingCount
                            from UserTestingHistories as hist left join UserTests as us on us.Id = hist.UserTestId
                            left join Tests t on t.Id = us.TestId
                            group by us.Id, hist.TestedDate,t.Name, us.UserId, us.TestId  having us.UserId = @UserId;";

            using (var connection = _dapperContext.CreateConnection())
            {
                var hist = await connection.QueryAsync<UserTestingHistoryStatisticsDto>(query, new { UserId = userId});
                return hist.ToList();
            }
        }

        public async Task<IEnumerable<UserTestingHistDto>> GetUserTestingHistByUserTestId(Guid userTestId)
        {
            var query = @"select t.name as TestName, hist.TestedDate as TestedDate, hist.TestScore as TestScore, hist.ResultStatus as ResultStatus 
                            from UserTests us 
                            left join UserTestingHistories hist on us.Id = hist.UserTestId
                            left join Tests t on t.Id = us.TestId
                            where us.Id = @UserTestId;";

            using (var connection = _dapperContext.CreateConnection())
            {
                var hist = await connection.QueryAsync<UserTestingHistDto>(query, new { UserTestId = userTestId });
                return hist.ToList();
            }
        }
    }
}
