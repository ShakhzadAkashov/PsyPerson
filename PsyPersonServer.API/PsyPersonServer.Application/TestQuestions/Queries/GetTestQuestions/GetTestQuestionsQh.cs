using AutoMapper;
using MediatR;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.TestQuestions.Queries.GetTestQuestions
{
    public class GetTestQuestionsQh : IRequestHandler<GetTestQuestionsQ, PagedResponse<TestQuestionDto>>
    {
        public GetTestQuestionsQh(IMapper mapper, ITestQuestionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private readonly IMapper _mapper;
        private readonly ITestQuestionRepository _repository;

        public async Task<PagedResponse<TestQuestionDto>> Handle(GetTestQuestionsQ request, CancellationToken cancellationToken)
        {
            var testQuestions =  await _repository.GetAll(request.Page, request.ItemPerPage);
            return new PagedResponse<TestQuestionDto>(testQuestions.Data.Select(x => _mapper.Map<TestQuestionDto>(x.Answers.Select(x => _mapper.Map<TestQuestionAnswerDto>(x)))),testQuestions.Total);
        }
    }
}
