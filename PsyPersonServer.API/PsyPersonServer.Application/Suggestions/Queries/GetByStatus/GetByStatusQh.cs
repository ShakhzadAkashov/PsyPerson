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

namespace PsyPersonServer.Application.Suggestions.Queries.GetByStatus
{
    class GetByStatusQh : IRequestHandler<GetByStatusQ, PagedResponse<SuggestionDto>>
    {
        public GetByStatusQh(IMapper mapper, ISuggestionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        private readonly IMapper _mapper;
        private readonly ISuggestionRepository _repository;

        public async Task<PagedResponse<SuggestionDto>> Handle(GetByStatusQ request, CancellationToken cancellationToken)
        {
            var suggestions = await _repository.GetByStatus(request.Page, request.ItemPerPage, request.Name, request.Status);
            var result = suggestions.Data.Select(x => _mapper.Map<SuggestionDto>(x)).ToList();

            return new PagedResponse<SuggestionDto>(result, suggestions.Total);
        }
    }
}
