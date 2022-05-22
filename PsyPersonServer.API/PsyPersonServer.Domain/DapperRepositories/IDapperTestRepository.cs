using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.DapperRepositories
{
    public interface IDapperTestRepository
    {
        Task<IEnumerable<Test>> GetTests();
    }
}
