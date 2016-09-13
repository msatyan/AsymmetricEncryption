using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace MyRsa
{
    public class Util
    {
        static public void PrintRSAParameters(RSAParameters key)
        {
            // RSAParameters contains fields named D, DP, DQ, Exponent, InverseQ, Modulus, P, Q
            // The Exponent field of RSAParameters is public key. 
            // The D field of RSAParameters has private key. 

            //  First a very quick refresher on the names of some of the numbers used in RSA.  
            // To generate a key pair, you start by creating two large prime numbers named p and q.  
            // These numbers are multiplied together and the result is called n.  
            // Note that since p and q are both prime, the only factors of n are 1, p, q, and n.  
            // Next you figure out how many numbers are relatively prime to, 
            // that is have no factors in common with, n (we’ll only consider numbers less than n).  
            // Skipping some of the math, it turns out that there will be (p – 1)(q – 1) of these numbers.  
            // Now we’ll choose a number e relatively prime to that value.  The public key is now represented as {e, n}.

            // To create the private key, we’ll calculate d, which is a number such that (d) (e) mod n = 1.  
            // The private key is now {d, n}.

            // Encryption of plaintext m to ciphertext c is defined as c = (m ^ e) mod n. 
            // Decrypting would then be defined as m = (c ^ d) mod n.  

            // PrintByteArray(key.D, "D");
            // PrintByteArray(key.DP, "DP");
            // PrintByteArray(key.DQ, "DQ");
            // PrintByteArray(key.Exponent, "Exponent");
            // PrintByteArray(key.InverseQ, "InverseQ");
            // PrintByteArray(key.Modulus, "Modulus");
            // PrintByteArray(key.P, "P");
            // PrintByteArray(key.Q, "Q");


            String s = "";
            
            Console.WriteLine("{");

            s = PrintBase64(key.D, "D");
            if (s != null)
                Console.WriteLine("    {0}, ", s);

            s = PrintBase64(key.DP, "DP");
            if (s != null)
                Console.WriteLine("    {0}, ", s);

            s = PrintBase64(key.DQ, "DQ");
            if (s != null)
                Console.WriteLine("    {0}, ", s);

            s = PrintBase64(key.Exponent, "Exponent");
            if (s != null)
                Console.WriteLine("    {0}, ", s);

            s = PrintBase64(key.InverseQ, "InverseQ");
            if (s != null)
                Console.WriteLine("    {0}, ", s);

            s = PrintBase64(key.Modulus, "Modulus");
            if (s != null)
                Console.WriteLine("    {0}, ", s);

            s = PrintBase64(key.P, "P");
            if (s != null)
                Console.WriteLine("    {0}, ", s);

            s = PrintBase64(key.Q, "Q");
            if (s != null)
                Console.WriteLine("    {0}, ", s);

            Console.WriteLine("}");
        }

        static public string PrintBase64(byte[] b, String name)
        {
            String s = null;

            if (b != null)
            {
                s = String.Format(" \"{0}\" : \"{1}\" ", name,
                    Convert.ToBase64String(b).
                            Replace('+', '-').Replace('/', '_').Replace("=", ""));
            }

            return (s);
        }

        static public void PrintByteArray(byte[] b, String name)
        {
            Console.WriteLine();
            Console.Write("{0}[{1}] = ", name, b.Length);

            for (int i = 0; i < b.Length; ++i)
            {
                int v = b[i];
                Console.Write("{0:X} ", v);
            }
            Console.WriteLine();
        }


        public static bool TestByte(byte[] bData)
        {
            String b64 = Convert.ToBase64String(bData).Replace('+', '-').Replace('/', '_').Replace("=", "");
            byte[] bData2 = Convert.FromBase64String(b64);

            bool IsValid = bData.SequenceEqual(bData2);
            if (!IsValid)
            {
                Console.WriteLine("Error");
            }

            return (IsValid);
        }
    }
}
