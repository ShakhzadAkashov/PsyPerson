using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.Testings.Commands;
using PsyPersonServer.Application.Testings.Commands.CheckFirstLevelDifficultTypeTesting;
using PsyPersonServer.Application.Testings.Commands.CheckSimpleTypeTesting;
using PsyPersonServer.Application.Testings.Queries.GetTestForTesting;
using PsyPersonServer.Application.Testings.Queries.GetTestingHistory;
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
    public class TestingsController : ControllerBase
    {
        public TestingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [Authorize(Permissions.Testings_View)]
        [HttpGet]
        [Route("GetTestForTesting")]
        //Get : /api/Testings/GetTestForTesting
        public async Task<IActionResult> GetTestForTesting([FromQuery] GetTestForTestingQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Testings_Create)]
        [HttpPost]
        [Route("CheckSimpleTypeTest")]
        //POST : /api/Testings/CheckSimpleTypeTest
        public async Task<IActionResult> CheckSimpleTypeTest([FromBody] CheckSimpleTypeTestingC query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Testings_Create)]
        [HttpPost]
        [Route("CheckFirstLevelDifficultTypeTesting")]
        //POST : /api/Testings/CheckFirstLevelDifficultTypeTesting
        public async Task<IActionResult> CheckFirstLevelDifficultTypeTesting([FromBody] CheckFirstLevelDifficultTypeTestingC query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Testings_Create)]
        [HttpPost]
        [Route("CheckSecondLevelDifficultTypeTesting")]
        //POST : /api/Testings/CheckSecondLevelDifficultTypeTesting
        public async Task<IActionResult> CheckSecondLevelDifficultTypeTesting([FromBody] CheckSecondLevelDifficultTypeTestingC query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Testings_ViewHistory)]
        [HttpGet]
        [Route("GetTestingHistory")]
        //Get : /api/Testings/GetTestingHistory
        public async Task<IActionResult> GetTestingHistory([FromQuery] GetTestingHistoryQ query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
