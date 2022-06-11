using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Common.Enums
{
    public enum UserAccessLevel
    {
        NoAccess = 0,
        Read = 1,
        Write = 2,
        Edit = 4,
        Delete = 8
    }
}
