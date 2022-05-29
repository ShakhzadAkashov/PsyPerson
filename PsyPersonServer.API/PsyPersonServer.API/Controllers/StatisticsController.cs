using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.Statistics.Queries.GetStatisticsForEmployees;
using PsyPersonServer.Application.Statistics.Queries.GetStatisticsForManager;
using PsyPersonServer.Application.Statistics.Queries.GetUserTestingHistoryStatistics;
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
        public async Task<IActionResult> StatisticsForManager()
        {
            return Ok(await _mediator.Send(new GetStatisticsForManagersQ()));
        }

        [Authorize(Permissions.Statistics_ForEmployees)]
        [HttpGet]
        [Route("StatisticsForEmployee")]
        //Get : /api/Statistics/StatisticsForEmployee
        public async Task<IActionResult> StatisticsForEmployee()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return Ok(await _mediator.Send(new GetStatisticsForEmployeesQ { UserId = userId }));
        }

        [Authorize(Permissions.Statistics_UserTestingHistoryReports)]
        [HttpGet]
        [Route("UserTestingHistoryStatistics")]
        //Get : /api/Statistics/UserTestingHistoryStatistics
        public async Task<IActionResult> UserTestingHistoryStatistics([FromQuery] GetUserTestingHistoryStatisticsQ query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
