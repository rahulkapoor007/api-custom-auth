using AuthorizationAndAuthentication.Common.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Common.Extensions
{
    public static class ResponseExtensions
    {
        public static void SetError(this IResponse response, Exception ex)
        {
            response.Success = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            if (ex is not ApplicationException)
            {
                response.Message = "There was an internal error, please contact to technical support.";
            }
            else
            {
                response.Message = ex.Message;
            }
        }

        public static void SetError(this IResponse response, string errorMessage, HttpStatusCode statusCode)
        {
            response.Success = false;
            response.StatusCode = statusCode;
            response.Message = errorMessage;
        }

        public static void SetSuccess(this IResponse response, string successMessage, HttpStatusCode statusCode)
        {
            response.Success = true;
            response.StatusCode = statusCode;
            response.Message = successMessage;
        }

        public static void SetAuthorizationError(this IResponse response, string unauthorizeMessage)
        {
            response.Success = false;
            response.StatusCode = HttpStatusCode.Unauthorized;
            response.Message = unauthorizeMessage;
        }
    }
}
