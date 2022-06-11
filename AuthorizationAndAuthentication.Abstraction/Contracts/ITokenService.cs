using AuthorizationAndAuthentication.Services.Abstraction.ResponseModels;
using AuthorizationAndAuthentication.Common.Enums;
using AuthorizationAndAuthentication.Common.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Services.Abstraction.Contracts
{
    public interface ITokenService
    {
        Task<IGenericResponse<AccessToken>> GenerateToken(User user);
        Task<IGenericResponse<ValidationTokenResponse>> ValidateToken(string token, ModuleAccess moduleAccess, UserAccessLevel accessLevel);
        Task<IGenericResponse<SuccessResponse>> ArchieveTokenForLogout(string token);

    }
}
