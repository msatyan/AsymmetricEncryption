/////////////////////////
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MyRsa
{
    public class MySimpleRSA // Simple Asymmetric Encryption Demo
    {
        public static void SimpleRsaTest()
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            byte[] DataToEncrypt = ByteConverter.GetBytes("Try to encrypt this message");

            bool IncludePrivateKeyToo = false;
            bool OAEPPadding = false;
            bool IsDecrip = false;

            // Use the default key, if no default key found then create a key pair.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            IncludePrivateKeyToo = false; // REquest to include only Public Key, (no private Key)
            // The Exponent field of RSAParameters is the public key.
            RSAParameters PublicKeyInfo = rsa.ExportParameters(IncludePrivateKeyToo);

            IncludePrivateKeyToo = true; // REquest to include both Public and Private Keys.
            // The private key will be stored in D
            RSAParameters PrivateKeyInfo = rsa.ExportParameters(IncludePrivateKeyToo);


            ///////////// Encript with puhlic key /////////
            IsDecrip = false; 
            byte[] EncryptedData = RSAEncryptOrDecrypt(DataToEncrypt, PublicKeyInfo, OAEPPadding, IsDecrip);

            //////////// Decrypt with private key. ///////
            IsDecrip = true;
            byte[] DataAfterDecrypt = RSAEncryptOrDecrypt(EncryptedData, PrivateKeyInfo, OAEPPadding, IsDecrip);



            Console.WriteLine("Original Data    : {0}", ByteConverter.GetString(DataAfterDecrypt));
            Console.WriteLine("DataAfterDecrypt : {0}", ByteConverter.GetString(DataAfterDecrypt));

            Util.PrintByteArray(EncryptedData, "EncryptedData");
        }

        static public byte[] RSAEncryptOrDecrypt(byte[] InputData, 
                            RSAParameters RSAKeyInfo, bool OAEPPadding, bool IsDecrip)
        {
            byte[] ResultantData;

            try
            {
                // Create new instance of RSA
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    //Import the given RSA Key information. 
                    rsa.ImportParameters(RSAKeyInfo);

                    if (IsDecrip)
                    {
                        ResultantData = rsa.Decrypt(InputData, OAEPPadding);
                    }
                    else
                    {
                        // Encrypt the InputData with OAEP padding.  
                        ResultantData = rsa.Encrypt(InputData, OAEPPadding);
                    }
                }
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return ResultantData;
        }
    }
}

