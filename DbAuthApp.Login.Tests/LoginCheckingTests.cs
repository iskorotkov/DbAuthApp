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
            var util = new LoginChecker();
            Assert.IsTrue(util.IsCorrect(login));
        }

        [Test]
        public void CorrectLoginsWithSpaces()
        {
            var util = new LoginChecker();
            Assert.IsTrue(util.IsCorrect("user name"));
            Assert.IsTrue(util.IsCorrect("    user name"));
            Assert.IsTrue(util.IsCorrect("  user name    "));
            Assert.IsTrue(util.IsCorrect("user    name"));
        }

        [Test]
        public void CorrectLoginsWithAllWhitespaces()
        {
            var util = new LoginChecker();
            Assert.IsTrue(util.IsCorrect("user\t\t\tname"));
            Assert.IsTrue(util.IsCorrect("user\rname"));
            Assert.IsTrue(util.IsCorrect("user\nname"));
            Assert.IsTrue(util.IsCorrect("user\r\nname"));
            Assert.IsTrue(util.IsCorrect("   user\tname\r\n"));
        }

        [TestCase("\x00")]
        [TestCase("\u2200@username")]
        [TestCase("user\u0460_name")]
        public void IncorrectLogins(string login)
        {
            var util = new LoginChecker();
            Assert.IsFalse(util.IsCorrect(login));
        }
    }
}
