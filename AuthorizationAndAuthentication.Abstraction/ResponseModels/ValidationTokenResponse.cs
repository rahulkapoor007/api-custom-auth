using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Services.Abstraction.ResponseModels
{
    public class ValidationTokenResponse
    {
        public int ErrorNo { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }
}
