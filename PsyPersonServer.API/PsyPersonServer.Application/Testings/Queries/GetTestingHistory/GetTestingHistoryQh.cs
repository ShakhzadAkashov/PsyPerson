using MediatR;
using PsyPersonServer.Application.Testings.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Testings.Queries.GetTestingHistory
{
    public class GetTestingHistoryQh : IRequestHandler<GetTestingHistoryQ, TestingHistoryDto<TestingHistoryQuestionDto>>
    {
        public GetTestingHistoryQh(IUserTestingHistoryRepository userTestingHistoryRepository, ITestQuestionRepository testQuestionRepository)
        {
            _userTestingHistoryRepository = userTestingHistoryRepository;
            _testQuestionRepository = testQuestionRepository;
        }

        private readonly IUserTestingHistoryRepository _userTestingHistoryRepository;
        private readonly ITestQuestionRepository _testQuestionRepository;
        public async Task<TestingHistoryDto<TestingHistoryQuestionDto>> Handle(GetTestingHistoryQ request, CancellationToken cancellationToken)
        {
            var userTestingHistory = await _userTestingHistoryRepository.GetById(request.UserTestingHistoryId);
            var testQuestions = await _testQuestionRepository.GetAll(request.Page, request.ItemPerPage, userTestingHistory.UserTestFk.TestId);
            var answers = await _userTestingHistoryRepository.GetAnswersById(request.UserTestingHistoryId);

            var testQuestionDtos = new List<TestingHistoryQuestionDto>();

            foreach (var i in testQuestions.Data)
            {
                var question = new TestingHistoryQuestionDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Answers = new List<TestingHistoryQuestionAnswerDto>()
                };

                foreach (var j in i.Answers)
                {
                    var answer = new TestingHistoryQuestionAnswerDto
                    {
                        Id = j.Id,
                        Name = j.Name,
                        IsCorrect = j.IsCorrect,
                        TestQuestionId = j.TestQuestionId,
                        IdForView = j.IdForView,
                        IsMarked = false
                    };

                    foreach (var k in answers)
                    {
                        if (j.Id == k.AnswerId && k.IsMarked == true)
                        {
                            answer.IsMarked = true;
                        }
                    }

                    question.Answers.Add(answer);
                }

                testQuestionDtos.Add(question);
            }

            return new TestingHistoryDto<TestingHistoryQuestionDto>(testQuestionDtos, testQuestions.Total, userTestingHistory.UserTestFk.TestFk.Name, userTestingHistory.TestScore);
        }
    }
}
