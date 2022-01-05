using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.Statistics.Queries.GetStatisticsForManager;
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
    public class StatisticsController : ControllerBase
    {
        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [Authorize(Permissions.Statistics_ForManagers)]
        [HttpGet]
        [Route("StatisticsForManager")]
        //Get : /api/Statistics/StatisticsForManager
        public async Task<IActionResult> GetTests([FromQuery] GetStatisticsForManagersQ query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
