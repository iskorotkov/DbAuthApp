using NUnit.Framework;

namespace DbAuthApp.Passwords.Tests
{
    public class HasherTests
    {
        [Test]
        public void UnequalInputKnownSalt()
        {
            var hasher = new PasswordHasher();
            var s1 = hasher.Hash("test", "xyz");
            var s2 = hasher.Hash("test_test", "xyz");
            Assert.AreNotEqual(s1, s2);
        }

        [Test]
        public void EqualInputKnownSalt()
        {
            var hasher = new PasswordHasher();
            var s1 = hasher.Hash("abc", "xyz");
            var s2 = hasher.Hash("abc", "xyz");
            Assert.AreEqual(s1, s2);
        }
    }
}
