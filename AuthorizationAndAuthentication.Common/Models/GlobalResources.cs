using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Common.Models
{
    public class GlobalResources
    {
        public int TokenExiprationTime { get; set; }
    }
    public static class GlobalVar
    {
        public static GlobalResources _options;
    }
}
