using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Common.Generics
{
    public class GenericResponse : IResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public object Data { get; set; } = new object();

    }
    public class GenericResponse<TModel> : IGenericResponse<TModel> where TModel : new()
    {
        public GenericResponse()
        {
            Data = new TModel();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public TModel Data { get; set; }

    }
}
