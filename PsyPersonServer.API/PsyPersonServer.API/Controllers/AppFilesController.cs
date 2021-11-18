using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.AppFiles.Commands.UploadeFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsyPersonServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppFilesController : ControllerBase
    {
        public AppFilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;

        [HttpPost]
        [Authorize]
        [Route("Upload")]
        //POST : /api/AppFiles/Upload
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files[0];
            return Ok(await _mediator.Send(new UploadFileC { File = file}));
        }
    }
}
