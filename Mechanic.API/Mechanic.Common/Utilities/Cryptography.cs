using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace Mechanic.Common.Utilities
{
    public class Cryptography : ICryptography
    {
        private readonly RijndaelCredentials _credentials;

        //    Read app settings
        public Cryptography(IOptions<RijndaelCredentials> rijndaelCredentials)
        {
            _credentials = rijndaelCredentials.Value;
        }

        public byte[] EncryptStringToBytes(string plainText)
        {
            if (plainText.Length < 0)
                throw new ArgumentNullException("plainText");

            var aesAlg = new RijndaelManaged();
            using (var msEncrypt = new MemoryStream())
            {
                try
                {
                    aesAlg.Key = Convert.FromBase64String(_credentials.EncryptionKey);
                    aesAlg.IV = Convert.FromBase64String(_credentials.EncryptionVector);
                    var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                    var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return msEncrypt.ToArray();
                }
                finally
                {
                    aesAlg.Clear();
                }
            }

        }

        public string DecryptStringFromBytes(byte[] cipherText)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null/* TODO Change to default(_) if this is not a reference type */;



            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();

                aesAlg.Key = Convert.FromBase64String(_credentials.EncryptionKey);
                aesAlg.IV = Convert.FromBase64String(_credentials.EncryptionVector);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decrypt = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                MemoryStream msDecrypt = new MemoryStream(cipherText);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decrypt, CryptoStreamMode.Read);
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    // Read the decrypted bytes from the decrypting stream
                    // and place them in a string.
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (!(aesAlg == null))
                    aesAlg.Clear();
            }
            return plaintext;
        }
    }
}
