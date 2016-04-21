using System;
using System.Security.Cryptography;
using System.Text;

namespace securityMandatory2.Models
{
    public class Hasher 
    {
        public int SaltSize { get; set; } = 4;

        public string Encrypt(string original)
        {
            // Konverter strengen oprindelige value til en byte array
            var originalData = Encoding.UTF8.GetBytes(original);

            //Opret en 4-byte salt ved hjælp af en cryptographically secure random number generator
            var saltData = new byte[SaltSize];
            var rng = new RNGCryptoServiceProvider();
            rng.GetNonZeroBytes(saltData);

            // Tiløj salt til enden af den original streng
            var saltedPasswordData = new byte[originalData.Length + saltData.Length];
            Array.Copy(originalData, 0, saltedPasswordData, 0, originalData.Length);
            Array.Copy(saltData, 0, saltedPasswordData, originalData.Length, saltData.Length);

            var sha = new SHA1Managed();
            var hashData = sha.ComputeHash(saltedPasswordData);

            var hashSaltData = new byte[hashData.Length + saltData.Length];
            Array.Copy(hashData, 0, hashSaltData, 0, hashData.Length);
            Array.Copy(saltData, 0, hashSaltData, hashData.Length, saltData.Length);

            return Convert.ToBase64String(hashSaltData);
        }

        public bool CompareStringToHash(string s, string hash)
        {
            var hashData = Convert.FromBase64String(hash);

            // Først, pluk fire-byte salt fra slutningen af enden af hash
            var saltData = new byte[SaltSize];
            Array.Copy(hashData, hashData.Length - saltData.Length, saltData, 0, saltData.Length);

            var passwordData = Encoding.UTF8.GetBytes(s);

            // Tiløj salt til enden af den original streng
            var saltedPasswordData = new byte[passwordData.Length + saltData.Length];
            Array.Copy(passwordData, 0, saltedPasswordData, 0, passwordData.Length);
            Array.Copy(saltData, 0, saltedPasswordData, passwordData.Length, saltData.Length);

            // Skab en ny SHA-1 instance og udregn hash 
            var sha = new SHA1Managed();
            var newHashData = sha.ComputeHash(saltedPasswordData);

            //  Tiløj salt til enden af den original hash for den gemte værdi
            var newHashSaltData = new byte[newHashData.Length + saltData.Length];
            Array.Copy(newHashData, 0, newHashSaltData, 0, newHashData.Length);
            Array.Copy(saltData, 0, newHashSaltData, newHashData.Length, saltData.Length);
            // Returner sammenligningen
            return (Convert.ToBase64String(hashData).Equals(Convert.ToBase64String(newHashSaltData)));
        }
    }
}