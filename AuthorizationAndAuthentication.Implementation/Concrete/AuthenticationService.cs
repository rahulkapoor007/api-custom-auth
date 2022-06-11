using AuthorizationAndAuthentication.Services.Abstraction.Contracts;
using AuthorizationAndAuthentication.Services.Abstraction.ResponseModels;
using AuthorizationAndAuthentication.Common.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationAndAuthentication.Common.Constants;
using System.Net;
using AuthorizationAndAuthentication.Common.Extensions;
using AuthorizationAndAuthentication.Common.Exceptions;

namespace AuthorizationAndAuthentication.Services.Implementation.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;

        public AuthenticationService(ITokenService tokenService)
        {
            this._tokenService = tokenService;
        }
        public Task<IGenericResponse<AccessToken>> CreateAccessTokenWithEmailAsync(string EmailId)
        {
            throw new NotImplementedException();
        }

        public async Task<IGenericResponse<AccessToken>> Login(User model)
        {
            var response = new GenericResponse<AccessToken>();
            try
            {
                if (model == null)
                {
                    response.SetError(SystemMessages.CHECK_EMAIL_PASSWORD, HttpStatusCode.OK);
                    return response;
                }

                //User is Blocked or not
                if (model.Status != 1)
                {
                    response.Data = new AccessToken();
                    response.Data.user = model;
                    response.SetSuccess(SystemMessages.BLOCKED_USER, HttpStatusCode.OK);
                    return response;
                }
                //Generate JWT Token
                var token = await _tokenService.GenerateToken(model);

                if (token == null || !token.Success)
                {
                    response.SetError(SystemMessages.TOKEN_VALIDATION_FAILED, HttpStatusCode.BadRequest);
                    return response;
                }
                response.Data = token.Data;
                response.SetSuccess(SystemMessages.LOGIN_SUCCESS, HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationErrorException) throw;
                else throw new ApplicationErrorException(ex);
            }
        }

        public async Task<IGenericResponse<SuccessResponse>> Logout(string token)
        {
            var response = new GenericResponse<SuccessResponse>();
            try
            {
                response.Data = new SuccessResponse();
                var result = await this._tokenService.ArchieveTokenForLogout(token);
                if (result != null && result.Success)
                {
                    response.Data.Success = true;
                    response.SetSuccess(SystemMessages.LOGOUT_SUCCESS, HttpStatusCode.OK);
                    return response;
                }

                response.SetError(SystemMessages.LOGOUT_FAILED, HttpStatusCode.BadRequest);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationErrorException) throw;
                else throw new ApplicationErrorException(ex);
            }
        }
    }
}
