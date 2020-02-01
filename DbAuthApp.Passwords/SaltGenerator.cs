using System;

namespace DbAuthApp.Passwords
{
    public class SaltGenerator
    {
        private readonly int _length;
        private readonly Random _random = new Random();

        public SaltGenerator(int length = 16)
        {
            _length = length;
        }

        public byte[] Next()
        {
            var bytes = new byte[_length];
            _random.NextBytes(bytes);
            return bytes;
        }
    }
}
