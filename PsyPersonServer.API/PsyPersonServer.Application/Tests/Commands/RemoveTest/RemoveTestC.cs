using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Tests.Commands.RemoveTest
{
    public class RemoveTestC : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
