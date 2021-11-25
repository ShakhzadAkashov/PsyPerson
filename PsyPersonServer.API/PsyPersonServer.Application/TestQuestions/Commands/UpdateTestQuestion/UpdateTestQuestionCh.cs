using AutoMapper;
using MediatR;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.TestQuestions.Commands.UpdateTestQuestion
{
    public class UpdateTestQuestionCh : IRequestHandler<UpdateTestQuestionC, bool>
    {
        public UpdateTestQuestionCh(ITestQuestionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly ITestQuestionRepository _repository;
        private readonly IMapper _mapper;

        public async Task<bool> Handle(UpdateTestQuestionC request, CancellationToken cancellationToken)
        {
            var answers = request.Answers.Select(x => _mapper.Map<TestQuestionAnswer>(x)).ToList();
            return await _repository.Update(request.Id,request.Name,answers);
        }
    }
}
