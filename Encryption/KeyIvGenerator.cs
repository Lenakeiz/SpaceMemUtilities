using System;
using System.Security.Cryptography;

namespace SpaceMem.Encryption
{ 
  /// <summary>
  /// Use this class to generate a key and iv for use with the encryption and decryption classes.
  /// GenerateKeyAndIv() will print the key and iv to the console.
  /// Save the key and iv to a file or some other storage.
  /// </summary>
    public class KeyIvGenerator
    {
        public static void GenerateKeyAndIv()
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                string base64Key = Convert.ToBase64String(aes.Key);
                string base64Iv = Convert.ToBase64String(aes.IV);

                Console.WriteLine($"Key: {base64Key}");
                Console.WriteLine($"IV: {base64Iv}");
            }
        }
    }
}
   