using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.UserTests.Commands.ReAssignTestForUser
{
    public class ReAssignTestForUserC : IRequest<bool>
    {
        public Guid UserTestId { get; set; }
    }
}
