using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Common.Generics
{
    public class ErrorResponse : IResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public object? Data { get; set; }

        public ErrorResponse(string message, HttpStatusCode statusCode)
        {
            this.Message = message;
            this.StatusCode = statusCode;
        }
    }
}
