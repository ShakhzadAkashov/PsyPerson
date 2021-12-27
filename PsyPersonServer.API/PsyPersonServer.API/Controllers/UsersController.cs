using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.Users.Commands.AssignRoleToUser;
using PsyPersonServer.Application.Users.Commands.CreateUser;
using PsyPersonServer.Application.Users.Commands.RemoveRoleFromUser;
using PsyPersonServer.Application.Users.Commands.RemoveUser;
using PsyPersonServer.Application.Users.Commands.UpdateUser;
using PsyPersonServer.Application.Users.Queries;
using PsyPersonServer.Application.Users.Queries.GetAllUserRoles;
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
    public class UsersController : ControllerBase
    {
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [Authorize(Permissions.Users_View)]
        [HttpGet]
        [Route("GetAll")]
        //Get : /api/Users/GetAll
        public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Users_Edit)]
        [HttpPut]
        [Route("UpdateUser")]
        //PUT : /api/Users/UpdateUser
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.Users_Create)]
        [HttpPost]
        [Route("CreateUser")]
        //POST : /api/Users/UpdateUser
        public async Task<IActionResult> CreateUser([FromBody] CreateUserC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.UserRoles_View)]
        [HttpGet]
        [Route("GetUserRoles")]
        //Get : /api/Users/GetUserRoles
        public async Task<IActionResult> GetUserRoles([FromQuery] GetUserRolesQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.UserRoles_Create)]
        [HttpPost]
        [Route("AssingRole")]
        //POST : /api/Users/AssingRole
        public async Task<IActionResult> AssingRole([FromBody] AssignRoleToUserC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.Users_Delete)]
        [HttpDelete]
        [Route("Remove")]
        //Delete : /api/Users/Remove
        public async Task<IActionResult> Remove([FromBody] RemoveUserC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.UserRoles_Delete)]
        [HttpDelete]
        [Route("RemoveRoleFromUser")]
        //Delete : /api/Users/RemoveRoleFromUser
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] RemoveRoleFromUserC command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
