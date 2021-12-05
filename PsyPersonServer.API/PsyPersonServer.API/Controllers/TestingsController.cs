using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.Testings.Queries.GetTestForTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyPersonServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestingsController : ControllerBase
    {
        public TestingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [Authorize]
        [HttpGet]
        [Route("GetTestForTesting")]
        //Get : /api/Testings/GetTestForTesting
        public async Task<IActionResult> GetTestForTesting([FromQuery] GetTestForTestingQ query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
