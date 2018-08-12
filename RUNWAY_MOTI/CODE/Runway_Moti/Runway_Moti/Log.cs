using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Threading;

using Newtonsoft.Json.Linq;
namespace Runway_Moti
{
    class Log
    {
        public static void write_to_file(string data)//紀錄連線失敗的學號與使用F1時領餐的類別&時間    runway_log.txt、runway_error_id.txt
        {
            try
            {
                //----------------<runway_log.txt>-----------------
                string all_data = "";

                // 先確定檔案存在
                if (File.Exists("..//..//Resources//moti_runway_log.txt"))
                {
                    // 檔案設定
                    FileStream fileInput = new FileStream("..//..//Resources//moti_runway_log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    // 開啟檔案
                    StreamReader streamIn = new StreamReader(fileInput);

                    // 讀檔
                    while (!streamIn.EndOfStream)
                    {
                        all_data = all_data + streamIn.ReadLine();
                        all_data = all_data + Environment.NewLine;
                    }
                    streamIn.Close();
                }

                all_data = data + Environment.NewLine + Environment.NewLine + all_data;

                // 檔案設定
                FileStream outFile = new FileStream("..//..//Resources//moti_runway_log.txt", FileMode.Create, FileAccess.Write);

                // 建立檔案流
                StreamWriter streamOut = new StreamWriter(outFile);

                // 寫檔
                streamOut.WriteLine(DateTime.Now.ToString("yyyy-MM-dd(ddd)  HH:mm:ss") + Environment.NewLine + all_data);

                streamOut.Close();
            }
            catch (Exception ex)
            {
                System.Console.Write("Write to file error:" + ex + "\n");

            }

        }
    }
}
