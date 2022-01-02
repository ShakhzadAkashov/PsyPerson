using MediatR;
using PsyPersonServer.Application.Testings.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Testings.Commands
{
    public class CheckSecondLevelDifficultTypeTestingC : IRequest<CheckTestingResponseDto>
    {
        public TestForTestingDto TestForTesting { get; set; }
        public string UserId { get; set; }
        public bool? IsChecked { get; set; }
        public Guid UserTestingHistoryId { get; set; }
    }
}
