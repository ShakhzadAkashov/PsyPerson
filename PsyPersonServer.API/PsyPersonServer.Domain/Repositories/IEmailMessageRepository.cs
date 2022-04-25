using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.Repositories
{
    public interface IEmailMessageRepository
    {
        Task<EmailMessageSetting> GetSetting();
        Task<EmailMessageSetting> CreateSetting(string hostName, string senderAddress, string senderPswd, string messageDisplayName);
        Task<bool> UpdateSetting(string hostName, string senderAddress, string senderPswd, string messageDisplayName);
        Task<bool> RemoveSetting(Guid id);
    }
}