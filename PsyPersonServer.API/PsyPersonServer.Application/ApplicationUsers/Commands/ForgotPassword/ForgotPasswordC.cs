using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PsyPersonServer.Application.ApplicationUsers.Commands.ForgotPassword
{
    public class ForgotPasswordC : IRequest<bool>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ClientURI { get; set; }
    }
}
