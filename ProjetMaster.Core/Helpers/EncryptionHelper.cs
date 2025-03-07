﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjetMaster.Core.Helpers
{
	
	public static class EncryptionHelper
	{
		public static string Encrypt(string plainText, string key)
		{
			byte[] iv = new byte[16];
			byte[] array;

			using (Aes aes = Aes.Create())
			{
				// Ensure the key is 32 bytes long
				byte[] keyBytes = Encoding.UTF8.GetBytes(key);
				Array.Resize(ref keyBytes, 32);
				aes.Key = keyBytes;
				aes.IV = iv;

				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
						{
							streamWriter.Write(plainText);
						}
						array = memoryStream.ToArray();
					}
				}
			}
			return Convert.ToBase64String(array);
		}


		public static string Decrypt(string cipherText, string key)
		{
			byte[] iv = new byte[16];
			byte[] buffer = Convert.FromBase64String(cipherText);

			using (Aes aes = Aes.Create())
			{
				// Redimensionner la clé pour qu'elle soit de 32 octets
				byte[] keyBytes = Encoding.UTF8.GetBytes(key);
				Array.Resize(ref keyBytes, 32);
				aes.Key = keyBytes;
				aes.IV = iv;

				ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

				using (MemoryStream memoryStream = new MemoryStream(buffer))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader streamReader = new StreamReader(cryptoStream))
						{
							return streamReader.ReadToEnd();
						}
					}
				}
			}
		}

	}

}
