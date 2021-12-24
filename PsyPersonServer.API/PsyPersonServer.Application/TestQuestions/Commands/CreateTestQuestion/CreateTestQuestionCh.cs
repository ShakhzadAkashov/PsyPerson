using AutoMapper;
using MediatR;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestion
{
    public class CreateTestQuestionCh : IRequestHandler<CreateTestQuestionC, TestQuestionDto>
    {
        public CreateTestQuestionCh(IMapper mapper, ITestQuestionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private readonly IMapper _mapper;
        private readonly ITestQuestionRepository _repository;

        public async Task<TestQuestionDto> Handle(CreateTestQuestionC request, CancellationToken cancellationToken)
        {
            var answers = request.Answers.Select(x => _mapper.Map<TestQuestionAnswer>(x)).ToList();
            var question = await _repository.Create(request.Name, request.TestId, answers);

            return _mapper.Map<TestQuestionDto>(question);
        }
    }
}
