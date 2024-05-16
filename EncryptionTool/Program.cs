using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

            Console.WriteLine("Do you want to encrypt or decrypt a file? (Type 'encrypt' or 'decrypt')");
            string choice = Console.ReadLine().Trim().ToLower();

            if (choice == "encrypt")
            {
                EncryptionHelper.GenerateKeyAndIV(out byte[] key, out byte[] iv);
                Console.WriteLine("Enter the relative path of the file to encrypt (relative to project root):");
                string relativeInputFile = Console.ReadLine();
                string inputFile = Path.Combine(projectRoot, relativeInputFile);

                string keyFilePath = Path.Combine(projectRoot, "encryption.key");
                string ivFilePath = Path.Combine(projectRoot, "encryption.iv");
                try
                {
                    File.WriteAllBytes(keyFilePath, key);
                    File.WriteAllBytes(ivFilePath, iv);
                    Console.WriteLine($"Key file created at: {keyFilePath}");
                    Console.WriteLine($"IV file created at: {ivFilePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to create key or IV file: {ex.Message}");
                    return;
                }

                string encryptedFile = Path.Combine(projectRoot, relativeInputFile + ".enc");
                EncryptionHelper.EncryptFile(inputFile, encryptedFile, key, iv);
                Console.WriteLine($"File encrypted to: {encryptedFile}");
            }
            else if (choice == "decrypt")
            {
                Console.WriteLine("Enter the relative path of the file to decrypt (relative to project root):");
                string relativeInputFile = Console.ReadLine();
                string encryptedFile = Path.Combine(projectRoot, relativeInputFile);

                Console.WriteLine("Enter the relative path of the key file (relative to project root):");
                string relativeKeyFile = Console.ReadLine();
                string keyFilePath = Path.Combine(projectRoot, relativeKeyFile);

                Console.WriteLine("Enter the relative path of the IV file (relative to project root):");
                string relativeIvFile = Console.ReadLine();
                string ivFilePath = Path.Combine(projectRoot, relativeIvFile);

                string decryptedFile = Path.Combine(projectRoot, relativeInputFile + ".dec");
                EncryptionHelper.DecryptFile(encryptedFile, decryptedFile, keyFilePath, ivFilePath);
                Console.WriteLine($"File decrypted to: {decryptedFile}");
            }
            else
            {
                Console.WriteLine("Invalid choice. Please restart the program and choose either 'encrypt' or 'decrypt'.");
            }

            Console.ReadLine();
        }
    }
}
