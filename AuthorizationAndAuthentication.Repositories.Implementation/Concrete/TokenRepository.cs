using AuthorizationAndAuthentication.Common.Enums;
using AuthorizationAndAuthentication.Common.Exceptions;
using AuthorizationAndAuthentication.Common.Models;
using AuthorizationAndAuthentication.Repositories.Abstraction.Contracts;
using AuthorizationAndAuthentication.Repositories.Context.Context;
using AuthorizationAndAuthentication.Repositories.Context.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace AuthorizationAndAuthentication.Repositories.Implementation.Concrete
{
    public class TokenRepository : ITokenRepository
    {
        private readonly SqlContext _sqlContext;

        public TokenRepository(SqlContext sqlContext)
        {
            this._sqlContext = sqlContext;
        }
        public async Task<SPCommonResponse> ArchieveToken(string token)
        {
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@Token", token));

                var data = await _sqlContext.SpResponse.FromSqlRaw("USP_User_Logout @Token", sqlParameters.ToArray()).ToListAsync();
                return data.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new ApplicationErrorException(ex);
            }
        }

        public async Task<SPCommonResponseWithIdentity<string>> SaveToken(int userId, long ticks)
        {
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@UserId", userId));
                sqlParameters.Add(new SqlParameter("@Ticks", ticks));

                var data = await _sqlContext.SpResponseString.FromSqlRaw("USP_User_InsertAuthToken @UserId,@Ticks", sqlParameters.ToArray()).ToListAsync();
                return data.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new ApplicationErrorException(ex);
            }
        }

        public async Task<SPCommonResponseWithIdentity<int>> VerifyToken(string token, int tokenExpiration, ModuleAccess moduleAccess, UserAccessLevel accessLevel)
        {
            try
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();

                sqlParameters.Add(new SqlParameter("@Token", token));
                sqlParameters.Add(new SqlParameter("@minutes", tokenExpiration));
                sqlParameters.Add(new SqlParameter("@ModuleAccess", moduleAccess));
                sqlParameters.Add(new SqlParameter("@AccessLevel", accessLevel));

                var data = await _sqlContext.SpResponseInteger.FromSqlRaw("USP_User_VerifyAuthToken @Token,@minutes,@ModuleAccess,@AccessLevel", sqlParameters.ToArray()).ToListAsync();
                return data.FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new ApplicationErrorException(ex);
            }
        }
    }
}
