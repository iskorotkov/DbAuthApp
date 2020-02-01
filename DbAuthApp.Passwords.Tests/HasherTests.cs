using NUnit.Framework;

namespace DbAuthApp.Passwords.Tests
{
    public class HasherTests
    {
        [Test]
        public void UnequalInputKnownSalt()
        {
            var hasher = new PasswordHasher();
            var s1 = hasher.Hash("test", new byte[] {1, 2, 3});
            var s2 = hasher.Hash("test_test", new byte[] {1, 2, 4});
            Assert.AreNotEqual(s1, s2);
        }

        [Test]
        public void EqualInputKnownSalt()
        {
            var hasher = new PasswordHasher();
            var s1 = hasher.Hash("abc", new byte[] {1, 2, 3});
            var s2 = hasher.Hash("abc", new byte[] {1, 2, 3});
            Assert.AreEqual(s1, s2);
        }
    }
}
