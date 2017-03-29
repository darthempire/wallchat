using System;
using System.Security.Cryptography;
using System.Text;

namespace wallchat.Helpers.Cryptography
{
    public static class SHA256
    {
        public static string GetHash ( string input )
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider( );

            var byteValue = Encoding.UTF8.GetBytes (input);

            var byteHash = hashAlgorithm.ComputeHash (byteValue);

            return Convert.ToBase64String (byteHash);
        }
    }
}