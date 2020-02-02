using NUnit.Framework;

namespace DbAuthApp.Login.Tests
{
    public class LoginProcessingTests
    {
        [TestCase("username")]
        [TestCase("username123456")]
        [TestCase("user name")]
        [TestCase("user name 20 20")]

        public void NoChangeLogin(string login)
        {
            Assert.AreEqual(login, new LoginProcessor().RemoveWhitespaces(login));
        }

        [TestCase("user name", "\t\nuser name    ")]
        public void TrimSpaces(string expected, string login)
        {
            Assert.AreEqual(expected, new LoginProcessor().RemoveWhitespaces(login));
        }

        [TestCase("user name", "user  name")]
        [TestCase("user name", "user\tname")]
        [TestCase("user name", "user\r\nname\n\n")]
        public void ReplaceWithSingleSpace(string expected, string login)
        {
            Assert.AreEqual(expected, new LoginProcessor().RemoveWhitespaces(login));
        }
    }
}
