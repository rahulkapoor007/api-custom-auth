using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Common.Generics
{
    public interface IResponse
    {
        HttpStatusCode StatusCode { get; set; }
        bool Success { get; set; }
        string Message { get; set; }
    }
}
