using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Data;
using System.Drawing;


using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System.IO;
using System.Threading;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Runway_Moti;

namespace Runway_Moti
{
    class Data_process
    {
        /// <summary>
        /// 字串加密(非對稱式)
        /// </summary>
        /// <param name="Source">加密前字串</param>
        /// <param name="CryptoKey">加密金鑰</param>
        /// <returns>加密後字串</returns>
        public static string aesEncryptBase64(string SourceStr, string CryptoKey, string CryptoIv)
        {
            string encrypt = "";
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

                byte[] key = (Encoding.ASCII.GetBytes(CryptoKey));
                byte[] iv = (Encoding.ASCII.GetBytes(CryptoIv));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Encoding.UTF8.GetBytes(SourceStr);
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                //System.Console.Write("aesEncryptBase64 error:" + ex + "\n");
                Log.write_to_file("aesEncryptBase64 error:" + ex);
            }
            return encrypt;
        }

        /// <summary>
        /// 字串解密(非對稱式)
        /// </summary>
        /// <param name="Source">解密前字串</param>
        /// <param name="CryptoKey">解密金鑰</param>
        /// <returns>解密後字串</returns>
        public static string aesDecryptBase64(string SourceStr, string CryptoKey, string CryptoIv)
        {
            string decrypt = "";
            try
            {
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                byte[] key = (Encoding.ASCII.GetBytes(CryptoKey));
                byte[] iv = (Encoding.ASCII.GetBytes(CryptoIv));
                aes.Key = key;
                aes.IV = iv;

                byte[] dataByteArray = Convert.FromBase64String(SourceStr);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(dataByteArray, 0, dataByteArray.Length);
                        cs.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                //System.Console.Write("DecryptBase64 error:" + ex + "\n");
                Log.write_to_file("DecryptBase64 error:" + ex);
            }
            return decrypt;
        }
    }
}
