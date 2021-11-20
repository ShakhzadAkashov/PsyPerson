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
    public class TestQuestionRepository : ITestQuestionRepository
    {
        public TestQuestionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly DBContext _dbContext;

        public async Task<PagedResponse<TestQuestion>> GetAll(int page, int itemPerPage)
        {
            var testQuestions = _dbContext.TestQuestions.Include(x => x.Answers).AsQueryable();

            var total = await testQuestions.CountAsync();

            return new PagedResponse<TestQuestion>(testQuestions
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page -1) * itemPerPage)
                .Take(itemPerPage),total);
        }

        public async Task<TestQuestion> Create(string name, TestQuestionEnum questionType, Guid testId, List<TestQuestionAnswer> answers)
        {
            var question = new TestQuestion
            {
                Id = new Guid(),
                Name = name,
                QuestionType = questionType,
                TestId = testId,
                CreatedDate = DateTime.Now
            };

            await _dbContext.TestQuestions.AddAsync(question);
            await _dbContext.SaveChangesAsync();

            foreach (var i in answers)
            {
                await CreateQuestionAnswer(i,question.Id);
            }

            return question;
        }

        private async Task CreateQuestionAnswer(TestQuestionAnswer questionAnswer, Guid id)
        {
            var qestionAnswer = new TestQuestionAnswer
            {
                Id = new Guid(),
                Name = questionAnswer.Name,
                IsCorrect = questionAnswer.IsCorrect,
                TestQuestionId = id
            };

            await _dbContext.TestQuestionAnswers.AddAsync(qestionAnswer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
