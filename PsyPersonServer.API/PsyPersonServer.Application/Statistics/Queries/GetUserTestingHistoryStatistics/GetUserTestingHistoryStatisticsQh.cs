using MediatR;
using PsyPersonServer.Application.Statistics.Dtos;
using PsyPersonServer.Domain.DapperRepositories;
using PsyPersonServer.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Statistics.Queries.GetUserTestingHistoryStatistics
{
    public class GetUserTestingHistoryStatisticsQh : IRequestHandler<GetUserTestingHistoryStatisticsQ, UserTestingReportDto>
    {
        public GetUserTestingHistoryStatisticsQh(IDapperUserTestingHistoryRepository repo)
        {
            _repo = repo;
        }

        private readonly IDapperUserTestingHistoryRepository _repo;

        public async Task<UserTestingReportDto> Handle(GetUserTestingHistoryStatisticsQ request, CancellationToken cancellationToken)
        {
            var result = new UserTestingReportDto();
            Random random = new Random();
            
            var statistic = InitStatistic("ALL_USET_TESTING", "Статистика тестирования для пользователя: ");

            var userTestingList = await _repo.GetUserTestingHistoryStatistics(request.UserId);

            //Диаграмма статистики
            foreach (var i in userTestingList)
            {
                var isContain = false;

                if (statistic.Data.Labels.Count == 0)
                    statistic.Data.Labels.Add(i.TestedDate.ToString("yyyy.MM.dd"));

                foreach (var j in statistic.Data.Labels.ToList())
                {
                    if (i.TestedDate.Date.ToString("yyyy.MM.dd") == j)
                        isContain = true;   
                }

                if(!isContain)
                    statistic.Data.Labels.Add(i.TestedDate.Date.ToString("yyyy.MM.dd"));

                if (statistic.Data.Datasets.Count == 0)
                    statistic.Data.Datasets.Add(InitDataset(i.TestName, random, true));
                else
                {
                    var isContainData = false;

                    foreach (var j in statistic.Data.Datasets.ToList())
                    {
                        if (j.Label == i.TestName)
                            isContainData = true;
                    }

                    if (!isContainData)
                        statistic.Data.Datasets.Add(InitDataset(i.TestName, random, false));
                }  
            }

            foreach (var k in statistic.Data.Labels)
            {
                foreach (var i in statistic.Data.Datasets.ToList())
                {
                    var isTestingContain = false;
                    var count = 0;

                    foreach (var j in userTestingList)
                    {
                        if (i.Label == j.TestName && j.TestedDate.Date.ToString("yyyy.MM.dd") == k)
                        {
                            isTestingContain = true;
                            count = j.TestingCount;
                        }    
                    }

                    if (isTestingContain)
                        i.Data.Add(count);
                    else
                        i.Data.Add(0);
                }
            }

            result.Statistic = statistic;

            //История тестирования по каждому тесту
            var userTestingHistDescriptionList = new List<UserTestingHistDescriptionsDto>();

            foreach (var i in userTestingList)
            {
                var isContain = false;

                if (userTestingHistDescriptionList.Count == 0)
                    userTestingHistDescriptionList.Add(new UserTestingHistDescriptionsDto { UserTestId = i.UserTestId, TestName = i.TestName });

                foreach (var j in userTestingHistDescriptionList.ToList())
                {
                    if (i.UserTestId == j.UserTestId)
                        isContain = true;
                }

                if (!isContain)
                    userTestingHistDescriptionList.Add(new UserTestingHistDescriptionsDto { UserTestId = i.UserTestId, TestName = i.TestName });
            }

            foreach (var i in userTestingHistDescriptionList)
            {
                var hist = await _repo.GetUserTestingHistByUserTestId(i.UserTestId);
                i.HistoryDesriptionList = hist.ToList();
            }

            result.TestingDescriptions = userTestingHistDescriptionList;

            return result;
        }

        private DatasetDto InitDataset(string label, Random random, bool yAxisID)
        {
            var dataset = new DatasetDto
            {
                Label = label,
                Fill = false,
                BorderColor = String.Format("#{0:X6}", random.Next(0x1000000)),
                YAxisID = yAxisID ? "y1" : "y",
                Tension = .4,
                Data = new List<double>()
            };

            return dataset;
        }

        private StatisticDto InitStatistic(string nameCode, string name)
        {
            var statistics = new StatisticDto();
            statistics.Data = new ChartDto();
            statistics.Data.Labels = new List<string>();
            statistics.Data.Datasets = new List<DatasetDto>();

            statistics.NameCode = nameCode;
            statistics.Name = name;

            return statistics;
        }
    }
}
