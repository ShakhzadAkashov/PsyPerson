using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.Repositories
{
    public interface IUserTestRepository
    {
        Task<IEnumerable<UserTest>> GetUserTestsByUserId(string userId);
    }
}
