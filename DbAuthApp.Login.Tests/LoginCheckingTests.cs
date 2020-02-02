using NUnit.Framework;

namespace DbAuthApp.Login.Tests
{
    public class LoginCheckingTests
    {
        [TestCase("username")]
        [TestCase("имяпользователя")]
        [TestCase("123имя7890")]
        [TestCase("12345678")]
        [TestCase(@"!@#$%^^&*()_+№;:?/|\/*-")]
        [TestCase("`~ёqй")]
        [TestCase("<>./,")]
        public void CorrectLoginsWithoutWhitespaces(string login)
        {
            Assert.IsTrue(new LoginChecker().IsCorrect(login));
        }

        [TestCase("user name")]
        [TestCase("    user name")]
        [TestCase("  user name    ")]
        [TestCase("user    name")]
        public void CorrectLoginsWithSpaces(string login)
        {
            Assert.IsTrue(new LoginChecker().IsCorrect(login));
        }

        [TestCase("user\t\t\tname")]
        [TestCase("user\rname")]
        [TestCase("user\nname")]
        [TestCase("user\r\nname")]
        [TestCase("   user\tname\r\n")]
        public void CorrectLoginsWithAllWhitespaces(string login)
        {
            Assert.IsTrue(new LoginChecker().IsCorrect(login));
        }

        [TestCase("\x00")]
        [TestCase("\u2200@username")]
        [TestCase("user\u0460_name")]
        public void IncorrectLogins(string login)
        {
            Assert.IsFalse(new LoginChecker().IsCorrect(login));
        }
    }
}
