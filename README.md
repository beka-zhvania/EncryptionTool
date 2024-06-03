
# EncryptionTool

## Overview
`EncryptionTool` is a very simple C# console application designed for file encryption and decryption using AES-256. This tool can be used for securing files by encrypting them and later decrypting them with the same key and initialization vector (IV).

## Features
- Encrypt any file with AES-256 encryption.
- Decrypt files using the previously generated key and IV.
- Automatically generates and stores encryption keys and IVs.

## Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Installation
1. Clone the repository to your local machine:

2. Navigate to the project directory:
   ```bash
   cd EncryptionTool
   ```
3. Build the project:
   ```bash
   dotnet build
   ```

## Usage
1. Run the application:
   ```bash
   dotnet run
   ```
2. Follow the on-screen instructions to either encrypt or decrypt a file:
   - To **encrypt** a file, enter `encrypt` and provide the relative path to the file you want to encrypt.
   - To **decrypt** a file, enter `decrypt` and provide the relative paths to both the encrypted file and the key & IV file.

### Example
- To encrypt a file named `example.txt` in the project root directory:
  ```
  Do you want to encrypt or decrypt a file? (Type 'encrypt' or 'decrypt')
  encrypt
  Enter the relative path of the file to encrypt (relative to project root):
  example.txt
  Key & IV file created at: <project_root>/encryption.keyiv
  File encrypted to: <project_root>/example.txt.enc
  ```

- To decrypt the previously encrypted file:
  ```
  Do you want to encrypt or decrypt a file? (Type 'encrypt' or 'decrypt')
  decrypt
  Enter the relative path of the file to decrypt (relative to project root):
  example.txt.enc
  Enter the relative path of the key & IV file (relative to project root):
  encryption.keyiv
  File decrypted to: <project_root>/example.txt.enc.dec
  ```

