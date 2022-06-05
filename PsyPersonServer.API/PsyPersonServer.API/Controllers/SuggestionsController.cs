using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.Suggestions.Commands.Create;
using PsyPersonServer.Application.Suggestions.Commands.Remove;
using PsyPersonServer.Application.Suggestions.Commands.Update;
using PsyPersonServer.Application.Suggestions.Queries.GetById;
using PsyPersonServer.Application.Suggestions.Queries.GetByStatus;
using PsyPersonServer.Application.Suggestions.Queries.GetSuggestions;
using PsyPersonServer.Domain.Models.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyPersonServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : ControllerBase
    {
        public SuggestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [Authorize(Permissions.Suggestions_View)]
        [HttpGet]
        [Route("GetAll")]
        //Get : /api/Suggestions/GetAll
        public async Task<IActionResult> GetAll([FromQuery] GetSuggestionsQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Suggestions_View)]
        [HttpGet]
        [Route("GetByStatus")]
        //Get : /api/Suggestions/GetByStatus
        public async Task<IActionResult> GetByStatus([FromQuery] GetByStatusQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Suggestions_View)]
        [HttpGet]
        [Route("GetById")]
        //Get : /api/Suggestions/GetById
        public async Task<IActionResult> GetById([FromQuery] GetByIdQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Suggestions_Create)]
        [HttpPost]
        [Route("Create")]
        //POST : /api/Suggestions/Create
        public async Task<IActionResult> Create([FromBody] CreateSuggestionC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.Suggestions_Edit)]
        [HttpPut]
        [Route("Update")]
        //PUT : /api/Suggestions/Update
        public async Task<IActionResult> Update([FromBody] UpdateSuggestionC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.Suggestions_Delete)]
        [HttpDelete]
        [Route("Remove")]
        //Delete : /api/Suggestions/Remove
        public async Task<IActionResult> Remove([FromBody] RemoveSuggestionC command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
