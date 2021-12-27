using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestion;
using PsyPersonServer.Application.TestQuestions.Commands.CreateTestQuestionFromFile;
using PsyPersonServer.Application.TestQuestions.Commands.UpdateTestQuestion;
using PsyPersonServer.Application.TestQuestions.Queries.GetTestQuestionById;
using PsyPersonServer.Application.TestQuestions.Queries.GetTestQuestions;
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
    public class TestQuestionsController : ControllerBase
    {
        public TestQuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;


        [Authorize(Permissions.TestQuestions_View)]
        [HttpGet]
        [Route("GetAll")]
        //Get : /api/TestQuestions/GetAll
        public async Task<IActionResult> GetTests([FromQuery] GetTestQuestionsQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.TestQuestions_View)]
        [HttpGet]
        [Route("GetById")]
        //Get : /api/TestQuestions/GetById
        public async Task<IActionResult> GetById([FromQuery] GetTestQuestionByIdQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.TestQuestions_Create)]
        [HttpPost]
        [Route("Create")]
        //POST : /api/TestQuestions/Create
        public async Task<IActionResult> Create([FromBody] CreateTestQuestionC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.TestQuestions_Edit)]
        [HttpPut]
        [Route("Update")]
        //PUT : /api/TestQuestions/Update
        public async Task<IActionResult> Update([FromBody] UpdateTestQuestionC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.TestQuestions_Upload)]
        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadFromFile")]
        //POST : /api/TestQuestions/UploadFromFile
        public async Task<IActionResult> UploadFromFile([FromForm] CreateTestQuestionsFromFileC command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
