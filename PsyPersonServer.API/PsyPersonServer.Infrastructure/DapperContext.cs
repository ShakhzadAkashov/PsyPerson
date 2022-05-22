using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PsyPersonServer.Infrastructure
{
    public class DapperContext
    {
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DbConnection");
        }

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
