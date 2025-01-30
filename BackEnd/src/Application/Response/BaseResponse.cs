using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Application.Configurations;

namespace Application.Response
{
    public class BaseResponse<T>
    {
        private readonly int _code;

        [JsonConstructor]
        public BaseResponse() => _code = PagedConfiguration.DefaultStatusCode;

        public BaseResponse(T data, int code = PagedConfiguration.DefaultStatusCode, string message = null)
        {
            Data = data;
            Message = message;
            _code = code;
        }

        public T Data { get; set; }
        public string Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess => _code is >= 200 and <= 299;
    }
}