using MediatR;
using Microsoft.Extensions.Logging;
using PsyPersonServer.Domain.Models.AppFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.AppFiles.Commands.UploadeFile
{
    public class UploadFileCh : IRequestHandler<UploadFileC, UploadFileResponse>
    {
        public UploadFileCh(ILogger<UploadFileCh> logger)
        {
            _logger = logger;
        }

        private readonly ILogger<UploadFileCh> _logger;

        public async Task<UploadFileResponse> Handle(UploadFileC request, CancellationToken cancellationToken)
        {
            try
            {
                var file = request.File;
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return new UploadFileResponse { DbPath = dbPath };
                }
                else
                {
                    _logger.LogError($"File {file.FileName} lenght <= 0");
                    throw new Exception($"File upload: {file.FileName} lenght <= 0");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"File upload failed: {ex}", ex);
                throw new Exception($"File upload failed {ex}", ex);
            }
        }
    }
}
