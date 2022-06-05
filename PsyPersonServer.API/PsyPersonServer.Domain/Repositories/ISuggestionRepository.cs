using PsyPersonServer.Domain.Entities;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PsyPersonServer.Domain.Repositories
{
    public interface ISuggestionRepository
    {
        Task<PagedResponse<Suggestion>> Filter(int page, int itemPerPage, string name);
        Task<Suggestion> GetById(Guid id);
        Task<PagedResponse<Suggestion>> GetByStatus(int page, int itemPerPage, string name, TestResultStatusEnum status);
        Task<Suggestion> Create(string name, string description, double rangeFrom, double rangeTo, TestResultStatusEnum status, SuggestionSelectTypeEnum selectionType);
        Task<bool> Update(Guid id, string name, string description, double rangeFrom, double rangeTo, TestResultStatusEnum status, SuggestionSelectTypeEnum selectionType);
        Task<bool> Remove(Guid id);
    }
}
