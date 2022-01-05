using MediatR;
using PsyPersonServer.Application.Statistics.Dtos;
using PsyPersonServer.Application.Statistics.Queries.GetStatisticsForManager;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Statistics.Queries.GetStatisticsForManagers
{
    public class GetStatisticsForManagersQh : IRequestHandler<GetStatisticsForManagersQ, StatisticDto>
    {
        public GetStatisticsForManagersQh(ITestRepository testRepository, IUserTestRepository userTestRepository)
        {
            _testRepository = testRepository;
            _userTestRepository = userTestRepository;
        }

        private readonly ITestRepository _testRepository;
        private readonly IUserTestRepository _userTestRepository;

        public async Task<StatisticDto> Handle(GetStatisticsForManagersQ request, CancellationToken cancellationToken)
        {
            var statistics = new StatisticDto();
            statistics.Data = new ChartDto();
            statistics.Data.Labels = new List<string>();
            statistics.Data.Datasets = new List<Dataset>();
            
            var all = new Dataset
            {
                Label = "Количество Назначенных",
                BackgroundColor = "#42A5F5",
                Data = new List<int>()
            };

            var tested = new Dataset
            {
                Label = "Количество пройденных",
                BackgroundColor = "#EC407A",
                Data = new List<int>()
            };

            var peding = new Dataset
            {
                Label = "Количество не пройденных",
                BackgroundColor = "#FFA726",
                Data = new List<int>()
            };

            statistics.Name = "ALL";

            var tests = await _testRepository.GetAll();

            foreach(var i in tests)
            {
                var amountAllUserTests = 0;
                var amountTestedUserTests = 0;
                var amountPendingUserTests = 0;
                statistics.Data.Labels.Add(i.Name);

                var userTests = _userTestRepository.GetUserTestsByTestId(i.Id).Result;

                if (userTests != null)
                {
                    amountAllUserTests = userTests.Count();
                    amountTestedUserTests = userTests.Where(x => x.IsTested == true).Count();
                    amountPendingUserTests = userTests.Where(x => x.IsTested == false).Count();
                }

                all.Data.Add(amountAllUserTests);
                tested.Data.Add(amountTestedUserTests);
                peding.Data.Add(amountPendingUserTests);
            }

            statistics.Data.Datasets.AddRange(new List<Dataset> { all, tested, peding });

            return statistics;
        }
    }
}
