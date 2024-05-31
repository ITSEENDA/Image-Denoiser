using System;
using System.Text;
using System.Security.Cryptography;

namespace PROTO.Tools
{
    internal static class Hasher
    {
        //private const int keySize = 64;
        //private const int iterations = 35000;
        
        //public static byte[] GenerateSalt()
        //{
        //    var rand = RandomNumberGenerator.Create();
        //    var salt = new byte[keySize];
        //    rand.GetBytes(salt);
        //    return salt;
        //}
        //public static string HashPassword(string password, byte[] salt)
        //{
        //    var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 
        //        iterations, HashAlgorithmName.SHA512);
        //    var key = pbkdf2.GetBytes(64);
        //    return Convert.ToBase64String(key);
        //}
        //public static bool VerifyPassword(string password, string basePassword, byte[] salt)
        //{
        //    var pbkdf2 = new Rfc2898DeriveBytes(password, salt,
        //        iterations, HashAlgorithmName.SHA512);
        //    var key = Convert.ToBase64String(pbkdf2.GetBytes(64));
        //    return string.Equals(key, basePassword);
        //}
        public static string HashPassword(string password)
        {
            using var md5 = MD5.Create();
            var key = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(key);
        }
        public static bool VerifyPassword(string password, string basePassword)
        {
            var hash = HashPassword(password);
            return string.Equals(hash, basePassword);
        }
    }
}
