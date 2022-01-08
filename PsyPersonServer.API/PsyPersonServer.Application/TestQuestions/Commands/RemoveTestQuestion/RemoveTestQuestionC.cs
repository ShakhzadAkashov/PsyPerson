using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.TestQuestions.Commands.RemoveTestQuestion
{
    public class RemoveTestQuestionC : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
