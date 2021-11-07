using System;
using System.Collections.Generic;
using System.Text;

namespace PsyPersonServer.Domain.Models.PagedResponse
{
    public class PagedResponse<T>
    {
        public PagedResponse(IEnumerable<T> data, int total)
        {
            Data = data;
            Total = total;
        }
        public IEnumerable<T> Data { get; set; }
        public int Total { get; set; }
    }
}
