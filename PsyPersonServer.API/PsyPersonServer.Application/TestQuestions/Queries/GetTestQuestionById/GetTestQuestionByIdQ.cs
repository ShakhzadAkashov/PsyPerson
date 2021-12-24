using MediatR;
using PsyPersonServer.Application.TestQuestions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.TestQuestions.Queries.GetTestQuestionById
{
    public class GetTestQuestionByIdQ : IRequest<TestQuestionDto>
    {
        public Guid Id { get; set; }
    }
}
