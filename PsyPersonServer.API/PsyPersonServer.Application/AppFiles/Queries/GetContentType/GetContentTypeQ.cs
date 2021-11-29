using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Application.AppFiles.Queries.GetContentType
{
    public class GetContentTypeQ : IRequest<string>
    {
        public string FilePath { get; set; }
    }
}
