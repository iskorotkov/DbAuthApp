using System.Text.RegularExpressions;

namespace DbAuthApp.Login
{
    public class LoginChecker
    {
        private const string LoginPattern = @"^[а-яА-Яa-zA-Z0-9\s]+$";

        public bool IsCorrect(string login)
        {
            return Regex.IsMatch(login, LoginPattern);
        }
    }
}
