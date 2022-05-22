using MediatR;
using PsyPersonServer.Application.Tests.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Tests.Queries.GetDapperTess
{
    public class GetDapperTestsQ : IRequest<IEnumerable<TestDto>>
    {

    }
}
