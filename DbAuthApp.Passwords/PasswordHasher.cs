using System.Collections.Generic;
using System.Linq;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;

namespace DbAuthApp.Passwords
{
    public class PasswordHasher
    {
        private const int BitLength = 512;
        private readonly Sha3Digest _hashAlgorithm = new Sha3Digest(BitLength);

        public byte[] Hash(string password, IEnumerable<byte> salt)
        {
            var bytes = Encoding.ASCII.GetBytes(password).ToList();
            bytes.AddRange(salt);
            
            _hashAlgorithm.BlockUpdate(bytes.ToArray(), 0, bytes.Count);
            var result = new byte[BitLength / 8];
            _hashAlgorithm.DoFinal(result, 0);

            return result;
        }
    }
}
