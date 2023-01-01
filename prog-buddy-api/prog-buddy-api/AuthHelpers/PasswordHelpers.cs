using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace prog_buddy_api.AuthHelpers
{
    public static class PasswordHelpers
    {
        const int keySize = 64;

        const int iterations = 350000;

        public static string HashPassword(this string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
            salt,
                iterations,
                HashAlgorithmName.SHA512,
                keySize);

            return Convert.ToHexString(hash);
        }

        public static bool VerifyPassword(this string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA512, keySize);

            return hashToCompare.SequenceEqual(Convert.FromHexString(hash));
        }
    }
}
