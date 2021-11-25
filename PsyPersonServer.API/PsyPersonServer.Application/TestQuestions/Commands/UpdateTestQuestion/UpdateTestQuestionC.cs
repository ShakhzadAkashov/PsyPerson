using MediatR;
using PsyPersonServer.Application.TestQuestions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.TestQuestions.Commands.UpdateTestQuestion
{
    public class UpdateTestQuestionC : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TestQuestionAnswerDto> Answers { get; set; }
    }
}
