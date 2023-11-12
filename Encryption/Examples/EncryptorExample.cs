using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SpaceMem.Encryption;
using System.Security.Cryptography;
using System;

namespace SpaceMem.Encryption.Example
{
#if UNITY_EDITOR
    [CustomEditor(typeof(EncryptorExample))]
    public class EncryptorExampleEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EncryptorExample script = (EncryptorExample)target;

            EditorGUILayout.LabelField("Encrypted Message", script.EncryptedMessage);

            if (GUILayout.Button("Encrypt Message"))
            {
                script.EncryptMessage();
            }
            if (GUILayout.Button("Decrypt Message"))
            {
                script.DecryptMessage();
            }
        }
    }

    public partial class EncryptorExample
    { 
        public string EncryptedMessage => _encryptedMessage;
    }

#endif

    public partial class EncryptorExample : MonoBehaviour
    {
        [SerializeField] private byte[] _key;
        [SerializeField] private byte[] _iv;

        [SerializeField] private string _originalMessage = "This Message Will Be Encrypted";
        private string _encryptedMessage = "";


        // Start is called before the first frame update
        void Start()
        {
            if (_key.Length == 0 || _iv.Length == 0)
                GenerateKeyAndIv();
        }

        private void GenerateKeyAndIv()
        {
            using (var aes = Aes.Create())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                _key = aes.Key;
                _iv = aes.IV;

                // For demonstration, printing the base64 key and IV
                Debug.Log("Key (Base64): " + Convert.ToBase64String(_key));
                Debug.Log("IV (Base64): " + Convert.ToBase64String(_iv));
            }
        }
        public void EncryptMessage()
        {
            if (string.IsNullOrEmpty(_originalMessage))
            {
                Debug.Log("Insert a message to encrypt");
                return;
            }

            Debug.Log("Original Message: " + _originalMessage);

            using (AesEncryptor encryptor = new AesEncryptor(_key, _iv))
            {
                string originalMessage = _originalMessage;
                byte[] encryptedMessage = encryptor.Encrypt(originalMessage);
                string encryptedBase64String = Convert.ToBase64String(encryptedMessage);

                Debug.Log("Encrypted Message (Base64): " + encryptedBase64String);

                _encryptedMessage = encryptedBase64String;
            }
        }
        public void DecryptMessage()
        {
            if (string.IsNullOrEmpty(_encryptedMessage))
            {
                Debug.Log("Please encrypt a message first");
                return;
            }

            using (AesEncryptor encryptor = new AesEncryptor(_key, _iv))
            {
                // Assuming the message to decrypt is the one already encrypted
                // and stored in _encryptedMessage
                byte[] encryptedBytes = Convert.FromBase64String(_encryptedMessage);
                string decryptedMessage = encryptor.Decrypt(encryptedBytes);
                Debug.Log("Decrypted Message: " + decryptedMessage);
            }
        }
    }

}

