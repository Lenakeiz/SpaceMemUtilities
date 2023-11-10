
namespace SpaceMem.Encryption
{
    public interface IEncryptor
    {
        byte[] Encrypt(string data);
        string Decrypt(byte[] encryptedData);
    }
}
