using MediatR;
using PsyPersonServer.Application.TestQuestions.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.TestQuestions.Queries.GetTestQuestions
{
    public class GetTestQuestionsQ : IRequest<PagedResponse<TestQuestionDto>>
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
    }
}
