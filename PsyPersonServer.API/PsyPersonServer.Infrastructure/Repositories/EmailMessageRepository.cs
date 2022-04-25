using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Infrastructure.Repositories
{
    public class EmailMessageRepository : IEmailMessageRepository
    {
        public EmailMessageRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private readonly DBContext _dbContext;

        public async Task<EmailMessageSetting> CreateSetting(string hostName, string senderAddress, string senderPswd, string messageDisplayName)
        {
            var setting = new EmailMessageSetting
            {
                Id = new Guid(),
                HostName = hostName,
                SenderAddress = senderAddress,
                SenderPswd = senderAddress,
                MessageDisplayName = messageDisplayName
            };

            await _dbContext.EmailMessageSettings.AddAsync(setting);
            await _dbContext.SaveChangesAsync();

            return setting;
        }

        public async Task<EmailMessageSetting> GetSetting()
        {
            var setting = await _dbContext.EmailMessageSettings.FirstOrDefaultAsync();
            if(setting != null)
                return setting;
            return new EmailMessageSetting();
        }

        public async Task<bool> UpdateSetting(string hostName, string senderAddress, string senderPswd, string messageDisplayName)
        {
            var setting = await _dbContext.EmailMessageSettings.FirstOrDefaultAsync();

            if (setting != null)
            {
                setting.HostName = hostName;
                setting.SenderAddress = senderAddress;
                setting.SenderPswd = senderPswd;
                setting.MessageDisplayName = messageDisplayName;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveSetting(Guid id)
        {
            var emailMessageSetting = await _dbContext.EmailMessageSettings.FirstOrDefaultAsync(x => x.Id == id);

            if (emailMessageSetting != null)
            {
                _dbContext.EmailMessageSettings.Remove(emailMessageSetting);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
