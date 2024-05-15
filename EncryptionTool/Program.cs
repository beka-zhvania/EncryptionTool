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
            EncryptionHelper.GenerateKeyAndIV(out byte[] key, out byte[] iv);

            //Console.WriteLine("Enter text to encrypt:");
            //string plainText = Console.ReadLine();

            //byte[] encrypted = EncryptionHelper.Encrypt(plainText, key, iv);
            //Console.WriteLine($"Encrypted text: {Convert.ToBase64String(encrypted)}");

            //string decrypted = EncryptionHelper.Decrypt(encrypted, key, iv);
            //Console.WriteLine($"Decrypted text: {decrypted}");

            // File encryption/decryption example with paths relative to project root directory
            Console.WriteLine("Enter the relative path of the file to encrypt (relative to project root):");
            string relativeInputFile = Console.ReadLine();

            // Navigate up to project root directory (two levels up from bin\Debug\net5.0 or similar)
            string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            string inputFile = Path.Combine(projectRoot, relativeInputFile);

            string encryptedFile = Path.Combine(projectRoot, relativeInputFile + ".enc");
            EncryptionHelper.EncryptFile(inputFile, encryptedFile, key, iv);
            Console.WriteLine($"File encrypted to: {encryptedFile}");

            string decryptedFile = Path.Combine(projectRoot, relativeInputFile + ".dec");
            EncryptionHelper.DecryptFile(encryptedFile, decryptedFile, key, iv);
            Console.WriteLine($"File decrypted to: {decryptedFile}");

            Console.ReadLine();
        }
    }
}
