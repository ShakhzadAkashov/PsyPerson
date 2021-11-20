using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.Repositories
{
    public interface ITestQuestionRepository
    {
        Task<PagedResponse<TestQuestion>> GetAll(int page, int itemPerPage);
        Task<TestQuestion> Create(string name, TestQuestionEnum questionType, Guid testId, List<TestQuestionAnswer> answers);
    }
}
