using MediatR;
using PsyPersonServer.Application.Tests.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Tests.Commands.UpdateTest
{
    public class UpdateTestC : TestDto, IRequest<bool>
    {
    }
}
