using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Repositories.Context.Entites
{
    public class SPCommonResponseWithIdentity<T>
    {
        public int ErrorNo { get; set; }
        public string Message { get; set; }
        public T Identity { get; set; }
    }
}
