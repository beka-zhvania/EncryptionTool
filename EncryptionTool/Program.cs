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
            bool exit = false;



            while (!exit)
            {

                Console.WriteLine("Do you want to encrypt or decrypt a file? (Type 'encrypt' or 'decrypt')");
                string choice = Console.ReadLine().Trim().ToLower();

                if (choice == "encrypt")
                {
                    EncryptionHelper.GenerateKeyAndIV(out byte[] key, out byte[] iv);
                    Console.WriteLine("Enter the relative path of the file to encrypt (relative to project root):");
                    string relativeInputFile = Console.ReadLine();
                    string inputFile = Path.Combine(projectRoot, relativeInputFile);


                    // Combine key and IV into a single byte array
                    byte[] keyAndIv = new byte[key.Length + iv.Length];
                    Buffer.BlockCopy(key, 0, keyAndIv, 0, key.Length);
                    Buffer.BlockCopy(iv, 0, keyAndIv, key.Length, iv.Length);

                    string keyIvFilePath = Path.Combine(projectRoot, "encryption.keyiv");

                    try
                    {
                        // Write combined key and IV to a single file
                        File.WriteAllBytes(keyIvFilePath, keyAndIv);
                        Console.WriteLine($"Key & IV file created at: {keyIvFilePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to create key or IV file: {ex.Message}");
                        continue;
                    }

                    string encryptedFile = Path.Combine(projectRoot, relativeInputFile + ".enc");
                    EncryptionHelper.EncryptFile(inputFile, encryptedFile, key, iv);
                    Console.WriteLine($"File encrypted to: {encryptedFile}\n");
                }
                else if (choice == "decrypt")
                {
                    Console.WriteLine("Enter the relative path of the file to decrypt (relative to project root):");
                    string relativeInputFile = Console.ReadLine();
                    string encryptedFile = Path.Combine(projectRoot, relativeInputFile);

                    Console.WriteLine("Enter the relative path of the key & IV file (relative to project root):");
                    string relativeKeyAndIVFile = Console.ReadLine();
                    string keyAndIVFilePath = Path.Combine(projectRoot, relativeKeyAndIVFile);


                    string decryptedFile = Path.Combine(projectRoot, relativeInputFile + ".dec");
                    EncryptionHelper.DecryptFile(encryptedFile, decryptedFile, keyAndIVFilePath);
                    Console.WriteLine($"File decrypted to: {decryptedFile}\n");
                }
                else if (choice == "exit")
                {
                    exit = true;
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please choose one of the following: Encrypt, Decrypt, Exit\n");
                }
            }

            Console.ReadLine();
        }
    }
}
