using NUnit.Framework;

namespace DbAuthApp.Passwords.Tests
{
    public class SaltGenerationTests
    {
        [Test]
        public void CorrectLength()
        {
            var sg = new SaltGenerator(12);
            Assert.AreEqual(12, sg.Next().Length);
            Assert.AreEqual(12, sg.Next().Length);
        }

        [Test]
        public void SaltIsUnique()
        {
            var sg = new SaltGenerator();
            var s1 = sg.Next();
            var s2 = sg.Next();
            Assert.AreNotEqual(s1, s2);
        }
    }
}
