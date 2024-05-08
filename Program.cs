using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Hashing
{
    class Program
    {
        static void Main(string[] args)
        {
            string OriginalText = "Hola holita vecinito";
            byte[] key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef");
            byte[] iv = Encoding.UTF8.GetBytes("0123456789abcdef");
            byte[] encrypted = EncryptString(OriginalText, key, iv);
     
            Console.WriteLine("Encrypted: " + Encoding.UTF8.GetString(encrypted));
            string decrypted = DecryptString(encrypted, key, iv);
            Console.WriteLine("Decrypted: " + decrypted);
        }

        static byte[] EncryptString(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return msEncrypt.ToArray();
                }
            }
        }

        static string DecryptString(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}