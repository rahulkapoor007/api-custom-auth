using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Repositories.Context.Entites
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int RoleId { get; set; }
        public bool IsActiveUserRole { get; set; }
        public byte StatusUserRole { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsActiveRole { get; set; }
        public byte StatusRole { get; set; }
        public int UserModuleAccessId { get; set; }
        public int ModuleId { get; set; }
        public byte AccessLevel { get; set; }
        public byte AccessStatus { get; set; }
        public bool AccessIsActive { get; set; }
        public string ModuleName { get; set; }
    }
}
