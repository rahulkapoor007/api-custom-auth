using AuthorizationAndAuthentication.Repositories.Context.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Repositories.Abstraction.Contracts
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetUserByEmailAndPassword(string email, string password);
        Task<List<UserListModel>> GetUserListWithPagination(int pageNumber, int pageSize);
        Task<SPCommonResponse> DeleteUserByUserId(int userId, int LoginUserId);
    }
}
