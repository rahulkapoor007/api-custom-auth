using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Services.Abstraction.ResponseModels
{
    public class AuthToken
    {
        public string Token { get; protected set; }
        public long Expiration { get; protected set; }
        public AuthToken()
        {

        }
        public AuthToken(string token, long expiration)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Invalid token.");

            if (expiration <= 0)
                throw new ArgumentException("Invalid expiration.");

            Token = token;
            Expiration = expiration;
        }

        public bool IsExpired() => DateTime.UtcNow.Ticks > Expiration;
    }
}
