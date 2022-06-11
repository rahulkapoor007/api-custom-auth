using AuthorizationAndAuthentication.Services.Abstraction.RequestModels;
using AuthorizationAndAuthentication.Services.Abstraction.ResponseModels;
using AuthorizationAndAuthentication.Common.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Services.Abstraction.Contracts
{
    public interface IUserService
    {
        Task<IGenericResponse<User>> GetUserByEmailAndPassword(LoginRequest model);
        Task<IGenericResponse<UserListResponse>> GetUserListWithPagination(int pageNumber, int pageSize);
        Task<IGenericResponse<DeleteResponse>> DeleteUser(int userId, int LoginUserId);
    }
}
