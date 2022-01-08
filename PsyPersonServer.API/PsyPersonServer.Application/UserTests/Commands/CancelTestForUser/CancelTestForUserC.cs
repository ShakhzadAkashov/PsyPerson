using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.UserTests.Commands.CancelTestForUser
{
    public class CancelTestForUserC : IRequest<bool>
    {
        public Guid UserTestId { get; set; }
    }
}
