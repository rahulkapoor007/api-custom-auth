using AuthorizationAndAuthentication.Services.Abstraction.ResponseModels;
using AuthorizationAndAuthentication.Common.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Services.Abstraction.Contracts
{
    public interface IAuthenticationService
    {
        Task<IGenericResponse<AccessToken>> Login(User model);
        Task<IGenericResponse<SuccessResponse>> Logout(string token);
        Task<IGenericResponse<AccessToken>> CreateAccessTokenWithEmailAsync(string EmailId);
    }
}
