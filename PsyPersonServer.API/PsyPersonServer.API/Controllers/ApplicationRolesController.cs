using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.ApplicationRoles.Commands.CreateRole;
using PsyPersonServer.Application.ApplicationRoles.Commands.RemoveRole;
using PsyPersonServer.Application.ApplicationRoles.Commands.UpdateRole;
using PsyPersonServer.Application.ApplicationRoles.Queries.GetAllRoles;
using PsyPersonServer.Application.Permissions.Commands.AssignPermissionsToRole;
using PsyPersonServer.Application.Permissions.Queries.GetRolePermissions;
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
    public class ApplicationRolesController : ControllerBase
    {
        public ApplicationRolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [Authorize(Permissions.Roles_View)]
        [HttpGet]
        [Route("GetAll")]
        //Get : /api/ApplicationRoles/GetAll
        public async Task<IActionResult> GetAll([FromQuery] GetAllRolesQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Roles_Create)]
        [HttpPost]
        [Route("CreateRole")]
        //POST : /api/ApplicationRoles/CreateRole
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.Roles_Edit)]
        [HttpPut]
        [Route("UpdateRole")]
        //PUT : /api/ApplicationRoles/UpdateRole
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.Roles_Delete)]
        [HttpDelete]
        [Route("Remove")]
        //Delete : /api/Users/Remove
        public async Task<IActionResult> Remove([FromBody] RemoveRoleC command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize(Permissions.Permission_View)]
        [HttpGet]
        [Route("GetPermissions")]
        //Get : /api/ApplicationRoles/GetPermissions
        public async Task<IActionResult> GetPermissions([FromQuery] GetRolePermissionsQ query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Permissions.Permission_Create)]
        [HttpPost]
        [Route("AssignPermissionsToRole")]
        //Get : /api/ApplicationRoles/AssignPermissionsToRole
        public async Task<IActionResult> AssignPermissionsToRole([FromBody] AssignPermissionsToRoleC command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
