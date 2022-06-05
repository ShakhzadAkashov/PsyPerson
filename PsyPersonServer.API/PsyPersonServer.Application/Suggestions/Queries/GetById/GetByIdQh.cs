using AutoMapper;
using MediatR;
using PsyPersonServer.Application.Syggestions.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Suggestions.Queries.GetById
{
    public class GetByIdQh : IRequestHandler<GetByIdQ, SuggestionDto>
    {
        public GetByIdQh(ISuggestionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private readonly ISuggestionRepository _repository;
        private readonly IMapper _mapper;

        public async Task<SuggestionDto> Handle(GetByIdQ request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetById(request.Id);
            return _mapper.Map<SuggestionDto>(result);
        }
    }
}
