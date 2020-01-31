using Zxcvbn;

namespace DbAuthApp.Passwords
{
    public class PasswordChecker
    {
        private readonly Zxcvbn.Zxcvbn _zx = new Zxcvbn.Zxcvbn();

        public bool IsStrong(string password, string login = null, int requiredStrengthLevel = 3)
        {
            return EvaluatePassword(password, login).Score >= requiredStrengthLevel;
        }

        public Result EvaluatePassword(string password, string login = null)
        {
            return _zx.EvaluatePassword(password, new[] {login});
        }
    }
}
