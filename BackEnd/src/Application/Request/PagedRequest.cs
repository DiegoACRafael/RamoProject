using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Configurations;

namespace Application.Request
{
    public abstract class PagedRequest
    {
        public int PageNumber { get; set; } = PagedConfiguration.DefaultPageNamber;
        public int PageSize { get; set; } = PagedConfiguration.DefaultPageSize;
    }
}