using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Request
{
    public abstract class PagedRequest
    {
        public int PageNumber { get; set; } = Configurations.DefaultPageNamber;
        public int PageSize { get; set; } = Configurations.DefaultPageSize;
    }
}