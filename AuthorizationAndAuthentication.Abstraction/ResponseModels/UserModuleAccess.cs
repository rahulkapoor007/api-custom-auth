using AuthorizationAndAuthentication.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Services.Abstraction.ResponseModels
{
    public class UserModuleAccess
    {
        public int UserModuleAccessId { get; set; }
        public int UserModuleId { get; set; }
        public int UserRoleId { get; set; }
        public long AccessLevel { get; set; }
        public bool NoAccess => AccessLevel == 0;
        public bool CanRead => CanAccess(UserAccessLevel.Read);
        public bool CanWrite => CanAccess(UserAccessLevel.Write);
        public bool CanEdit => CanAccess(UserAccessLevel.Edit);
        public bool CanDelete => CanAccess(UserAccessLevel.Delete);
        public bool CanAccess(UserAccessLevel type) => (AccessLevel | (long)type) == AccessLevel;
        public UserModule UserModule { get; set; }
    }
}
