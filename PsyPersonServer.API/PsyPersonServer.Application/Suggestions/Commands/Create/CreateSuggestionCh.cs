using AutoMapper;
using MediatR;
using PsyPersonServer.Application.Syggestions.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Suggestions.Commands.Create
{
    public class CreateSuggestionCh : IRequestHandler<CreateSuggestionC, SuggestionDto>
    {
        public CreateSuggestionCh(ISuggestionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly ISuggestionRepository _repository;
        private readonly IMapper _mapper;

        public async Task<SuggestionDto> Handle(CreateSuggestionC request, CancellationToken cancellationToken)
        {
            var suggestion = await _repository.Create(request.Name, request.Description, request.RangeFrom, request.RangeTo,request.Status, request.SelectionType);

            return _mapper.Map<SuggestionDto>(suggestion);
        }
    }
}
