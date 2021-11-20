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
            var testQuestions =  await _repository.GetAll(request.Page, request.ItemPerPage, request.TestId);
            var testQuestionDtos = testQuestions.Data.Select(x => _mapper.Map<TestQuestionDto>(x)).ToList();

            foreach (var i in testQuestionDtos) 
            {
                i.AmountCorrectAnswers = i.Answers.Count(x => x.IsCorrect == true);
            }

            return new PagedResponse<TestQuestionDto>(testQuestionDtos,testQuestions.Total);
        }
    }
}
