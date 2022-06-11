using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Services.Abstraction.ResponseModels
{
    public class UserListResponse
    {
        public List<UserList> List { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int ResultCount { get; set; }
    }
    public class UserList
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<Role> Roles { get; set; }
        public string Email { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
