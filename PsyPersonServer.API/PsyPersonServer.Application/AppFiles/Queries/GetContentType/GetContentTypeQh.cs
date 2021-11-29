using MediatR;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PsyPersonServer.Application.AppFiles.Queries.GetContentType
{
    public class GetContentTypeQh : IRequestHandler<GetContentTypeQ, string>
    {
        public async Task<string> Handle(GetContentTypeQ request, CancellationToken cancellationToken)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(request.FilePath, out string contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
