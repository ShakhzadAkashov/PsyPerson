using MediatR;
using PsyPersonServer.Application.TestQuestions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestions
{
    public class CreateTestQuestionsC : IRequest<bool>
    {
        public List<TestQuestionDto> TestQuestions { get; set; }
    }
}
