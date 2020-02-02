using System.Text.RegularExpressions;

namespace DbAuthApp.Login
{
    public class LoginProcessor
    {
        public string RemoveWhitespaces(string login)
        {
            login = login.Trim();
            login = Regex.Replace(login, @"\s{2,}", " ");
            return login;
        }
    }
}
