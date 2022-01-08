using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.Repositories
{
    public interface ITestRepository
    {
        Task<PagedResponse<Test>> GetTests(int page, int itemPerPage);
        Task<Test> Create(string name, string description, string imgPath,TestTypeEnum testType, List<TestResult> testResultList);
        Task<bool> Update(Guid id, string name, string description, string imgPath, List<TestResult> testResultList);
        Task<Test> GetTestById(Guid id);
        Task<int> GetAmountTestQuestionsById(Guid id);
        Task<PagedResponse<Test>> GetTestsByUserId(int page, int itemPerPage, string userId);
        Task<IEnumerable<Test>> GetAll();
        Task<bool> Remove(Guid id);
    }
}
