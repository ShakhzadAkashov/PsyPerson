using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Dtos
{
    public class TestingHistoryDto<T> : PagedResponse<T>
    {
        public TestingHistoryDto(IEnumerable<T> data, int total, string testName, double testScore) : base(data, total)
        {
            TestName = testName;
            TestScore = testScore;
        }
        public string TestName { get; set; }
        public double TestScore { get; set; }
    }

    public class TestingHistoryQuestionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TestingHistoryQuestionAnswerDto> Answers { get; set; }
    }

    public class TestingHistoryQuestionAnswerDto : TestQuestionAnswerDto 
    {
        public bool IsMarked { get; set; }
    }
}
