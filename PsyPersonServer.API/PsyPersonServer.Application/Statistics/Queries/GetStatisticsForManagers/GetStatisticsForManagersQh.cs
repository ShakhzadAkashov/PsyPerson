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
    public class GetStatisticsForManagersQh : IRequestHandler<GetStatisticsForManagersQ, List<StatisticDto>>
    {
        public GetStatisticsForManagersQh(ITestRepository testRepository, IUserTestRepository userTestRepository, IUserTestingHistoryRepository userTestingHistoryRepository)
        {
            _testRepository = testRepository;
            _userTestRepository = userTestRepository;
            _userTestingHistoryRepository = userTestingHistoryRepository;
        }

        private readonly ITestRepository _testRepository;
        private readonly IUserTestRepository _userTestRepository;
        private readonly IUserTestingHistoryRepository _userTestingHistoryRepository;

        public async Task<List<StatisticDto>> Handle(GetStatisticsForManagersQ request, CancellationToken cancellationToken)
        {
            Random random = new Random();
            var currentDate = DateTime.Now.Date;
            var periodOfWeek = DateTime.Now.Date.AddDays(-7);

            var result = new List<StatisticDto>();
            var userStatistics = InitStatistic("WEEKLY_USERS", "Статистика тестирования за неделю. Количество пройденных тестов пользователем.");

            var userTestingHistList = await _userTestingHistoryRepository.GetByPeriod(periodOfWeek, currentDate);

            foreach (var i in userTestingHistList)
            {
                var isContain = false;

                foreach (var j in userStatistics.Data.Datasets)
                {
                    if (i.UserTestFk.UserFk.UserName == j.Label)
                        isContain = true;
                }

                if (!isContain)
                {
                    if (userStatistics.Data.Datasets.Count == 0)
                    {
                        userStatistics.Data.Datasets.Add(InitDataset(i.UserTestFk.UserFk.UserName, random, true));
                    }
                    else 
                    {
                        userStatistics.Data.Datasets.Add(InitDataset(i.UserTestFk.UserFk.UserName, random, false));
                    }
                }
            }

            for (var i = 1; i <= 7; i++)
            {
                userStatistics.Data.Labels.Add(DateTime.Now.Date.AddDays(-(7-i)).ToString("yyyy.MM.dd"));

                Dictionary<string,int> userDictionary = new Dictionary<string, int>();
                foreach (var k in userStatistics.Data.Datasets)
                {
                    userDictionary.Add(k.Label, 0);
                }

                var hist = userTestingHistList.Where(x => x.TestedDate.Date >= DateTime.Now.Date.AddDays(-(7 - i)) && x.TestedDate.Date <= DateTime.Now.Date.AddDays(-(7 - i))).Select(x => x.UserTestId).Distinct();

                foreach (var j in hist)
                {
                    var userTest = await _userTestRepository.GetById(j);

                    foreach (var d in userDictionary)
                    {
                        if (d.Key == userTest.UserFk.UserName)
                            userDictionary[d.Key] += 1;
                    }
                }

                foreach (var q in userStatistics.Data.Datasets)
                {
                    foreach (var u in userDictionary)
                    {
                        if (u.Key == q.Label)
                            q.Data.Add(u.Value);
                    }
                }
            }

            result.Add(userStatistics);

            //--------------------------------------------------------------------------------------------------------------------------//

            var testStatistics = InitStatistic("WEEKLY_TESTS", "Статистика тестирования за неделю. Количество прохожения каждого теста.");

            foreach (var i in userTestingHistList)
            {
                var isContain = false;

                foreach (var j in testStatistics.Data.Datasets)
                {
                    if (i.UserTestFk.TestFk.Name == j.Label)
                        isContain = true;
                }

                if (!isContain)
                {
                    if (testStatistics.Data.Datasets.Count == 0)
                    {
                        testStatistics.Data.Datasets.Add(InitDataset(i.UserTestFk.TestFk.Name, random, true));
                    }
                    else
                    {
                        testStatistics.Data.Datasets.Add(InitDataset(i.UserTestFk.TestFk.Name,random,false));
                    }
                }
            }
            for (var i = 1; i <= 7; i++)
            {
                testStatistics.Data.Labels.Add(DateTime.Now.Date.AddDays(-(7 - i)).ToString("yyyy.MM.dd"));

                Dictionary<string, int> testDictionary = new Dictionary<string, int>();
                foreach (var k in testStatistics.Data.Datasets)
                {
                    testDictionary.Add(k.Label, 0);
                }

                var hist = userTestingHistList.Where(x => x.TestedDate.Date >= DateTime.Now.Date.AddDays(-(7 - i)) && x.TestedDate.Date <= DateTime.Now.Date.AddDays(-(7 - i)));

                foreach (var j in hist)
                {
                    var userTest = await _userTestRepository.GetById(j.UserTestId);

                    foreach (var d in testDictionary)
                    {
                        if (d.Key == userTest.TestFk.Name)
                            testDictionary[d.Key] += 1;
                    }
                }

                foreach (var q in testStatistics.Data.Datasets)
                {
                    foreach (var u in testDictionary)
                    {
                        if (u.Key == q.Label)
                            q.Data.Add(u.Value);
                    }
                }
            }

            result.Add(testStatistics);

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
