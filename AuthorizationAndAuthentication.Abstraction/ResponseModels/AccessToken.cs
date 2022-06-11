using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Services.Abstraction.ResponseModels
{
    public class AccessToken : AuthToken
    {
        public User user { get; set; }
        public AccessToken()
        {

        }
        public AccessToken(string token, long expiration, User user) : base(token, expiration)
        {
            this.user = user;
        }
    }
}
