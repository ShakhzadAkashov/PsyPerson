using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.Tests;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Infrastructure.Repositories
{
    public class UserTestingHistoryRepository : IUserTestingHistoryRepository
    {
        public UserTestingHistoryRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly DBContext _dbContext;

        public async Task<UserTestingHistory> Create(double testScore, TestResultStatusEnum resultStatus, Guid userTestId)
        {
            var userTestingHistory = new UserTestingHistory
            {
                Id = new Guid(),
                TestScore = testScore,
                TestedDate = DateTime.Now,
                ResultStatus = resultStatus,
                UserTestId = userTestId
            };

            await _dbContext.UserTestingHistories.AddAsync(userTestingHistory);
            await _dbContext.SaveChangesAsync();

            return userTestingHistory;
        }

        public async Task<TestingHistoryQuestionAnswer> CreateTestingHistoryQuestionAnswer(bool isMarked, Guid answerId, Guid userTestingHistoryId)
        {
            var testingHistoryQuestionAnswer = new TestingHistoryQuestionAnswer
            {
                Id = new Guid(),
                IsMarked = isMarked,
                AnswerId = answerId,
                UserTestingHistoryId = userTestingHistoryId
            };

            await _dbContext.TestingHistoryQuestionAnswers.AddAsync(testingHistoryQuestionAnswer);
            await _dbContext.SaveChangesAsync();

            return testingHistoryQuestionAnswer;
        }

        public async Task<UserTestingHistory> GetById(Guid id)
        {
            var userTestingHistory = await _dbContext.UserTestingHistories
                .Include(x => x.UserTestFk)
                .ThenInclude(x => x.TestFk)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return userTestingHistory;
        }

        public async Task<IEnumerable<TestingHistoryQuestionAnswer>> GetAnswersById(Guid userTestingHistoryId)
        {
            var list =  await _dbContext.TestingHistoryQuestionAnswers.Where(x => x.UserTestingHistoryId == userTestingHistoryId).AsNoTracking().ToListAsync();
            return list;
        }
    }
}
