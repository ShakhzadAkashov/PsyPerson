using AutoMapper;
using MediatR;
using PsyPersonServer.Application.Syggestions.Dtos;
using PsyPersonServer.Domain.Models.PagedResponse;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Suggestions.Queries.GetSuggestions
{
    class GetSuggestionsQh : IRequestHandler<GetSuggestionsQ, PagedResponse<SuggestionDto>>
    {
        public GetSuggestionsQh(IMapper mapper, ISuggestionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private readonly IMapper _mapper;
        private readonly ISuggestionRepository _repository;

        public async Task<PagedResponse<SuggestionDto>> Handle(GetSuggestionsQ request, CancellationToken cancellationToken)
        {
            var suggestions = await _repository.Filter(request.Page, request.ItemPerPage, request.Name);
            var result = suggestions.Data.Select(x => _mapper.Map<SuggestionDto>(x)).ToList();

            return new PagedResponse<SuggestionDto>(result, suggestions.Total);
        }
    }
}
