using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.Tests.Commands.CreateTest;
using PsyPersonServer.Application.Tests.Commands.UpdateTest;
using PsyPersonServer.Application.Tests.Queries.GetTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyPersonServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        public TestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [HttpGet]
        [Authorize]
        [Route("GetTests")]
        //Get : /api/Tests/GetTests
        public async Task<IActionResult> GetTests([FromQuery] GetTestsQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        [Authorize]
        [Route("CreateTest")]
        //POST : /api/Tests/CreateTest
        public async Task<IActionResult> CreateTest([FromBody] CreateTestC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [Authorize]
        [Route("UpdateTest")]
        //POST : /api/Tests/UpdateTest
        public async Task<IActionResult> UpdateTest([FromBody] UpdateTestC command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
