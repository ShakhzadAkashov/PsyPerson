using MediatR;
using PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestion;
using PsyPersonServer.Application.TestQuestions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestions
{
    public class CreateTestQuestionsCh : IRequestHandler<CreateTestQuestionsC, bool>
    {
        public CreateTestQuestionsCh(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        public async Task<bool> Handle(CreateTestQuestionsC request, CancellationToken cancellationToken)
        {
            foreach (var i in request.TestQuestions)
            {
                await _mediator.Send(new CreateTestQuestionC 
                {
                    Name = i.Name,
                    TestId = i.TestId,
                    Answers = new List<TestQuestionAnswerDto>()
                });
            }

            return true;
        }
    }
}
