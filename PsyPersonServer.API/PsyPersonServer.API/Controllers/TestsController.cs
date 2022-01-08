using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.Tests.Commands.CreateTest;
using PsyPersonServer.Application.Tests.Commands.UpdateTest;
using PsyPersonServer.Application.Tests.Queries.GetTests;
using PsyPersonServer.Application.Tests.Queries.GetTestsByUserId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PsyPersonServer.Domain.Models.Permission;
using PsyPersonServer.Application.Tests.Commands.RemoveTest;

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

        [Authorize(Permissions.Tests_View)]
        [HttpGet]
        [Route("GetTests")]
        //Get : /api/Tests/GetTests
        public async Task<IActionResult> GetTests([FromQuery] GetTestsQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Tests_View)]
        [HttpGet]
        [Route("TestsForLookupTable")]
        //Get : /api/Tests/TestsForLookupTable
        public async Task<IActionResult> TestsForLookupTable([FromQuery] GetTestsByUserIdForLookupTableQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Tests_Create)]
        [HttpPost]
        [Route("CreateTest")]
        //POST : /api/Tests/CreateTest
        public async Task<IActionResult> CreateTest([FromBody] CreateTestC command)
        {
            return Ok(await _mediator.Send(command));
        }


        [Authorize(Permissions.Tests_Edit)]
        [HttpPut]
        [Route("UpdateTest")]
        //PUT : /api/Tests/UpdateTest
        public async Task<IActionResult> UpdateTest([FromBody] UpdateTestC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.Tests_Delete)]
        [HttpDelete]
        [Route("Remove")]
        //Delete : /api/Tests/Remove
        public async Task<IActionResult> Remove([FromBody] RemoveTestC command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
