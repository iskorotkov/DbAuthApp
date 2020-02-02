using System.Text.RegularExpressions;

namespace DbAuthApp.Login
{
    public class LoginChecker
    {
        private const string LoginPattern = @"^[a-zA-Zа-яА-Я0-9ё`!@#$%^&*()_+|\-=\\{}\[\]:"";'<>?,./№~]+$";

        public bool IsCorrect(string login) => Regex.IsMatch(login, LoginPattern);
    }
}
