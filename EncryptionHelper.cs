using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


//* *************** Not used but might use later ***********************

public static class EncryptionHelper
{
    public static string Encrypt(string plainText, string password)
    {
        byte[] salt = GenerateRandomSalt();
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        Rfc2898DeriveBytes passwordDeriveBytes = new Rfc2898DeriveBytes(password, salt, 10000);

        byte[] keyBytes = passwordDeriveBytes.GetBytes(32);
        byte[] ivBytes = passwordDeriveBytes.GetBytes(16);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(salt, 0, salt.Length);

                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }

                byte[] encryptedBytes = memoryStream.ToArray();
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    public static string Decrypt(string cipherText, string password)
    {
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

        byte[] salt = new byte[16];
        Buffer.BlockCopy(cipherTextBytes, 0, salt, 0, salt.Length);

        byte[] encryptedBytes = new byte[cipherTextBytes.Length - salt.Length];
        Buffer.BlockCopy(cipherTextBytes, salt.Length, encryptedBytes, 0, encryptedBytes.Length);

        Rfc2898DeriveBytes passwordDeriveBytes = new Rfc2898DeriveBytes(password, salt, 10000);
        byte[] keyBytes = passwordDeriveBytes.GetBytes(32);
        byte[] ivBytes = passwordDeriveBytes.GetBytes(16);

        using (Aes aes = Aes.Create())
        {
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }

                byte[] decryptedBytes = memoryStream.ToArray();
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }

    private static byte[] GenerateRandomSalt()
    {
        byte[] salt = new byte[16];
        using (RNGCryptoServiceProvider randomNumberGenerator = new RNGCryptoServiceProvider())
        {
            randomNumberGenerator.GetBytes(salt);
        }
        return salt;
    }

    // this has a static path
    private static string GetOPENAPIKeyFromDiskEncrypted(string pwd)
    {
        return Decrypt(File.ReadAllText(@"c:\OpenAIKeyEncrypted.txt"), pwd);
    }


    // I'm a bozo, so I want to load the API key from a file outside the git repo
    public static string GetOPENAPIKeyFromDisk()
    {
        return File.ReadAllText(@"d:\OpenAIKeyPlain.txt");
    }


    // I'm a bozo, so I want to load the API key from a file outside the git repo
    public static string GetElevelLabsAPIKeyFromDisk()
    {
        return File.ReadAllText(@"d:\ElevenLabsKeyPlain.txt");
    }
}
