using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Response
{
    public class PagedResponse<T> : BaseResponse<T>
    {
        [JsonConstructor]
        public PagedResponse(T data, int totalCount, int currentPage = 1, int pageSize = Configurations.DefaultPageSize, string message = null)
        : base(data)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PageSize = pageSize;
            Message = message;
        }

        public PagedResponse(T data, int code = Configurations.DefaultStatusCode, string message = null)
        : base(data, code, message)
        {

        }

        public int CurrentPage { get; set; }
        public int TotalPage => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageSize { get; set; } = Configurations.DefaultPageSize;
        public int TotalCount { get; set; }

    }
}