using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.Repositories
{
    public interface IUserTestingHistoryRepository
    {
        Task<UserTestingHistory> Create(double testScore, TestResultStatusEnum resultStatus, Guid userTestId);
        Task<TestingHistoryQuestionAnswer> CreateTestingHistoryQuestionAnswer(bool isMarked, Guid answerId, Guid userTestingHistoryId);
        Task<UserTestingHistory> GetById(Guid id);
        Task<IEnumerable<TestingHistoryQuestionAnswer>> GetAnswersById(Guid userTestingHistoryId);
    }
}
