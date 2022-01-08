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
    public class UserTestRepository : IUserTestRepository
    {
        public UserTestRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly DBContext _dbContext;

        public Task<IEnumerable<UserTest>> GetUserTestsByUserId(string userId)
        {
            var userTests = _dbContext.UserTests.Where(x => x.UserId == userId && x.IsActive == true);
            return Task.FromResult<IEnumerable<UserTest>>(userTests);
        }

        public async Task<PagedResponse<UserTest>> GetUserTests(int page, int itemPerPage, string userId)
        {
            var userTests1 = _dbContext.UserTests
                .Include(x => x.TestFk).Where(x => x.UserId == userId && x.IsActive == true);

            var total = await userTests1.CountAsync();

            var u = userTests1
                .OrderByDescending(x => x.AssignedDate)
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage);

            var userTests = await u.ToListAsync();

            foreach (var i in userTests)
            {
                var list = await _dbContext.UserTestingHistories.Where(x => x.UserTestId == i.Id).ToListAsync();
                i.UserTestingHistoryList = new List<UserTestingHistory>();

                foreach (var j in list)
                {
                    var h = new UserTestingHistory
                    {
                        Id = j.Id,
                        TestScore = j.TestScore,
                        TestedDate = j.TestedDate,
                        ResultStatus = j.ResultStatus,
                        UserTestId = j.UserTestId,
                        IsChecked = j.IsChecked
                    };

                    i.UserTestingHistoryList.Add(h);
                }
            }

            return new PagedResponse<UserTest>(userTests, total);
        }

        public async Task<UserTest> GetUserTest(string userId, Guid testId)
        {
            var userTests = await _dbContext.UserTests.FirstOrDefaultAsync(x => x.UserId == userId && x.TestId == testId);
            if(userTests != null)
                return userTests;
            return new UserTest();
        }

        public async Task<UserTest> Create(string userId, Guid testId)
        {
            var userTest = await _dbContext.UserTests.FirstOrDefaultAsync(x => x.UserId == userId && x.TestId == testId);

            if (userTest == null)
            {
                userTest = new UserTest
                {
                    Id = new Guid(),
                    IsActive = true,
                    IsTested = false,
                    UserId = userId,
                    TestId = testId,
                    AssignedDate = DateTime.Now
                };

                await _dbContext.UserTests.AddAsync(userTest);
            }
            else 
            {
                userTest.IsActive = true;
                userTest.IsTested = false;
                userTest.AssignedDate = DateTime.Now;
            }
            
            await _dbContext.SaveChangesAsync();
            return userTest;
        }

        public async Task<bool> Update(Guid id, bool isActive, bool isTested, DateTime assignedDate)
        {
            var userTest = await _dbContext.UserTests.FirstOrDefaultAsync(x => x.Id == id);

            if (userTest != null)
            {
                userTest.IsActive = isActive;
                userTest.IsTested = isTested;
                userTest.AssignedDate = assignedDate;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public Task<IEnumerable<UserTest>> GetUserTestsByTestId(Guid testId)
        {
            var userTests = _dbContext.UserTests.Where(x => x.TestId == testId);
            return Task.FromResult<IEnumerable<UserTest>>(userTests);
        }

        public async Task<UserTest> GetById(Guid id)
        {
            var userTest = await _dbContext.UserTests.FirstOrDefaultAsync(x => x.Id == id);
            return userTest;
        }
    }
}
