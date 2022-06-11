using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Common.Generics
{
    public static class HttpResponses
    {
        public static IActionResult ToHttpResponse<TModel>(this IGenericResponse<TModel> response)
        {
            return new OkObjectResult(response) { StatusCode = (int)response.StatusCode };
        }

        public static IActionResult ToHttpResponse(this IResponse response)
        {
            return new OkObjectResult(response) { StatusCode = (int)response.StatusCode };
        }
    }
}
