using MediatR;
using PsyPersonServer.Application.EmailMessage.Commands.UpdateEmailMessageSetting;
using PsyPersonServer.Application.Syggestions.Dtos;
using PsyPersonServer.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.Suggestions.Commands.Update
{
    public class UpdateSuggestionCh : IRequestHandler<UpdateSuggestionC, bool>
    {
        public UpdateSuggestionCh(ISuggestionRepository repository)
        {
            _repository = repository;
        }

        private readonly ISuggestionRepository _repository;

        public async Task<bool> Handle(UpdateSuggestionC request, CancellationToken cancellationToken)
        {
            return await _repository.Update(request.Id,request.Name, request.Description, request.RangeFrom, request.RangeTo, request.Status, request.SelectionType);
        }
    }
}
