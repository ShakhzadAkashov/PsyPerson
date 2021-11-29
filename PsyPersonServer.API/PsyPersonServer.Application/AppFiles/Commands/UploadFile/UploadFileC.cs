using MediatR;
using PsyPersonServer.Domain.Models.AppFiles;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PsyPersonServer.Application.AppFiles.Commands.UploadeFile
{
    public class UploadFileC : IRequest<UploadFileResponseDto>
    {
        public IFormFile File { get; set; }
    }
}
