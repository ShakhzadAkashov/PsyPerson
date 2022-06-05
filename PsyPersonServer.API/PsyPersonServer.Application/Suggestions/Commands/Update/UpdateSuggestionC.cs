using MediatR;
using PsyPersonServer.Application.Syggestions.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.Suggestions.Commands.Update
{
    public class UpdateSuggestionC : SuggestionDto, IRequest<bool>
    {

    }
}
