using System;
using System.Text;
using NUnit.Framework;
using EncryptionTool;

namespace EncryptionTool.Tests
{
    [TestFixture]
    public class EncryptionHelperTests
    {

        private byte[] _key;
        private byte[] _iv;
        private string _plainText;

        [SetUp]
        public void Setup()
        {
            EncryptionHelper.GenerateKeyAndIV(out _key, out _iv);
            _plainText = "Hello, World!";
        }

        [Test]
        public void Encrypt_ReturnsCipherText()
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(_plainText);

            // Encrypt plain text
            byte[] cipherText = EncryptionHelper.Encrypt(_plainText, _key, _iv);

            // Assert that encrypted text differs from the plain text
            Assert.IsNotNull(cipherText);
            Assert.AreNotEqual(_plainText, cipherText);

        }

        [Test]
        public void Decrypt_ReturnsCipherText()
        {
 

            // Encrypt and then decrypt the text
            byte[] cipherText = EncryptionHelper.Encrypt(_plainText, _key, _iv);
            string decryptedText = EncryptionHelper.Decrypt(cipherText, _key, _iv);

            // Assert that decrypted text is same as the text before encryption
            Assert.AreEqual(_plainText, decryptedText);

        }
    }
}