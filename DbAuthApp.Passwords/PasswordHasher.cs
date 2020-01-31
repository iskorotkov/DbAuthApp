using System;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;

namespace DbAuthApp.Passwords
{
    public class PasswordHasher
    {
        private readonly Sha3Digest _hashAlgorithm = new Sha3Digest(512);

        public string Hash(string password, string salt)
        {
            var bytes = Encoding.ASCII.GetBytes(password + salt);
            
            _hashAlgorithm.BlockUpdate(bytes, 0, bytes.Length);
            var result = new byte[64]; // 512 : 8
            _hashAlgorithm.DoFinal(result, 0);
            
            var hash = BitConverter.ToString(result);
            return hash.Replace("-", "")
                .ToLowerInvariant();
        }
    }
}
