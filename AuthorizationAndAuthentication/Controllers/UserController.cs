using AuthorizationAndAuthentication.Services.Abstraction.Contracts;
using AuthorizationAndAuthentication.Services.Abstraction.RequestModels;
using AuthorizationAndAuthentication.Services.Abstraction.ResponseModels;
using AuthorizationAndAuthentication.ActionFilters;
using AuthorizationAndAuthentication.Common.Enums;
using AuthorizationAndAuthentication.Common.Generics;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAndAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            this._userService = userService;
            this._authenticationService = authenticationService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(AccessToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AccessToken), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AccessToken), StatusCodes.Status401Unauthorized)]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var result = await _userService.GetUserByEmailAndPassword(model);
            if (result.Success)
            {
                var loginResponse = await _authenticationService.Login(result.Data);
                return loginResponse.ToHttpResponse();
            }
            return result.ToHttpResponse();
        }

        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { ModuleAccess.UserManagement, UserAccessLevel.Write })]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            const string BEARER_PREFIX = "Bearer ";
            string token = AuthorizationToken.Substring(BEARER_PREFIX.Length);
            var result = await _authenticationService.Logout(token);
            return result.ToHttpResponse();
        }
    }
}
