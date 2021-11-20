using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Infrastructure.Repositories
{
    public class TestQuestionRepository : ITestQuestionRepository
    {
        public TestQuestionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly DBContext _dbContext;

        public async Task<PagedResponse<TestQuestion>> GetAll(int page, int itemPerPage)
        {
            var testQuestions = _dbContext.TestQuestions.Include(x => x.Answers).AsQueryable();

            var total = await testQuestions.CountAsync();

            return new PagedResponse<TestQuestion>(testQuestions
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page -1) * itemPerPage)
                .Take(itemPerPage),total);
        }
    }
}
