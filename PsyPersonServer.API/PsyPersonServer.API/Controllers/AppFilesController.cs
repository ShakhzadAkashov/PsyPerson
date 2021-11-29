using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsyPersonServer.Application.AppFiles.Commands.UploadeFile;
using PsyPersonServer.Application.AppFiles.Queries.GetContentType;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost, DisableRequestSizeLimit]
        [Authorize]
        [Route("Upload")]
        //POST : /api/AppFiles/Upload
        public async Task<IActionResult> Upload()
        {
            var file = Request.Form.Files[0];
            return Ok(await _mediator.Send(new UploadFileC { File = file}));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetPhoto")]
        //GET : /api/AppFiles/GetPhoto
        public IActionResult GetPhoto(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var image = System.IO.File.OpenRead(filePath);
            return File(image, "image/*");
        }

        [HttpGet]
        [Authorize]
        [Route("Download")]
        //GET : /api/AppFiles/Download
        public async Task<IActionResult> Download([FromQuery] string fileName)
        {
            var uploads = Path.Combine("StaticFiles", "Images");
            var filePath = Path.Combine(uploads, fileName);
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            var contentType = await _mediator.Send(new GetContentTypeQ { FilePath = filePath});

            return File(memory, contentType, fileName);
        }
    }
}
