using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Dtos
{
    public class TestingHistoryDto<T> : PagedResponse<T>
    {
        public TestingHistoryDto(IEnumerable<T> data, int total, string testName, double testScore, Guid testId) : base(data, total)
        {
            TestName = testName;
            TestScore = testScore;
            TestId = testId;
        }
        public string TestName { get; set; }
        public double TestScore { get; set; }
        public Guid TestId { get; set; }
    }

    public class TestingHistoryQuestionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TestingHistoryQuestionAnswerDto> Answers { get; set; }
        public TestingHistoryCustomQuestionAnswerDto CustomAnswer { get; set; }
    }

    public class TestingHistoryQuestionAnswerDto : TestQuestionAnswerDto 
    {
        public bool IsMarked { get; set; }
    }

    public class TestingHistoryCustomQuestionAnswerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double AnswerScore { get; set; }
        public AnswerResultStatusEnum AnswerStatus { get; set; }
        public Guid UserTestingHistoryId { get; set; }
        public Guid TestQuestionId { get; set; }
    }
}
