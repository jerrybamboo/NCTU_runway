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

namespace ConsoleApplication2
{
    class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool FreeConsole();


        static string authString = "manager@nctu.com.tw" + ":" + "manager";
        static string authStringEnc = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString), Base64FormattingOptions.None);
        static string enc_key = "aSRWheHYjG2xTPsLG71qH0QVhpGiAeur";
        static string enc_iv = "B3XVa5pTQhi+aPyP";
        static API_use control = new API_use();
        static string date_now;
        static string date_bf;

        static void Main(string[] args)
        {
            
            //將視窗隱藏起來
            FreeConsole();
            try
            {
                //System.Console.Write("Don't close the program, this program is for MOTI");
                Timer t = new Timer(TimerCallback, null, 0, 14400000);
                // Wait for the user to hit <Enter>
                while (true)
                {
                    Console.ReadLine();
                }
                
            }
            catch (Exception ex)
            {
                Log.write_to_file("main died:" + ex);
            }

        }

        public static void TimerCallback(Object o)
        {
            try
            {
                //System.Console.Write("\n"+ DateTime.Now.ToString("yyyy-MM-dd(ddd)  HH:mm:ss") +"\n");
                date_now = DateTime.Now.ToString("yyyy-MM-dd");
                if (date_now != date_bf)
                {
                    control.api_start(authStringEnc, enc_key, enc_iv);
                    date_bf = date_now;
                }
            }
            catch (Exception ex)
            {
                Log.write_to_file("timer died:" + ex);
            }

        }
    }
}
