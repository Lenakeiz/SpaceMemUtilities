using System;
using System.IO;
using System.Security.Cryptography;

namespace SpaceMem.Encryption
{
    public class AesEncryptor : IEncryptor
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;
        private readonly Aes _aes;

        // Constructor accepting external key and IV
        public AesEncryptor(byte[] key, byte[] iv)
        {
            _key = key ?? throw new ArgumentNullException(nameof(key));
            _iv = iv ?? throw new ArgumentNullException(nameof(iv));

            if (_key.Length != 32) // 32 bytes for AES-256
            {
                throw new ArgumentException("Key must be 32 bytes for AES-256.", nameof(key));
            }

            if (_iv.Length != 16) // 16 bytes for IV
            {
                throw new ArgumentException("IV must be 16 bytes.", nameof(iv));
            }

            _aes = Aes.Create();
            _aes.Key = _key;
            _aes.IV = _iv;
        }

        public byte[] Encrypt(string data)
        {
            if (string.IsNullOrEmpty(data))
            { 
                throw new ArgumentNullException(nameof(data));
            }

            byte[] encryptedData;

            ICryptoTransform encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(data);
                    }
                }
                return msEncrypt.ToArray();
            }
        }

        public string Decrypt(byte[] encryptedData)
        {
            if (encryptedData == null || encryptedData.Length <= 0)
            {
                throw new ArgumentNullException(nameof(encryptedData));
            }

            ICryptoTransform decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV);
            using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
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
        // Implement IDisposable to properly dispose of the AES instance
        public void Dispose()
        {
            _aes?.Dispose();
        }

    }
}
