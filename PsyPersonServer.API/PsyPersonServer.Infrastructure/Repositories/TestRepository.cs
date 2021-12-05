using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Models.Tests;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Infrastructure.Repositories
{
    public class TestRepository : ITestRepository
    {
        public TestRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private readonly DBContext _dbContext;

        public async Task<PagedResponse<Test>> GetTests(int page, int itemPerPage)
        {
            var tests = _dbContext.Tests.AsQueryable();

            var total = await tests.CountAsync();

            return new PagedResponse<Test>(tests
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage), total);
        }

        public async Task<Test> GetTestById(Guid id)
        {
            var test = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);
            return test;
        }

        public async Task<int> GetAmountTestQuestionsById(Guid id)
        {
            var amount = await _dbContext.TestQuestions.CountAsync(x => x.TestId == id);
            return amount;
        }

        public async Task<Test> Create(string name, string description, string imgPath, TestTypeEnum testType)
        {
            var test = new Test
            {
                Id = new Guid(),
                Name = name,
                Description = description,
                ImgPath = imgPath,
                CreatedDate = DateTime.Now,
                TestType = testType
            };

            await _dbContext.Tests.AddAsync(test);
            await _dbContext.SaveChangesAsync();
            return test;
        }

        public async Task<bool> Update(Guid id, string name, string description, string imgPath)
        {
            var test = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);

            if (test != null)
            {
                test.Name = name;
                test.Description = description;
                test.ImgPath = imgPath;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
