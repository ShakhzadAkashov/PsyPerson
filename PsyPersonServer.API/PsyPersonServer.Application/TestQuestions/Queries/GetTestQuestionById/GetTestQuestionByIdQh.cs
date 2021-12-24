using AutoMapper;
using MediatR;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.TestQuestions.Queries.GetTestQuestionById
{
    public class GetTestQuestionByIdQh : IRequestHandler<GetTestQuestionByIdQ, TestQuestionDto>
    {
        public GetTestQuestionByIdQh(ITestQuestionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly ITestQuestionRepository _repository;
        private readonly IMapper _mapper;
        public async  Task<TestQuestionDto> Handle(GetTestQuestionByIdQ request, CancellationToken cancellationToken)
        {
            var testQuestion = await _repository.GetById(request.Id);
            return _mapper.Map<TestQuestionDto>(testQuestion);
        }
    }
}
