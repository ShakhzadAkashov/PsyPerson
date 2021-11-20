using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.TestQuestions.Queries.GetTestQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyPersonServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestQuestionsController : ControllerBase
    {
        public TestQuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [HttpGet]
        [Authorize]
        [Route("GetAll")]
        //Get : /api/TestQuestions/GetAll
        public async Task<IActionResult> GetTests([FromQuery] GetTestQuestionsQ query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
