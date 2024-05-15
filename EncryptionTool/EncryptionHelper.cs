using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionTool

{
    public static class EncryptionHelper
    {
        // Method to encrypt plain text using AES encryption
        public static byte[] Encrypt(string plainText, byte[] key, byte[] iv)
        {
            // Create an instance of the AES algorithm
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and initialization vector (IV)
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create an encryptor object
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create a memory stream to hold the encrypted data
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Create a CryptoStream to perform encryption
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        // Write the plain text to the stream, which gets encrypted
                        swEncrypt.Write(plainText);
                    }

                    // Return the encrypted data as a byte array
                    return msEncrypt.ToArray();
                }
            }
        }

        // Method to decrypt cipher text using AES decryption
        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Create an instance of the AES algorithm
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and initialization vector (IV)
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decryptor object
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create a memory stream with the cipher text
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    // Create a CryptoStream to perform decryption
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        // Read the decrypted data from the stream and return as a string
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        // Method to generate a random key and IV for AES encryption
        public static void GenerateKeyAndIV(out byte[] key, out byte[] iv)
        {
            // Create an instance of the AES algorithm
            using (Aes aesAlg = Aes.Create())
            {
                // Assign the generated key and IV to the out parameters
                key = aesAlg.Key;
                iv = aesAlg.IV;
            }
        }

        // Method to encrypt a file using AES encryption
        public static void EncryptFile(string inputFile, string outputFile, byte[] key, byte[] iv)
        {
            // Open the input and output files
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and initialization vector (IV)
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create an encryptor object
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create a CryptoStream to perform encryption
                using (CryptoStream csEncrypt = new CryptoStream(fsOutput, encryptor, CryptoStreamMode.Write))
                {
                    // Copy the contents of the input file to the CryptoStream, encrypting it
                    fsInput.CopyTo(csEncrypt);
                }
            }
        }

        // Method to decrypt a file using AES decryption
        public static void DecryptFile(string inputFile, string outputFile, byte[] key, byte[] iv)
        {
            // Open the input and output files
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and initialization vector (IV)
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decryptor object
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create a CryptoStream to perform decryption
                using (CryptoStream csDecrypt = new CryptoStream(fsInput, decryptor, CryptoStreamMode.Read))
                {
                    // Copy the contents of the CryptoStream to the output file, decrypting it
                    csDecrypt.CopyTo(fsOutput);
                }
            }
        }
    }

}