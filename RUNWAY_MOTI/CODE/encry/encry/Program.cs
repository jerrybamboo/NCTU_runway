using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;


using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace encry
{
    class Program
    {
        static string authString = "manager@nctu.com.tw" + ":" + "manager";
        static string authStringEnc = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString), Base64FormattingOptions.None);
        static string enc_key = "aSRWheHYjG2xTPsLG71qH0QVhpGiAeur";
        static string enc_iv = "B3XVa5pTQhi+aPyP";

        
        static JObject jo= new JObject();
        static JObject jo2 = new JObject();

        static JObject jobject = new JObject();
        static JArray jarray = new JArray();

        static string enc;
        static string dec;
        static string postData;
        
        static void Main(string[] args)
        {
            //test->500 error , test2-> correct
            string test = "30801215-7648-4279-A479-105EC694DE42";
            string test2 = "92C101AB-70F8-40B4-B218-9B43D1DFDBF0";

            //the id you want to test 
            jo.Add(new JProperty("member_id", test2));
            jo.Add(new JProperty("fitness_sdatetime", "2018-08-15 16:00:00"));
            jo.Add(new JProperty("fitness_edatetime", "2018-08-16 15:59:59"));

            //output origin input
            System.Console.Write("input origin\n"+jo+"\n");

            //encrypt
            enc = aesEncryptBase64(""+jo, enc_key, enc_iv);

            //after encrypting
            System.Console.Write("\nenc\n" + enc + "\n");

            //check encrypt
            dec = aesDecryptBase64(enc, enc_key, enc_iv);
            System.Console.Write("\ndec\n" + dec + "\n");

            jo2.Add(new JProperty("data", enc));

            //convert JObject to string
            postData = ""+jo2;

            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_fitness_record", postData, 2, true, "");
            Console.ReadLine();
        }

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
                System.Console.Write("aesEncryptBase64 error:" + ex + "\n");
                
            }
            return encrypt;
        }

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
                System.Console.Write("DecryptBase64 error:" + ex + "\n");
            }
            return decrypt;
        }
        public static void Post(string url, string postData, int which_api, Boolean isarray, string member_id)
        {
            string responseFromServer;

            try
            {
                // Post input
                System.Console.Write("\nPost input:\n" + postData + "\n");
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(url);

                // Set the Method property of the request to POST.
                request.Method = "POST";
                request.Headers.Add("Authorization", "Basic " + authStringEnc);
                // Create POST data and convert it to a byte array.
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                // Set the ContentType property of the WebRequest.
                request.ContentType = "application/json";
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = byteArray.Length;
                // Get the request stream.
                Stream dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);
                // Close the Stream object.
                dataStream.Close();


                // Get the response.
                WebResponse response = request.GetResponse();

                // Get the stream containing content returned by the server.
                dataStream = response.GetResponseStream();

                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                responseFromServer = reader.ReadLine();
                // Output response
                System.Console.Write("\nres\n" + responseFromServer + "\n");
                responseFromServer = responseFromServer.Substring(1, responseFromServer.Length - 2);
                // To descrypt the response
                string response_string = aesDecryptBase64(responseFromServer, enc_key, enc_iv);

                if (isarray)
                {
                    jarray = (JArray)JsonConvert.DeserializeObject(response_string);

                    System.Console.Write("\nPoat output:\n" + jarray + "\n");
                }
                else
                {
                    jobject = (JObject)JsonConvert.DeserializeObject(response_string);
                    System.Console.Write("\nPost output:\n" + jobject + "\n");
                }

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

            }
            catch (Exception ex)
            {
                System.Console.Write("Post error(API " + which_api + "):" + ex + "\n");
            }
        }
    }
}
