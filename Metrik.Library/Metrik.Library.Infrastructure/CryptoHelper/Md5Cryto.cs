using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Metrik.Library.Infrastructure.CryptoHelper
{
    public class Md5Cryto
    {
        public static string Encrypt(string value)
        {

            byte[] data = UTF8Encoding.UTF8.GetBytes(value);
            string result = "";
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                string hash = "anonymus";
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    result = Convert.ToBase64String(results, 0, results.Length);
                }
            }
            return result;
        }
    }
}
