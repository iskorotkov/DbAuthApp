using System;
using System.Linq;

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

        public string Next() =>
            new string(Enumerable.Repeat(0, _length)
                .Select(_ => (char)_random.Next(256))
                .ToArray());
    }
}
