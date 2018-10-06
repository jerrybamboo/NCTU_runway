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
        JArray jarray_lesson = new JArray();
        JArray jarray_lesson_detail = new JArray();


        string strJson;
        //string date;
        string time_start;
        string time_end;
        int date_offset = 0;//-1為前一天
        /*
         notice:
         
         
         */
        
        public void api_start(string authStringEnc, string enc_key , string enc_iv)
        {
            try
            {
                //Console.Clear();
                this.authStringEnc = authStringEnc;
                this.enc_key = enc_key;
                this.enc_iv = enc_iv;


                DateTime dt = DateTime.Now.AddDays(date_offset);
                DateTime dt2s = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
                DateTime dt2e = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
                DateTime dtutcs = TimeZoneInfo.ConvertTimeToUtc(dt2s);
                DateTime dtutce = TimeZoneInfo.ConvertTimeToUtc(dt2e);

                time_start = dtutcs.ToString("yyyy-MM-dd HH:mm:ss");
                time_end = dtutce.ToString("yyyy-MM-dd HH:mm:ss");
                //time_start = dtutcs.ToString("2018-08-31 10:00:00");
                //time_end = dtutce.ToString("2018-09-04 12:00:00");


                //==========================< API 7>==================================
                System.Console.Write("\n(7)\n");
                api7();

                //==========================< API 8>==================================
                System.Console.Write("\n(8)\n");
                api8();

                //==========================< API 9>==================================
                System.Console.Write("\n(9)\n");
                api9();

                //==========================< API 1 >==================================
                System.Console.Write("\n(1)\n");

                for (int i = 0; i < ((JArray)jarray_all).Count; i++)
                    api1("" + jarray_all[i]["member_id"]);
                //==========================< API 2 >==================================
                System.Console.Write("\n(2)\n");
                for (int i = 0; i < ((JArray)jarray_all).Count; i++)
                    api2("" + jarray_all[i]["member_id"], time_start, time_end);



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
            catch (Exception ex)
            {
                Log.write_to_file("api_start died:" + ex);
            }
            
        } 

        

        public void api1(string member_id)
        {
            work_out_info_i.member_id = member_id;
            //物件序列化
            strJson = JsonConvert.SerializeObject(work_out_info_i, Formatting.None);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/WorkoutInfo/API/Services/Workout_info", strJson,1, true, member_id);

        }

        public void api2(string member_id , string fitness_sdatetime , string fitness_edatetime)
        {
            syn_member_fitness_record_i.member_id = member_id;
            syn_member_fitness_record_i.fitness_sdatetime = fitness_sdatetime;
            syn_member_fitness_record_i.fitness_edatetime = fitness_edatetime;
            //物件序列化
            strJson = JsonConvert.SerializeObject(syn_member_fitness_record_i, Formatting.None);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_fitness_record", strJson,2, true, member_id);

        }

        public void api3(string email)
        {
            syn_member_info_i.email = email;
            //物件序列化
            strJson = JsonConvert.SerializeObject(syn_member_info_i, Formatting.None);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_info", strJson,3, false,"");
        }

        public void api4(string member_id, string pedometer_sdatetime, string pedometer_edatetime)
        {
            syn_member_pedometer_record_i.member_id = member_id;
            syn_member_pedometer_record_i.pedometer_sdatetime = pedometer_sdatetime;
            syn_member_pedometer_record_i.pedometer_edatetime = pedometer_edatetime;
            //物件序列化
            strJson = JsonConvert.SerializeObject(syn_member_pedometer_record_i, Formatting.None);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_pedometer_record", strJson,4, true, member_id);

        }

        public void api5(string member_id, string record_sdatetime, string record_edatetime)
        {
            syn_member_info_weekly_record_i.member_id = member_id;
            syn_member_info_weekly_record_i.record_sdatetime = record_sdatetime;
            syn_member_info_weekly_record_i.record_edatetime = record_edatetime;
            //物件序列化
            strJson = JsonConvert.SerializeObject(syn_member_info_weekly_record_i, Formatting.None);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_info_weekly_record", strJson,5, true,member_id);

        }

        public void api6(string member_id, string aerobic_sdatetime_start, string aerobic_sdatetime_end)
        {
            syn_member_aerobic_record_i.member_id = member_id;
            syn_member_aerobic_record_i.aerobic_sdatetime_start = aerobic_sdatetime_start;
            syn_member_aerobic_record_i.aerobic_sdatetime_end = aerobic_sdatetime_end;
            //物件序列化
            strJson = JsonConvert.SerializeObject(syn_member_aerobic_record_i, Formatting.None);
            //輸出結果
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_aerobic_record", strJson,6, true,member_id);

        }
        public void api7()
        {
            Post("http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/list_member_info", "",7, true, "");
        }

        public void api8()
        {
            Post("http://jmex-api-service.azurewebsites.net/jmex-api/Nctu/DownloadLessonInfo", "",8, true, "");
        }

        public void api9()
        {
            Post("http://jmex-api-service.azurewebsites.net/jmex-api/Nctu/DownloadLessonWorkoutInfo", "",9, true,"");
        }


        public void Post(string url, string postData , int which_api,Boolean isarray,string member_id)
        {
            string responseFromServer;

            try
            {
                System.Console.Write("\ninput:\n" + postData + "\n");
                postData = "{\"data\":\"" + Data_process.aesEncryptBase64(postData, enc_key, enc_iv) + "\"}";
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

                if (isarray)
                {
                    jarray = (JArray)JsonConvert.DeserializeObject(response_string);
                    if (url == "http://jmex-api-service.azurewebsites.net/jmex-api/Nctu/DownloadLessonInfo")//api8
                    {
                        for (int i = 0; i < ((JArray)jarray).Count; i++)
                        {
                            if ("" + jarray[i]["verify_flag"] != "3")
                            {
                                JObject jo = (JObject)jarray[i];
                                jarray_lesson.Add(jo);
                            }
                        }
                        jarray = jarray_lesson;
                    }
                    else if (url == "http://jmex-api-service.azurewebsites.net/jmex-api/Nctu/DownloadLessonWorkoutInfo")//api9
                    {
                        
                        for (int i = 0; i < ((JArray)jarray).Count; i++)
                        {
                            for (int j = 0; j < ((JArray)jarray_lesson).Count; j++)
                            {
                                if ("" + jarray_lesson[j]["lesson_id"] == "" + jarray[i]["lesson_id"])
                                {
                                    JObject jo = (JObject)jarray[i];
                                    jarray_lesson_detail.Add(jo);
                                    break;
                                }
                            }

                        }
                        jarray = jarray_lesson_detail;
                        

                    }
                    else if (url == "http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/list_member_info")//api7
                    {
                        jarray_all = jarray;
                    }

                    System.Console.Write("output:\n" + jarray + "\n");
                }
                else
                {
                    jobject = (JObject)JsonConvert.DeserializeObject(response_string);
                    System.Console.Write("output:\n" + jobject + "\n");
                }


                if (url == "http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/syn_member_fitness_record")//api2
                {
                    for (int i = 0; i < ((JArray)jarray).Count; i++)
                    {
                        DateTime NewDate = DateTime.ParseExact("" + jarray[i]["fitness_sdatetime"], "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                        TimeZoneInfo info = TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time");
                        DateTime taidt = TimeZoneInfo.ConvertTimeFromUtc(NewDate, info);
                        //System.Console.Write("\n" + taidt.ToString("yyyy-MM-dd HH:mm:ss"));

                        DateTime NewDate2 = DateTime.ParseExact("" + jarray[i]["fitness_edatetime"], "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                        DateTime taidt2 = TimeZoneInfo.ConvertTimeFromUtc(NewDate2, info);
                        //System.Console.Write("\n" + taidt2.ToString("yyyy-MM-dd HH:mm:ss"));


                        jarray[i]["fitness_sdatetime"] = taidt.ToString("yyyy-MM-dd HH:mm:ss");
                        jarray[i]["fitness_edatetime"] = taidt2.ToString("yyyy-MM-dd HH:mm:ss");

                    }

                    string pdata = "" + jarray;
                    
                    System.Console.Write("input to Post_DB:\n" + pdata + "\n");
                    Post_to_DB("http://140.113.199.75/api/test.php", pdata , which_api);

                    Judge.judge_course(jarray_lesson,jarray_lesson_detail, jarray,member_id);//比對課程是否完成
                    
                }
                else if(url == "http://sports.moti-wearable.com/nctu/DesktopModules/MemberInfo/API/Services/list_member_info")//api7
                {
                    JArray ja = new JArray(); 
                    for (int i = 0; i < ((JArray)jarray).Count; i++)
                    {
                        JObject jo = (JObject)jarray[i];
                        jo.Add(new JProperty("password", "t"));
                        ja.Add(jo);

                    }
                    string pdata = "" + ja;
                    System.Console.Write("input to Post_DB:\n" + pdata + "\n");
                    Post_to_DB("http://140.113.199.75/api/test2.php", pdata , which_api);
                }

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

            }
            catch (Exception ex)
            {
                System.Console.Write("Post error(API "+ which_api + "):" + ex + "\n");
                Log.write_to_file("Post error(API "+ which_api + "):" + ex);
            }
        }
        public void Post_to_DB(string url, string postData,int which_api)
        {
            string responseFromServer;

            try
            {
                System.Console.Write("output Post_DB:\n");
 
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
                if (responseFromServer != "success")
                {
                    Log.write_to_file("Post to DB response error:"+responseFromServer);
                }
                reader.Close();
                dataStream.Close();
                response.Close();



            }
            catch (Exception ex)
            {
                System.Console.Write("Post to DB error:" + ex + "\n");
                Log.write_to_file("Post to DB error(API " + which_api + "):" + ex);
            }
        }


    }
}

