using MediatR;
using PsyPersonServer.Application.Testings.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Queries.GetTestingHistory
{
    public class GetTestingHistoryQ : IRequest<TestingHistoryDto<TestingHistoryQuestionDto>>
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
        public Guid UserTestingHistoryId { get; set; }
    }
}
