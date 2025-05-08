using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBackend.Application.Common
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }

        public Response() { }

        public Response(bool success, T? data = default, string? message = null)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        public static Response<T> Ok(T data, string? message = null) =>
            new(true, data, message);

    }
}
