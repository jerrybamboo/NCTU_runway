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
    class API_use
    {
        string authStringEnc;
        string enc_key;
        string enc_iv;

        Work_out_info_i work_out_info_i = new Work_out_info_i();
        Syn_member_fitness_record_i syn_member_fitness_record_i = new Syn_member_fitness_record_i();
        Syn_member_pedometer_record_i syn_member_pedometer_record_i = new Syn_member_pedometer_record_i();
        Syn_member_info_weekly_record_i syn_member_info_weekly_record_i = new Syn_member_info_weekly_record_i();
        Syn_member_aerobic_record_i syn_member_aerobic_record_i = new Syn_member_aerobic_record_i();
        Syn_member_info_i syn_member_info_i = new Syn_member_info_i();

        JObject jobject;
        JArray jarray;
        JArray jarray_all;
        string strJson;
        string date;
        string time_start;
        string time_end;
        public void api_start(string authStringEnc, string enc_key , string enc_iv)
        {
            this.authStringEnc = authStringEnc;
            this.enc_key = enc_key;
            this.enc_iv = enc_iv;
            date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            time_start = date + " 00:00:00";
            time_end = date + " 23:59:59";
            //syn_member_info_i = Newtonsoft.Json.JsonConvert.DeserializeObject<SYN_MEMBER_INFO_INPUT>("{\"email\":\"hu581nctu53.eed05@nctu.edu.tw\"}");

            
            //==========================< API 0 >==================================
            System.Console.Write("\n(0)\n");
            api0();

            //==========================< API 1 >==================================
            //System.Console.Write("\n(1)\n");
            //(wait for api finish) 
            //for (int i = 0; i < ((JArray)jarray_all).Count; i++)
            //    api1("" + jarray_all[i]["member_id"]);
            //==========================< API 2 >==================================
            System.Console.Write("\n(2)\n");
            for (int i = 0; i < ((JArray)jarray_all).Count; i++)
                api2(""+jarray_all[i]["member_id"], time_start, time_end);
            
            //==========================< API 3 >=================================
            System.Console.Write("\n(3)\n");
            for (int i = 0; i < ((JArray)jarray_all).Count; i++)
                api3("" + jarray_all[i]["email"]);

            //==========================< API 4 >==================================
            System.Console.Write("\n(4)\n");
            for (int i = 0; i < ((JArray)jarray_all).Count; i++)
                api4("" + jarray_all[i]["member_id"], time_start, time_end);
            //==========================< API 5 >==================================
            System.Console.Write("\n(5)\n");
            for (int i = 0; i < ((JArray)jarray_all).Count; i++)
                api5("" + jarray_all[i]["member_id"], time_start, time_end);
            //==========================< API 6 >==================================
            System.Console.Write("\n(6)\n");
            for (int i = 0; i < ((JArray)jarray_all).Count; i++)
                api6("" + jarray_all[i]["member_id"], time_start, time_end);


            // Force a garbage collection to occur for this
            GC.Collect();
        } 

        public void api0()
        {
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/list_member_info", "", true);
        }

        public void api1(string member_id)
        {
            work_out_info_i.member_id = member_id;
            //物件序列化
            strJson = JsonConvert.SerializeObject(work_out_info_i, Formatting.Indented);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/WorkoutInfo/API/Services/WorkoutInfo", strJson, false);

        }

        public void api2(string member_id , string fitness_sdatetime , string fitness_edatetime)
        {
            syn_member_fitness_record_i.member_id = member_id;
            syn_member_fitness_record_i.fitness_sdatetime = fitness_sdatetime;
            syn_member_fitness_record_i.fitness_edatetime = fitness_edatetime;
            //物件序列化
            strJson = JsonConvert.SerializeObject(syn_member_fitness_record_i, Formatting.Indented);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_fitness_record", strJson, true);

        }

        public void api3(string email)
        {
            syn_member_info_i.email = email;
            //物件序列化
            strJson = JsonConvert.SerializeObject(syn_member_info_i, Formatting.Indented);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_info", strJson, false);
        }

        public void api4(string member_id, string pedometer_sdatetime, string pedometer_edatetime)
        {
            syn_member_pedometer_record_i.member_id = member_id;
            syn_member_pedometer_record_i.pedometer_sdatetime = pedometer_sdatetime;
            syn_member_pedometer_record_i.pedometer_edatetime = pedometer_edatetime;
            //物件序列化
            strJson = JsonConvert.SerializeObject(syn_member_pedometer_record_i, Formatting.Indented);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_pedometer_record", strJson, true);

        }

        public void api5(string member_id, string record_sdatetime, string record_edatetime)
        {
            syn_member_info_weekly_record_i.member_id = member_id;
            syn_member_info_weekly_record_i.record_sdatetime = record_sdatetime;
            syn_member_info_weekly_record_i.record_edatetime = record_edatetime;
            //物件序列化
            strJson = JsonConvert.SerializeObject(syn_member_info_weekly_record_i, Formatting.Indented);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_info_weekly_record", strJson, true);

        }

        public void api6(string member_id, string aerobic_sdatetime_start, string aerobic_sdatetime_end)
        {
            syn_member_aerobic_record_i.member_id = member_id;
            syn_member_aerobic_record_i.aerobic_sdatetime_start = aerobic_sdatetime_start;
            syn_member_aerobic_record_i.aerobic_sdatetime_end = aerobic_sdatetime_end;
            //物件序列化
            strJson = JsonConvert.SerializeObject(syn_member_aerobic_record_i, Formatting.Indented);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_aerobic_record", strJson, true);

        }

        public void Post(string url, string postData , Boolean isarray)
        {
            string responseFromServer;

            try
            {
                System.Console.Write("\ninput:\n" + postData + "\n");
                postData = "{\"data\":\"" + Data_process.aesEncryptBase64(postData, enc_key, enc_iv) + "\"}";
                //System.Console.Write(postData + "\n");
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

                responseFromServer = responseFromServer.Substring(1, responseFromServer.Length - 2);

                string response_string = Data_process.aesDecryptBase64(responseFromServer, enc_key, enc_iv);

                //System.Console.Write(response_string + "\n");

                if (isarray)
                {
                    jarray = (JArray)JsonConvert.DeserializeObject(response_string);
                    System.Console.Write("output:\n" + jarray + "\n");
                    if (url == "http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/list_member_info")
                        jarray_all = jarray;
                }
                else
                {
                    jobject = (JObject)JsonConvert.DeserializeObject(response_string);
                    System.Console.Write("output:\n" + jobject + "\n");
                }
                if (url == "http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_fitness_record")
                {
                    string pdata = "" + jarray;
                    
                    System.Console.Write("input to Post_DB:\n" + pdata + "\n");
                    Post_to_DB("http://140.113.67.226/php/syn_member_fitness_record.php", pdata);
                }
               
                //SYN_MEMBER_INFO_OUTPUT_1 = Newtonsoft.Json.JsonConvert.DeserializeObject<SYN_MEMBER_INFO_OUTPUT>(response_string);
                //string strJson2 = JsonConvert.SerializeObject(SYN_MEMBER_INFO_OUTPUT_1, Formatting.Indented);
                //System.Console.Write(strJson2 + "\n");

                //LIST_MEMBER_INFO_OUTPUT_all = Newtonsoft.Json.JsonConvert.DeserializeObject<LIST_MEMBER_INFO_OUTPUT>(response_string);
                //string strJson2 = JsonConvert.SerializeObject(LIST_MEMBER_INFO_OUTPUT_all, Formatting.Indented);
                //System.Console.Write(strJson2 + "\n");

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();



            }
            catch (Exception ex)
            {
                System.Console.Write("Post error:" + ex + "\n");
            }
        }
        public void Post_to_DB(string url, string postData)
        {
            string responseFromServer;

            try
            {
                System.Console.Write("output Post_DB:\n");
                //System.Console.Write("\nDB:\n" + postData + "\n");

                //System.Console.Write(postData + "\n");
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(url);

                // Set the Method property of the request to POST.
                request.Method = "POST";
                //request.Headers.Add("Authorization", "Basic " + authStringEnc);
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
                System.Console.Write(responseFromServer + "\n");

                reader.Close();
                dataStream.Close();
                response.Close();



            }
            catch (Exception ex)
            {
                System.Console.Write("Post to DB error:" + ex + "\n");
            }
        }

        public string Deal_post_data(string input)
        {
            return "{"+input.Substring(1, input.Length - 2)+"}";
        }
    }
}

