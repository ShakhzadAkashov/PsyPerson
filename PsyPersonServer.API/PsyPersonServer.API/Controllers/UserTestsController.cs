using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.UserTests.Commands.CreateUserTest;
using PsyPersonServer.Application.UserTests.Queries.GetAllUsers;
using PsyPersonServer.Application.UserTests.Queries.GetUserTests;
using PsyPersonServer.Application.UserTests.Queries.GetUserTestsDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyPersonServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserTestsController : ControllerBase
    {
        public UserTestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [HttpGet]
        [Route("Users")]
        //Get : /api/UserTests/Users
        public async Task<IActionResult> Users([FromQuery] GetAllUsersQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("UserTests")]
        //Get : /api/UserTests/UserTests
        public async Task<IActionResult> UserTests([FromQuery] GetUserTestsQ query)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            return Ok(await _mediator.Send(new GetUserTestsQ 
            {
                Page = query.Page,
                ItemPerPage = query.ItemPerPage,
                UserId = userId
            }));
        }

        [HttpPost]
        [Route("CreateUserTest")]
        //POST : /api/UserTests/CreateUserTest
        public async Task<IActionResult> CreateUserTest([FromBody] CreateUserTestC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("UserTestsDetails")]
        //Get : /api/UserTests/UserTestsDetails
        public async Task<IActionResult> UserTestsDetails([FromQuery] GetUserTestsDetailsQ query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
