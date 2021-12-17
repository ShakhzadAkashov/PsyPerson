using Microsoft.EntityFrameworkCore;
using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Models.Tests;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Infrastructure.Repositories
{
    public class TestRepository : ITestRepository
    {
        public TestRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private readonly DBContext _dbContext;

        public async Task<PagedResponse<Test>> GetTests(int page, int itemPerPage)
        {
            var tests = _dbContext.Tests.Include(x => x.TestResultList).AsQueryable();

            var total = await tests.CountAsync();

            return new PagedResponse<Test>(tests
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage), total);
        }

        public async Task<Test> GetTestById(Guid id)
        {
            var test = await _dbContext.Tests.Include(x => x.TestResultList).FirstOrDefaultAsync(x => x.Id == id);
            return test;
        }

        public async Task<PagedResponse<Test>> GetTestsByUserId(int page, int itemPerPage, string userId)
        {
            var tests = _dbContext.Tests.AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                var userTests = _dbContext.UserTests.Where(x => x.UserId == userId);

                foreach (var i in userTests)
                {
                    foreach (var j in tests)
                    {
                        if (i.TestId == j.Id)
                        {
                            tests = tests.Where(x => x.Id != i.TestId);
                        }
                    }
                }
            }

            var total = await tests.CountAsync();

            return new PagedResponse<Test>(tests
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage), total);
        }

        public async Task<int> GetAmountTestQuestionsById(Guid id)
        {
            var amount = await _dbContext.TestQuestions.CountAsync(x => x.TestId == id);
            return amount;
        }

        public async Task<Test> Create(string name, string description, string imgPath, TestTypeEnum testType, List<TestResult> testResultList)
        {
            var test = new Test
            {
                Id = new Guid(),
                Name = name,
                Description = description,
                ImgPath = imgPath,
                CreatedDate = DateTime.Now,
                TestType = testType
            };

            await _dbContext.Tests.AddAsync(test);
            await _dbContext.SaveChangesAsync();

            foreach (var i in testResultList)
            {
                var result = await CreateTestResult(i, test.Id);
                test.TestResultList.Add(result);
            }
            return test;
        }

        public async Task<bool> Update(Guid id, string name, string description, string imgPath, List<TestResult> testResultList)
        {
            var test = await _dbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);

            if (test != null)
            {
                test.Name = name;
                test.Description = description;
                test.ImgPath = imgPath;

                var testResultFromDbIds = _dbContext.TestResults.Where(x => x.TestId == id).Select(x => x.Id);
                var testResultIds = testResultList.Select(x => x.Id).ToList();

                foreach (var i in testResultFromDbIds)
                {
                    if (!testResultIds.Contains(i))
                    {
                        var a = _dbContext.TestResults.FirstOrDefault(x => x.Id == i);
                        _dbContext.TestResults.Remove(a);
                    }
                }

                foreach (var i in testResultList)
                {
                    if (i.Id == Guid.Empty || i.Id == null)
                    {
                        await CreateTestResult(i, id);
                    }
                    else
                    {
                        await UpdateTestResult(i.Id, i.Name, i.RangeFrom, i.RangeTo, i.Status);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        private async Task<TestResult> CreateTestResult(TestResult testResult, Guid id)
        {
            testResult.TestId = id;

            await _dbContext.TestResults.AddAsync(testResult);
            await _dbContext.SaveChangesAsync();

            return testResult;
        }

        private async Task<bool> UpdateTestResult(Guid id, string name, double rangeFrom, double rangeTo, TestResultStatusEnum status)
        {
            var result = await _dbContext.TestResults.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                result.Name = name;
                result.RangeFrom = rangeFrom;
                result.RangeTo = rangeTo;
                result.Status = status;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
