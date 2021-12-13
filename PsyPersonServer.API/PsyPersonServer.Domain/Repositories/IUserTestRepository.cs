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
    }
}
