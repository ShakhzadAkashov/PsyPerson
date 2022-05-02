using MediatR;
using PsyPersonServer.Domain.Models.Tests;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.UserTests.Commands.CountStatus
{
    public class CountStatusCh : IRequestHandler<CountStatusC, TestResultStatusEnum>
    {
        public async Task<TestResultStatusEnum> Handle(CountStatusC request, CancellationToken cancellationToken)
        {
            var testScoreSum = request.TestScoreList.Sum();

            TestResultStatusEnum status = TestResultStatusEnum.Unknown;

            foreach (var item in ResultList.Results)
            {
                if (item.RangeFrom <= Convert.ToDouble(testScoreSum) && item.RangeTo >= Convert.ToDouble(testScoreSum))
                {
                    status = item.Status;
                    return status;
                }
            }

            return status;
        }
    }

    public static class ResultList
    {
        public static List<Results> Results = new List<Results>()
        {
            new Results { RangeFrom = 0, RangeTo = 30, Status = TestResultStatusEnum.Low },
            new Results { RangeFrom = 31, RangeTo = 60, Status = TestResultStatusEnum.Satisfactory },
            new Results { RangeFrom = 61, RangeTo = 90, Status = TestResultStatusEnum.Good },
            new Results { RangeFrom = 91, RangeTo = 100, Status = TestResultStatusEnum.Excelent }
        };
    }
    public class Results
    {
        public double RangeFrom { get; set; }
        public double RangeTo { get; set; }
        public TestResultStatusEnum Status { get; set; }
    }
}
