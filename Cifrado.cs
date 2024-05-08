using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sello
{
    internal class Cifrado
    {
        private static byte[] salteado = Encoding.UTF8.GetBytes("cickerSello");

        internal static string cifrar(string texto, string contraseñaMaestra)
        {
            using (Aes aesAlg = Aes.Create())
            {
                Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(contraseñaMaestra, salteado);
                aesAlg.Key = keyDerivation.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = keyDerivation.GetBytes(aesAlg.BlockSize / 8);

                using (ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(texto);
                            }
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        internal static string descifrar(string textoCifrado, string contraseñaMaestra)
        {
            using (Aes aesAlg = Aes.Create())
            {
                Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(contraseñaMaestra, salteado);
                aesAlg.Key = keyDerivation.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = keyDerivation.GetBytes(aesAlg.BlockSize / 8);

                using (ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(textoCifrado)))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
        }
    }
}
