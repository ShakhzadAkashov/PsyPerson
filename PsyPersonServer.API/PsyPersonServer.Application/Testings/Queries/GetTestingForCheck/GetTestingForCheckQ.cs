using MediatR;
using PsyPersonServer.Application.UserTests.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Queries.GetTestingForCheck
{
    public class GetTestingForCheckQ : IRequest<PagedResponse<UserTestingHistoryDto>>
    {
        public int Page { get; set; } = 1;
        public int ItemPerPage { get; set; } = 10;
        public bool IsChecked { get; set; } = false;
    }
}
