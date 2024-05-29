using System.Configuration;

namespace universal_windows_platform_cs.Core.Services
{
    public class AccountUser
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }

    public static class AuthCoreService
    {
        public static AccountUser GetInfo()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string username = appSettings["username"].ToString();
            string password = appSettings["password"].ToString();
            string email = appSettings["email"].ToString();
            return new AccountUser()
            {
                username = username,
                password = password,
                email = email,
            };
        }

        public static bool LoginByUser(string username, string password)
        {
            AccountUser account = GetInfo();
            if (Equals(account.username, username) && Equals(account.password, password))
            {
                return true;
            }
            return false;
        }
        public static bool GetUserByUserName(string username)
        {
            var appSettings = ConfigurationManager.AppSettings;
            if (username == appSettings["username"].ToString())
                return true; return false;
        }
    }
}
