using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestion;
using PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestionFromFile;
using PsyPersonServer.Application.TestQuestions.Commands.UpdateTestQuestion;
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

        [HttpPost]
        [Authorize]
        [Route("Create")]
        //POST : /api/TestQuestions/Create
        public async Task<IActionResult> Create([FromBody] CreateTestQuestionC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Authorize]
        [Route("Update")]
        //PUT : /api/TestQuestions/Update
        public async Task<IActionResult> Update([FromBody] UpdateTestQuestionC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost, DisableRequestSizeLimit]
        [Authorize]
        [Route("UploadFromFile")]
        //POST : /api/TestQuestions/UploadFromFile
        public async Task<IActionResult> UploadFromFile([FromForm] CreateTestQuestionsFromFileC command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
