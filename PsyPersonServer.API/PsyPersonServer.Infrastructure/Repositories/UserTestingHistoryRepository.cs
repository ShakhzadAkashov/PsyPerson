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
    public class UserTestingHistoryRepository : IUserTestingHistoryRepository
    {
        public UserTestingHistoryRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly DBContext _dbContext;

        public async Task<UserTestingHistory> Create(double testScore, TestResultStatusEnum resultStatus, Guid userTestId, bool? isChecked)
        {
            var userTestingHistory = new UserTestingHistory
            {
                Id = new Guid(),
                TestScore = testScore,
                TestedDate = DateTime.Now,
                ResultStatus = resultStatus,
                UserTestId = userTestId,
                IsChecked = isChecked
            };

            await _dbContext.UserTestingHistories.AddAsync(userTestingHistory);
            await _dbContext.SaveChangesAsync();

            return userTestingHistory;
        }

        public async Task<bool> Update(Guid id,double testScore, TestResultStatusEnum resultStatus, bool isChecked)
        {
            var userTestingHistory = await _dbContext.UserTestingHistories.FirstOrDefaultAsync(x => x.Id == id);
            if (userTestingHistory != null)
            {
                userTestingHistory.ResultStatus = resultStatus;
                userTestingHistory.TestScore = testScore;
                userTestingHistory.IsChecked = isChecked;

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
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

        public async Task<TestingHistoryCustomQuestionAnswer> CreateTestingHistoryCustomQuestionAnswer(Guid userTestingHistoryId, string name, Guid testQuestionId)
        {
            var testingHistoryCustomQuestionAnswer = new TestingHistoryCustomQuestionAnswer
            {
                Id = new Guid(),
                Name = name,
                AnswerScore = 0,
                AnswerStatus = AnswerResultStatusEnum.Unknown,
                UserTestingHistoryId = userTestingHistoryId,
                TestQuestionId = testQuestionId
            };

            await _dbContext.TestingHistoryCustomQuestionAnswers.AddAsync(testingHistoryCustomQuestionAnswer);
            await _dbContext.SaveChangesAsync();

            return testingHistoryCustomQuestionAnswer;
        }

        public async Task<bool> UpdateTestingHistoryCustomQuestionAnswer(Guid id, double answerScore, AnswerResultStatusEnum? answerStatus)
        {
            var testingHistoryCustomQuestionAnswer = await _dbContext.TestingHistoryCustomQuestionAnswers.FirstOrDefaultAsync(x => x.Id == id);

            if (testingHistoryCustomQuestionAnswer != null)
            {
                testingHistoryCustomQuestionAnswer.AnswerScore = answerScore;
                testingHistoryCustomQuestionAnswer.AnswerStatus = answerStatus.Value;

                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<UserTestingHistory> GetById(Guid id)
        {
            var userTestingHistory = await _dbContext.UserTestingHistories
                .Include(x => x.UserTestFk)
                .ThenInclude(x => x.TestFk)
                .Where(x => x.Id == id).FirstOrDefaultAsync();

            return userTestingHistory;
        }

        public async Task<PagedResponse<UserTestingHistory>> GetUserTestingHistoryForCheck(int page, int itemPerPage, bool isChecked, string testName)
        {
            var userTestingHistoryList = _dbContext.UserTestingHistories
                .Include(x => x.UserTestFk).ThenInclude(x => x.TestFk)
                .Include(x => x.UserTestFk).ThenInclude(x => x.UserFk)
                .AsQueryable();

            if (isChecked == false)
            {
                userTestingHistoryList = userTestingHistoryList.Where(x => x.IsChecked == isChecked);
            }

            if (!string.IsNullOrEmpty(testName))
            {
                userTestingHistoryList = userTestingHistoryList.Where(x => x.UserTestFk.TestFk.Name.Contains(testName));
            }

            var total = await userTestingHistoryList.CountAsync();

            foreach (var i in userTestingHistoryList)
            {
                i.UserTestFk.UserTestingHistoryList = null;
            }

            return new PagedResponse<UserTestingHistory>(userTestingHistoryList
                .OrderBy(x => x.TestedDate)
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage), total);
        }

        public async Task<IEnumerable<TestingHistoryQuestionAnswer>> GetAnswersById(Guid userTestingHistoryId)
        {
            var list =  await _dbContext.TestingHistoryQuestionAnswers.Where(x => x.UserTestingHistoryId == userTestingHistoryId).AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<IEnumerable<TestingHistoryCustomQuestionAnswer>> GetCustomAnswersById(Guid userTestingHistoryId)
        {
            var list = await _dbContext.TestingHistoryCustomQuestionAnswers.Where(x => x.UserTestingHistoryId == userTestingHistoryId).AsNoTracking().ToListAsync();
            return list;
        }
    }
}
