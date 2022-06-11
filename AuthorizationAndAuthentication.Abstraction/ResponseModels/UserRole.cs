using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Services.Abstraction.ResponseModels
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte Status { get; set; }
        public bool IsActive { get; set; }
        public HashSet<UserModuleAccess> UserModuleAccesses { get; set; } = new HashSet<UserModuleAccess>(new UserModuleAccessComparer());

    }
    #region Comparator Implementations
    public class UserModuleAccessComparer : EqualityComparer<UserModuleAccess>
    {
        public override bool Equals(UserModuleAccess x, UserModuleAccess y)
        {
            return (x.UserModuleAccessId == y.UserModuleAccessId) && (x.UserModuleId == x.UserModuleId) && (x.UserRoleId == y.UserRoleId);
        }

        public override int GetHashCode(UserModuleAccess x)
        {
            return x.UserModuleAccessId.GetHashCode() ^ x.UserModuleId.GetHashCode() ^ x.UserRoleId.GetHashCode();
        }
    }

    #endregion
}
