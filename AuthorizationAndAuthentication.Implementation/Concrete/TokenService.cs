using AuthorizationAndAuthentication.Services.Abstraction.Contracts;
using AuthorizationAndAuthentication.Services.Abstraction.ResponseModels;
using AuthorizationAndAuthentication.Common.Enums;
using AuthorizationAndAuthentication.Common.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationAndAuthentication.Common.Exceptions;
using AuthorizationAndAuthentication.Common.Constants;
using System.Net;
using AuthorizationAndAuthentication.Common.Extensions;
using AuthorizationAndAuthentication.Repositories.Abstraction.Contracts;
using AuthorizationAndAuthentication.Common.Models;

namespace AuthorizationAndAuthentication.Services.Implementation.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            this._tokenRepository = tokenRepository;
        }
        public async Task<IGenericResponse<SuccessResponse>> ArchieveTokenForLogout(string token)
        {
            var response = new GenericResponse<SuccessResponse>();
            try
            {
                response.Data = new SuccessResponse();
                var result = await _tokenRepository.ArchieveToken(token);
                if (result == null)
                {
                    response.SetError(SystemMessages.LOGOUT_FAILED, HttpStatusCode.BadRequest);
                    return response;
                }
                if (result.ErrorNo > 0)
                {
                    string message = ConstantMessages.messages.ContainsKey(result.ErrorNo) ? ConstantMessages.messages[result.ErrorNo] : "";
                    response.SetError(message, HttpStatusCode.BadRequest);
                    return response;
                }
                response.Data.Success = true;
                response.SetSuccess(SystemMessages.SUCCESS, HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationErrorException) throw;
                else throw new ApplicationErrorException(ex);
            }
        }

        public async Task<IGenericResponse<AccessToken>> GenerateToken(User user)
        {
            var response = new GenericResponse<AccessToken>();
            try
            {
                var accessTokenExpiration = DateTime.UtcNow.AddMinutes(GlobalVar._options.TokenExiprationTime).Ticks;
                var result = await _tokenRepository.SaveToken(user.UserId, accessTokenExpiration);
                if (result == null)
                {
                    response.SetError(SystemMessages.TOKEN_VALIDATION_FAILED, HttpStatusCode.BadRequest);
                    return response;
                }
                if (result.ErrorNo > 0)
                {
                    string message = ConstantMessages.messages.ContainsKey(result.ErrorNo) ? ConstantMessages.messages[result.ErrorNo] : "";
                    response.SetError(message, HttpStatusCode.BadRequest);
                    return response;
                }
                response.Data = new AccessToken(result.Identity, accessTokenExpiration, user);
                response.SetSuccess(SystemMessages.SUCCESS, HttpStatusCode.OK);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is ApplicationErrorException) throw;
                else throw new ApplicationErrorException(ex);
            }
        }

        public async Task<IGenericResponse<ValidationTokenResponse>> ValidateToken(string token, ModuleAccess moduleAccess, UserAccessLevel accessLevel)
        {
            var response = new GenericResponse<ValidationTokenResponse>();
            try
            {
                var result = await _tokenRepository.VerifyToken(token, GlobalVar._options.TokenExiprationTime, moduleAccess, accessLevel);
                if (result == null)
                {
                    response.SetError(SystemMessages.TOKEN_VALIDATION_FAILED, HttpStatusCode.BadRequest);
                    return response;
                }
                response.Data = new ValidationTokenResponse()
                {
                    ErrorNo = result.ErrorNo,
                    Message = result.Message,
                    UserId = result.Identity
                };
                if (result.ErrorNo > 0)
                {
                    if (result.ErrorNo == (int)HttpStatusCode.Forbidden)
                    {
                        response.SetError(SystemMessages.FORBIDDEN, HttpStatusCode.Forbidden);
                        return response;
                    }
                    else
                    {
                        string message = ConstantMessages.messages.ContainsKey(result.ErrorNo) ? ConstantMessages.messages[result.ErrorNo] : "";
                        response.SetError(message, HttpStatusCode.BadRequest);
                        return response;
                    }
                }
                response.SetSuccess(SystemMessages.SUCCESS, HttpStatusCode.OK);
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
