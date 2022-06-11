using AuthorizationAndAuthentication.ActionFilters;
using AuthorizationAndAuthentication.Common.Enums;
using AuthorizationAndAuthentication.Common.Generics;
using AuthorizationAndAuthentication.Services.Abstraction.Contracts;
using AuthorizationAndAuthentication.Services.Abstraction.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAndAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        private readonly IUserService _userService;

        public DashboardController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(UserListResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserListResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserListResponse), StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { ModuleAccess.DashboardManagement, UserAccessLevel.Read })]
        [Route("UserList")]
        public async Task<IActionResult> GetUsersList([FromQuery] int PageNumber, int PageSize)
        {
            var result = await _userService.GetUserListWithPagination(PageNumber, PageSize);

            return result.ToHttpResponse();
        }
        [HttpDelete]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(DeleteResponse), StatusCodes.Status401Unauthorized)]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new object[] { ModuleAccess.DashboardManagement, UserAccessLevel.Delete })]
        [Route("User")]
        public async Task<IActionResult> DeleteUsersList([FromQuery] int UserId)
        {
            var result = await _userService.DeleteUser(UserId, this.UserId);

            return result.ToHttpResponse();
        }
    }
}
