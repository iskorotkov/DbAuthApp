using NUnit.Framework;

namespace DbAuthApp.Login.Tests
{
    public class LoginCheckingTests
    {
        [Test]
        public void CorrectLoginsWithoutWhitespaces()
        {
            var util = new LoginChecker();
            Assert.IsTrue(util.IsCorrect("username"));
            Assert.IsTrue(util.IsCorrect("имяпользователя"));
            Assert.IsTrue(util.IsCorrect("123имя7890"));
            Assert.IsTrue(util.IsCorrect("12345678"));
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

        [Test]
        public void IncorrectLogins()
        {
            var util = new LoginChecker();
            Assert.IsFalse(util.IsCorrect("//"));
            Assert.IsFalse(util.IsCorrect("@username"));
            Assert.IsFalse(util.IsCorrect("user_name"));
            Assert.IsFalse(util.IsCorrect("~username"));
        }
    }
}
