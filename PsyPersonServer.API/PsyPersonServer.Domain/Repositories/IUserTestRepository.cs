using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.Repositories
{
    public interface IUserTestRepository
    {
        Task<IEnumerable<UserTest>> GetUserTestsByUserId(string userId);
        Task<PagedResponse<UserTest>> GetUserTests(int page, int itemPerPage, string userId);
        Task<UserTest> Create(string userId, Guid testId);
        Task<UserTest> GetUserTest(string userId, Guid testId);
        Task<bool> Update(Guid id, bool isActive, bool isTested, DateTime assignedDate);
        Task<IEnumerable<UserTest>> GetUserTestsByTestId(Guid testId);
    }
}
