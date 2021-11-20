using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.Repositories
{
    public interface ITestQuestionRepository
    {
        Task<PagedResponse<TestQuestion>> GetAll(int page, int itemPerPage);
    }
}
