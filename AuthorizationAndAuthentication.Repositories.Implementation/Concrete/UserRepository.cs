using AuthorizationAndAuthentication.Common;
using AuthorizationAndAuthentication.Repositories.Abstraction.Contracts;
using AuthorizationAndAuthentication.Repositories.Context.Context;
using AuthorizationAndAuthentication.Repositories.Context.Entites;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthorizationAndAuthentication.Common.Exceptions;
using Microsoft.Data.SqlClient;

namespace AuthorizationAndAuthentication.Repositories.Implementation.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlContext _sqlContext;

        public UserRepository(SqlContext sqlContext)
        {
            this._sqlContext = sqlContext;
        }
        public async Task<List<UserModel>> GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@email", email));
                sqlParameters.Add(new SqlParameter("@password", password));

                var data = _sqlContext.Users.FromSqlRaw("USP_User_GetUserByEmailAndPass @email,@password", sqlParameters.ToArray()).ToList();
                return data;

            }
            catch (Exception ex)
            {
                throw new ApplicationErrorException(ex);
            }
        }

        public async Task<List<UserListModel>> GetUserListWithPagination(int pageNumber, int pageSize)
        {
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@Page_Index_int", pageNumber));
                sqlParameters.Add(new SqlParameter("@Page_Size_int", pageSize));

                var data =await _sqlContext.UsersList.FromSqlRaw("USP_User_GetAllUsersWithPagintion @Page_Index_int,@Page_Size_int", sqlParameters.ToArray()).ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw new ApplicationErrorException(ex);
            }
        }
        public async Task<SPCommonResponse> DeleteUserByUserId(int userId, int LoginUserId)
        {
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@UserId", userId));
                sqlParameters.Add(new SqlParameter("@ModifiedBy", LoginUserId));

                var data = await _sqlContext.SpResponse.FromSqlRaw("USP_User_DeleteUserByUserid @UserId, @ModifiedBy", sqlParameters.ToArray()).ToListAsync();
                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ApplicationErrorException(ex);
            }
        }
    }
}
