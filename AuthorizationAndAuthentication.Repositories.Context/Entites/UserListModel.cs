using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Repositories.Context.Entites
{
    public class UserListModel
    {
        public long RowNum { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public int CreatedBy { get; set; }
        public int Count { get; set; }
    }
}
