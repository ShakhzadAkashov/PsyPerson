using MediatR;
using PsyPersonServer.Application.Statistics.Dtos;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Statistics.Queries.GetStatisticsForEmployees
{
    public class GetStatisticsForEmployeesQh : IRequestHandler<GetStatisticsForEmployeesQ, StatisticDto>
    {
        public GetStatisticsForEmployeesQh(IUserTestRepository userTestRepository, IUserTestingHistoryRepository userTestingHistoryRepository)
        {
            _userTestRepository = userTestRepository;
            _userTestingHistoryRepository = userTestingHistoryRepository;
        }

        private readonly IUserTestRepository _userTestRepository;
        private readonly IUserTestingHistoryRepository _userTestingHistoryRepository;

        public async Task<StatisticDto> Handle(GetStatisticsForEmployeesQ request, CancellationToken cancellationToken)
        {
            Random random = new Random();
            var currentDate = DateTime.Now.Date;
            var periodOfWeek = DateTime.Now.Date.AddDays(-7);

            var statistics = new StatisticDto();
            statistics.Data = new ChartDto();
            statistics.Data.Labels = new List<string>();
            statistics.Data.Datasets = new List<DatasetDto>();

            statistics.NameCode = "WEEKLY_USER_TESTINGS";
            statistics.Name = "Статистика тестирования за неделю. Результат оценки в процентах.";

            var historyList = new List<UserTestingHistory>();

            var userTests = _userTestRepository.GetUserTestsByUserId(request.UserId).Result;
            var userTestingHistList = await _userTestingHistoryRepository.GetByPeriod(periodOfWeek, currentDate);

            foreach (var u in userTests)
            {
                var hist = userTestingHistList.Where(x => x.UserTestId == u.Id);
                historyList.AddRange(hist);
            }

            foreach (var i in historyList)
            {
                var isContain = false;

                foreach (var j in statistics.Data.Datasets)
                {
                    if (i.UserTestFk.TestFk.Name == j.Label)
                        isContain = true;
                }

                if (!isContain)
                {
                    if (statistics.Data.Datasets.Count == 0)
                    {
                        statistics.Data.Datasets.Add(InitDataset(i.UserTestFk.TestFk.Name, random, true));
                    }
                    else
                    {
                        statistics.Data.Datasets.Add(InitDataset(i.UserTestFk.TestFk.Name, random, false));
                    }
                }
            }

            for (var i = 1; i <= 7; i++)
            {
                statistics.Data.Labels.Add(DateTime.Now.Date.AddDays(-(7 - i)).ToString("yyyy.MM.dd"));

                Dictionary<string, double> testDictionary = new Dictionary<string, double>();
                foreach (var k in statistics.Data.Datasets)
                {
                    testDictionary.Add(k.Label, 0);
                }

                var hist = historyList.Where(x => x.TestedDate.Date >= DateTime.Now.Date.AddDays(-(7 - i)) && x.TestedDate.Date <= DateTime.Now.Date.AddDays(-(7 - i)));
                var resultHist = new List<UserTestingHistory>();

                foreach (var j in hist)
                {
                    var testHistory = hist.Where(x => x.UserTestId == j.UserTestId).OrderByDescending(x => x.TestedDate).First();

                    if (!resultHist.Contains(testHistory))
                        resultHist.Add(testHistory);
                }

                foreach (var h in resultHist)
                {
                    foreach(var t in testDictionary)
                    {
                        if (h.UserTestFk.TestFk.Name == t.Key)
                            testDictionary[t.Key] = h.TestScore;
                    }
                }

                foreach (var q in statistics.Data.Datasets)
                {
                    foreach (var u in testDictionary)
                    {
                        if (u.Key == q.Label)
                            q.Data.Add(u.Value);
                    }
                }
            }

            return statistics;
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
    }
}
