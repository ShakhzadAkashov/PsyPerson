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
    public class SuggestionRepository : ISuggestionRepository
    {
        public SuggestionRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly DBContext _dbContext;

        public async Task<PagedResponse<Suggestion>> Filter(int page, int itemPerPage, string name)
        {
            var suggestions = _dbContext.Suggestions.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                suggestions = suggestions.Where(x => x.Name.Contains(name));
            }

            var total = await suggestions.CountAsync();

            return new PagedResponse<Suggestion>(suggestions
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage), total);
        }

        public async Task<Suggestion> GetById(Guid id)
        {
            var suggestions = await _dbContext.Suggestions.FirstOrDefaultAsync(x => x.Id == id);
            return suggestions;
        }

        public async Task<PagedResponse<Suggestion>> GetByStatus(int page, int itemPerPage, string name,TestResultStatusEnum status)
        {
            var suggestions = _dbContext.Suggestions.Where(x => x.Status == status).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                suggestions = suggestions.Where(x => x.Name.Contains(name));
            }

            var total = await suggestions.CountAsync();

            return new PagedResponse<Suggestion>(suggestions
                .Skip((page - 1) * itemPerPage)
                .Take(itemPerPage), total);
        }

        public async Task<Suggestion> Create(string name, string description, double rangeFrom, double rangeTo, TestResultStatusEnum status, SuggestionSelectTypeEnum selectionType)
        {
            var suggestion = new Suggestion
            {
                Id = new Guid(),
                Name = name,
                Description = description,
                RangeFrom = rangeFrom,
                RangeTo = rangeTo,
                Status = status,
                SelectionType = selectionType
            };

            await _dbContext.Suggestions.AddAsync(suggestion);
            await _dbContext.SaveChangesAsync();

            return suggestion;
        }

        public async Task<bool> Update(Guid id, string name, string description, double rangeFrom, double rangeTo, TestResultStatusEnum status, SuggestionSelectTypeEnum selectionType)
        {
            var suggestion = await _dbContext.Suggestions.FirstOrDefaultAsync(x => x.Id == id);

            if (suggestion != null)
            {
                suggestion.Name = name;
                suggestion.Description = description;
                suggestion.RangeFrom = rangeFrom;
                suggestion.RangeTo = rangeTo;
                suggestion.Status = status;
                suggestion.SelectionType = selectionType;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Remove(Guid id)
        {
            var suggestion = await _dbContext.Suggestions.FirstOrDefaultAsync(x => x.Id == id);

            if (suggestion != null)
            {
                _dbContext.Suggestions.Remove(suggestion);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
