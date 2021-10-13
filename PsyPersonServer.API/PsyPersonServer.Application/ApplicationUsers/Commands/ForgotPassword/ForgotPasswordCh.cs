using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using PsyPersonServer.Application.EmailMessage.Commands.SendEmailMessage;
using PsyPersonServer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.ApplicationUsers.Commands.ForgotPassword
{
    public class ForgotPasswordCh : IRequestHandler<ForgotPasswordC, bool>
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;
        public ForgotPasswordCh(UserManager<ApplicationUser> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }
        public async Task<bool> Handle(ForgotPasswordC request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("User Not Founded");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>
            {
                { "token", token},
                { "email", request.Email}
            };

            var callback = QueryHelpers.AddQueryString(request.ClientURI, param);

            string message = string.Format("<h2 style='color:red;'>{0}</h2>", callback);
            string firstName = user.FirstName ?? "";
            string lastName = user.LastName ?? "";
            string patronymic = user.Patronymic ?? "";
            string fullName = firstName + " " + lastName + " " + patronymic;
            string letterHeader = "Ссылка для сброса пароля";

            var res = await _mediator.Send(new SendEmailMessageC
            {
                ReceiverMailAddress = request.Email,
                EmailMessage = message,
                ReceiverFullName = fullName,
                LetterHeader = letterHeader,
                IsHTML = true 
            });

            return res;
        }
    }
}
