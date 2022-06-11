using AuthorizationAndAuthentication.Services.Abstraction.Contracts;
using AuthorizationAndAuthentication.Common.Constants;
using AuthorizationAndAuthentication.Common.Enums;
using AuthorizationAndAuthentication.Common.Generics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AuthorizationAndAuthentication.ActionFilters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly ITokenService _tokenService;
        private readonly UserAccessLevel _accessLevel;
        private readonly ModuleAccess _moduleAccess;
        private const string BEARER_PREFIX = "Bearer ";
        public AuthorizationFilter(ITokenService tokenService, 
            UserAccessLevel accessLevel, ModuleAccess moduleAccess)
        {
            this._tokenService = tokenService;
            this._accessLevel = accessLevel;
            this._moduleAccess = moduleAccess;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var result = true;
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                result = false;

            string token = context.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value;

            if (string.IsNullOrEmpty(token))
                result = false;
            else
            {
                try
                {
                    token = token.Substring(BEARER_PREFIX.Length);
                    var res = _tokenService.ValidateToken(token, _moduleAccess, _accessLevel).ConfigureAwait(false).GetAwaiter().GetResult();
                    if (res != null)
                    {
                        context.HttpContext.Items["UserId"] = res.Data.UserId;
                        if (res.Data.ErrorNo == 0)
                            return;
                        if (res.Data.ErrorNo > 0)
                        {
                            if (res.Data.ErrorNo == (int)HttpStatusCode.Forbidden)
                            {
                                context.Result = new ObjectResult(new ErrorResponse(SystemMessages.FORBIDDEN, HttpStatusCode.Forbidden)) { StatusCode = (int)HttpStatusCode.Forbidden };
                                return;
                            }
                            context.Result = new UnauthorizedObjectResult(new ErrorResponse(SystemMessages.UNAUTHORIZED_USER, HttpStatusCode.Unauthorized));
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    context.Result = new UnauthorizedObjectResult(new ErrorResponse(ex.Message, HttpStatusCode.Unauthorized));
                    return;
                }
            }
            if (!result)
            {
                context.Result = new UnauthorizedObjectResult(new ErrorResponse(SystemMessages.UNAUTHORIZED_USER, HttpStatusCode.Unauthorized));
            }
        }
    }
}
