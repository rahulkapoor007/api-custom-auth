using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Services.Abstraction.ResponseModels
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<UserRole> Roles { get; set; }
        public string Email { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
