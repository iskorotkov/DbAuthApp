using NUnit.Framework;

namespace DbAuthApp.Login.Tests
{
    public class LoginProcessingTests
    {
        [Test]
        public void NoChangeLogin()
        {
            var proc = new LoginProcessor();
            Assert.AreEqual("username", proc.RemoveWhitespaces("username"));
            Assert.AreEqual("username123456", proc.RemoveWhitespaces("username123456"));
            Assert.AreEqual("user name", proc.RemoveWhitespaces("user name"));
            Assert.AreEqual("user name 20 20", proc.RemoveWhitespaces("user name 20 20"));
        }

        [Test]
        public void TrimSpaces()
        {
            var proc = new LoginProcessor();
            Assert.AreEqual("user name", proc.RemoveWhitespaces("\t\nuser name    "));
        }

        [Test]
        public void ReplaceWithSingleSpace()
        {
            var proc = new LoginProcessor();
            Assert.AreEqual("user name", proc.RemoveWhitespaces("user  name"));
            Assert.AreEqual("user name", proc.RemoveWhitespaces("user\tname"));
            Assert.AreEqual("user name", proc.RemoveWhitespaces("user\r\nname\n\n"));
        }
    }
}
