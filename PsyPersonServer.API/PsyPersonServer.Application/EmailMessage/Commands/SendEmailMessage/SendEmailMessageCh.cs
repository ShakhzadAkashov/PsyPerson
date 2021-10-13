using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PsyPersonServer.Domain.Models.EmailMessage;
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
        public SendEmailMessageCh(IOptions<EmailMessageSettings> emailMessageSettings, ILogger<SendEmailMessageCh> logger)
        {
            _emailMessageSettings = emailMessageSettings.Value;
            _logger = logger;
        }
        public Task<bool> Handle(SendEmailMessageC request, CancellationToken cancellationToken)
        {
            try
            {
                SmtpClient mySmtpClient = new SmtpClient(_emailMessageSettings.SenderAddress, 25);

                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.EnableSsl = true;

                NetworkCredential basicAuthenticationInfo = new NetworkCredential(_emailMessageSettings.SenderAddress, _emailMessageSettings.SenderPswd);
                mySmtpClient.Credentials = basicAuthenticationInfo;

                MailAddress from = new MailAddress(_emailMessageSettings.SenderAddress, _emailMessageSettings.MessageDisplayName);
                MailAddress to = new MailAddress(request.ReceiverMailAddress, request.ReceiverFullName);
                MailMessage myMail = new MailMessage(from, to);

                MailAddress replyTo = new MailAddress(_emailMessageSettings.SenderAddress);
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
