using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.ApplicationUsers.Commands.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyPersonServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ApplicationUserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public async Task<IActionResult> PostApplicationUser(RegisterC command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
