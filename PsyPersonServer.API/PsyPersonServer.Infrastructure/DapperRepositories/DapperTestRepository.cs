using Dapper;
using PsyPersonServer.Domain.DapperRepositories;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Infrastructure.DapperRepositories
{
    public class DapperTestRepository : IDapperTestRepository
    {
        public DapperTestRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        private readonly DapperContext _dapperContext;

        public async Task<IEnumerable<Test>> GetTests()
        {
            var query = "SELECT * FROM TESTS";

            using (var connection = _dapperContext.CreateConnection())
            {
                var tests = await connection.QueryAsync<Test>(query);
                return tests.ToList();
            }
        }
    }
}
