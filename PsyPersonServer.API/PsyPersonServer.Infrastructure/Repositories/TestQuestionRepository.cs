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

        public async Task<PagedResponse<TestQuestion>> GetAll(int page, int itemPerPage, Guid? testId)
        {
            var testQuestions = _dbContext.TestQuestions.Include(x => x.Answers).AsQueryable();

            if (testId.HasValue)
            {
                testQuestions = testQuestions.Where(x => x.TestId == testId);
            }

            var total = await testQuestions.CountAsync();

            return new PagedResponse<TestQuestion>(testQuestions
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page -1) * itemPerPage)
                .Take(itemPerPage),total);
        }

        public async Task<IEnumerable<TestQuestion>> GetAllForTestingById(Guid testId)
        {
            var testQuestions = await _dbContext.TestQuestions.Include(x => x.Answers).Where(x => x.TestId == testId).ToListAsync();

            foreach (var i in testQuestions)
            {
                foreach (var j in i.Answers)
                {
                    if (j.IsCorrect == true)
                        j.IsCorrect = false;
                }
            }
            return testQuestions;
        }

        public async Task<IEnumerable<TestQuestion>> GetAllWithOnlyTruAnswersByTestId(Guid testId)
        {
            var testQuestions = await _dbContext.TestQuestions.Include(x => x.Answers)
                .Where(x => x.TestId == testId).ToListAsync();

            foreach (var i in testQuestions)
            {
                i.Answers = i.Answers.Where(x => x.IsCorrect == true).ToList();
            }
            return testQuestions;
        }

        public async Task<TestQuestion> Create(string name, TestQuestionTypeEnum questionType, Guid testId, List<TestQuestionAnswer> answers)
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
                IdForView = questionAnswer.IdForView,
                TestQuestionId = id
            };

            await _dbContext.TestQuestionAnswers.AddAsync(qestionAnswer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Update(Guid id, string name, List<TestQuestionAnswer> answers) 
        {
            var question = await _dbContext.TestQuestions.FirstOrDefaultAsync(x => x.Id == id);

            if (question != null) 
            {
                question.Name = name;

                var answerFromDbIds = _dbContext.TestQuestionAnswers.Where(x => x.TestQuestionId == id).Select(x => x.Id);
                var answerIds = answers.Select(x => x.Id).ToList();

                foreach (var i in answerFromDbIds)
                {
                    if (!answerIds.Contains(i))
                    {
                        var a = _dbContext.TestQuestionAnswers.FirstOrDefault(x => x.Id == i);
                        _dbContext.TestQuestionAnswers.Remove(a);
                    }
                }

                foreach (var i in answers)
                {
                    if (i.Id == Guid.Empty || i.Id == null)
                    {
                        await CreateQuestionAnswer(i, id);
                    }
                    else
                    {
                        await UpdateQuestionAnswer(i.Id, i.Name, i.IsCorrect, i.IdForView);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private async Task<bool> UpdateQuestionAnswer(Guid id, string name, bool? isCorrect, int idForView)
        {
            var answer = await _dbContext.TestQuestionAnswers.FirstOrDefaultAsync(x => x.Id == id);

            if (answer != null)
            {
                answer.Name = name;
                answer.IsCorrect = isCorrect;
                answer.IdForView = idForView;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
