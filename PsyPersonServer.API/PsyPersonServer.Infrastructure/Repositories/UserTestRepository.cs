using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Infrastructure.Repositories
{
    public class UserTestRepository : IUserTestRepository
    {
        public UserTestRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly DBContext _dbContext;

        public Task<IEnumerable<UserTest>> GetUserTestsByUserId(string userId)
        {
            var userTests = _dbContext.UserTests.Where(x => x.UserId == userId);
            return Task.FromResult<IEnumerable<UserTest>>(userTests);
        }
    }
}
