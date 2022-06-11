using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Common.Constants
{
    public static class SystemMessages
    {
        public const string SUCCESS = "Success.";
        public const string FORBIDDEN = "You don't have the permission for this request.";
        public const string UNAUTHORIZED_USER = "You not an authorized user.";
        public const string CHECK_EMAIL_PASSWORD = "Please check the email and password.";
        public const string NO_DATA_FOUND = "No data found.";
        public const string BLOCKED_USER = "User is blocked.";
        public const string TOKEN_VALIDATION_FAILED = "Please send the correct token.";
        public const string LOGIN_SUCCESS = "User login successfully.";
        public const string LOGOUT_SUCCESS = "User logout successfully.";
        public const string LOGOUT_FAILED = "User login failed.";
        public const string ERROR = "Please contact customer support.";
        public const string ACCOUNT_DELETED = "Your account has been deleted.";
    }
    public static class ConstantMessages
    {
        public static IDictionary<int, string> messages = 
            new Dictionary<int, string>{
                                           { 5000, "You not an authorized user." },
                                           { 5001, "Access token has expired." }
                                       };
    }
}
