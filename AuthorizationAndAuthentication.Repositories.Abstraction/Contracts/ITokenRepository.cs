using AuthorizationAndAuthentication.Common.Enums;
using AuthorizationAndAuthentication.Repositories.Context.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Repositories.Abstraction.Contracts
{
    public interface ITokenRepository
    {
        Task<SPCommonResponseWithIdentity<string>> SaveToken(int userId, long ticks);
        Task<SPCommonResponseWithIdentity<int>> VerifyToken(string token, int tokenExpiration, ModuleAccess moduleAccess, UserAccessLevel accessLevel);
        Task<SPCommonResponse> ArchieveToken(string token);
    }
}
