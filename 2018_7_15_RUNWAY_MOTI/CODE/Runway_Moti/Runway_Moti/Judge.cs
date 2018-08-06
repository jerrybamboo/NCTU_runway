using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    class Judge
    {
        
        public static void judge_course(JArray jarray_lesson,JArray jarray_lesson_detail, JArray excerise, string member_id)
        {
            if((((JArray)excerise).Count) == 0)return;

            JArray finish_course = new JArray();
            JArray ja = new JArray();
            JObject t = new JObject();

            int s;
            int[] workout_times = new int [130];
            //string finish_lesson_id = "nothing";
            string first_course = "";
            string last_course = "";
            try
            {
                
                for (int i = 0; i < ((JArray)jarray_lesson_detail).Count - 1; i++)
                {
                    s = i;
                    for (int j = i + 1; j < ((JArray)jarray_lesson_detail).Count; j++)
                    {
                        if ((int)jarray_lesson_detail[j]["lesson_id"]< (int)jarray_lesson_detail[s]["lesson_id"])
                        {
                            s = j;
                        }
                        
                    }
                    t = (JObject)jarray_lesson_detail[s];
                    jarray_lesson_detail[s] = jarray_lesson_detail[i];
                    jarray_lesson_detail[i] = t;
                }
                //System.Console.Write("after sort:\n" + jarray_lesson_detail + "\n");

                for (int i = 0; i < ((JArray)jarray_lesson_detail).Count; i++)
                {
                    if (i == 0 || last_course != (string)jarray_lesson_detail[i]["lesson_id"])
                        first_course = (string)jarray_lesson_detail[i]["lesson_id"];
                    else
                        continue;

                    for (int j = 0; j < 130; j++)
                    {
                        workout_times[j] = 0;
                    }
                    for (int j = i; j < ((JArray)jarray_lesson_detail).Count; j++)
                    {
                        if ((string)jarray_lesson_detail[j]["lesson_id"] == first_course)
                            workout_times[((int)jarray_lesson_detail[j]["workout_id"] - 1)] = ((int)jarray_lesson_detail[j]["fitness_reps"]);
                    }
                    for (int j = 0; j < ((JArray)excerise).Count; j++)
                    {
                        workout_times[((int)excerise[j]["workout_id"] - 1)] -= ((int)excerise[j]["fitness_reps"]);
                    }
                    for (int j = 0; j < 130; j++)
                    {
                        if (workout_times[j] > 0) break;
                        if (j == 129)
                        {
                            for (int k = 0; k < ((JArray)jarray_lesson).Count; k++)
                            {
                                if (first_course == (string)jarray_lesson[k]["lesson_id"])
                                {
                                    JObject post_json = new JObject();
                                    post_json.Add(new JProperty("member_id",member_id));
                                    post_json.Add(new JProperty("lesson_name", (string)jarray_lesson[k]["lesson_name"]));
                                    post_json.Add(new JProperty("lesson_level_id", (string)jarray_lesson[k]["lesson_level_id"]));
                                    post_json.Add(new JProperty("update_time",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    finish_course.Add(post_json);
                                    
                                }

                            }

                        }
                    }
                    last_course = first_course;
                }
                if (((JArray)finish_course).Count>0)
                {
                    System.Console.Write("finish course(input to Post_DB):\n" + finish_course + "\n");

                    Post_to_DB("http://140.113.199.75/api/test3.php", ""+finish_course);
                }


            }
            catch (Exception ex)
            {
                System.Console.Write("Judge error:" + ex + "\n");
                
            }
            
        }

        public static void Post_to_DB(string url, string postData)
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
    }
}
