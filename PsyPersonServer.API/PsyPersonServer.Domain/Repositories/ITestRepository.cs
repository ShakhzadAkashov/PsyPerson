using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.Repositories
{
    public interface ITestRepository
    {
        Task<PagedResponse<Test>> GetTests(int page, int itemPerPage);
        Task<Test> Create(string name, string description, string imgPath);
        Task<bool> Update(Guid id, string name, string description, string imgPath);
        Task<Test> GetTestById(Guid id);
    }
}
