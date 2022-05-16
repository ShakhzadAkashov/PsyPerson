using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PsyPersonServer.Domain.Models.EmailMessage;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.EmailMessage.Commands.SendEmailMessage
{
    public class SendEmailMessageCh : IRequestHandler<SendEmailMessageC, bool>
    {
        private readonly EmailMessageSettings _emailMessageSettings;
        private readonly ILogger<SendEmailMessageCh> _logger;
        private IEmailMessageRepository _emailMessageRepository;
        public SendEmailMessageCh(IOptions<EmailMessageSettings> emailMessageSettings, ILogger<SendEmailMessageCh> logger, IEmailMessageRepository emailMessageRepository)
        {
            _emailMessageSettings = emailMessageSettings.Value;
            _logger = logger;
            _emailMessageRepository = emailMessageRepository;
        }
        public Task<bool> Handle(SendEmailMessageC request, CancellationToken cancellationToken)
        {
            var emailMessageSettings = _emailMessageRepository.GetSetting().Result;

            if (emailMessageSettings == null)
                throw new ApplicationException("Cannot settting email message sender, email message settings not found!");

            try
            {
                SmtpClient mySmtpClient = new SmtpClient(emailMessageSettings.HostName, 25);

                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.EnableSsl = true;

                NetworkCredential basicAuthenticationInfo = new NetworkCredential(emailMessageSettings.SenderAddress, emailMessageSettings.SenderPswd);
                mySmtpClient.Credentials = basicAuthenticationInfo;

                MailAddress from = new MailAddress(emailMessageSettings.SenderAddress, emailMessageSettings.MessageDisplayName);
                MailAddress to = new MailAddress(request.ReceiverMailAddress, request.ReceiverFullName);
                MailMessage myMail = new MailMessage(from, to);

                MailAddress replyTo = new MailAddress(emailMessageSettings.SenderAddress);
                myMail.ReplyToList.Add(replyTo);

                myMail.Subject = request.LetterHeader;
                myMail.SubjectEncoding = Encoding.UTF8;

                myMail.Body = request.EmailMessage;
                if (request.IsHTML == false)
                    myMail.BodyEncoding = Encoding.UTF8;
                myMail.IsBodyHtml = request.IsHTML;

                mySmtpClient.Send(myMail);

                return Task.FromResult(true);
            }
            catch (SmtpException ex)
            {
                _logger.LogError($"Send Email Message Failed. {ex.Message}", ex);
                throw new ApplicationException("SmtpException " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Send Email Message Failed. {ex.Message}", ex);
                throw ex;
            }
        }
    }
}
