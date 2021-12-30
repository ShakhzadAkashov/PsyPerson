using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.Repositories
{
    public interface IUserTestingHistoryRepository
    {
        Task<UserTestingHistory> Create(double testScore, TestResultStatusEnum resultStatus, Guid userTestId, bool? isChecked);
        Task<TestingHistoryQuestionAnswer> CreateTestingHistoryQuestionAnswer(bool isMarked, Guid answerId, Guid userTestingHistoryId);
        Task<UserTestingHistory> GetById(Guid id);
        Task<IEnumerable<TestingHistoryQuestionAnswer>> GetAnswersById(Guid userTestingHistoryId);
        Task<TestingHistoryCustomQuestionAnswer> CreateTestingHistoryCustomQuestionAnswer(Guid userTestingHistoryId, string name);
        Task<PagedResponse<UserTestingHistory>> GetUserTestingHistoryForCheck(int page, int itemPerPage, bool isChecked);
    }
}
