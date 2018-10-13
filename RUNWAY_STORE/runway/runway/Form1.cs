using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System.IO;
using System.Threading;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace runway
{
    public partial class Form1 : Form
    {
        int num_39 ;//當天剩下的39元早餐數
        int num_49;//當天剩下的49元早餐數


        string text = "待命中";//不同status所要顯示的文字
        int max_x;//全螢幕寬度
        int max_y;//全螢幕高度
        Student stu = new Student { userid = "4818", name = "None", result = -1, status = 1, time = 0 };//當前顯示在螢幕上的學生，以上為內定值

        int choose = 1;          //選擇兌換的早餐    1:39元早餐      2:49元早餐

        bool finish_choose = true;//是否選好領取39或49元早餐
        bool error = false;      //是否程式錯誤，方便debug用
        bool draw = false;

        //特例處理
        bool skip = false;
        bool forever_skip = false;//特殊狀態是否開啟
        string rec = "start";
        
        public class Student
        {
            public int GetID_result { get; set; }//辨識API所傳輸的資料
            /*
             GetID_resul代碼說明:
             代碼數值	            說明	                         http statusCode
                 1	              表示成功	                               200
                 0	            表示指紋辨識失敗	                       400
               -254	     表示發送API送出的資料內容有問題	               400
               -255	     表示程式內部發生錯誤，導致辨識過程被中斷	       500

            */

            public string message { get; set; }//辨識API所傳輸的資料的狀況
            /*
             message代碼說明:
             
             代碼數值	        說明
                OK	             OK
             InvalidData	表示發送的資料內容有問題
             VerifyFail    	指紋辨識失敗

            */
            public string userid { get; set; }//ID
            public string name { get; set; }//名字
            public int result { get; set; }//是否可領餐
            public int status { get; set; }//狀態
            /*
                    case -1:
                        text = "待命中";
                    case 0:
                        text = "查無會員資料";
                    case 1:
                        text = "可領餐";
                    case 2:
                        text = "已領餐";
                    case 3:
                        text = "上週未完成跑步要求";
                    case 4:
                        text = "尚未跑步";
                    case 5:
                        text = "等級未達要求";
                    case 6:
                        text = "點數不足";
                    case 7:
                        text = "跑步超時";
                    case 8:
                        text = "時間錯誤";
                    default:
                        text = "錯誤";
          */
        public int time { get; set; }//愈時

        }

        public Form1()
        {

            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;

            max_x = this.ClientSize.Width;//全螢幕寬度
            max_y = this.ClientSize.Height;//全螢幕高度

            //max_x = 1536;
            //max_y = 801;

            label1.Text = "可選擇39元早餐或49元早餐!!!";
            date.Text = DateTime.Now.ToString("yyyy-MM-dd(ddd)  HH:mm:ss");
            command.Text = "請輸入學號";
            except.Text = "*錯誤:連線失敗";
            noitem.Text = "*錯誤:39元餐已領畢，你的等級不足以領49元餐";
            no49.Text = "*公告:49元餐已領畢";
            sp_event.Text = "特殊狀態:關閉";
            qu39.Text = "";//button1 下顯示的備註，暫時沒用到
            qu49.Text = "";//button2 下顯示的備註，暫時沒用到

            //--------------------<設定位置>----------------------
            user.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5));
            condition.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5 + 100));
            overtime.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5 + 200));
            date.Location = new Point(max_x - 400, 100);
            button1.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5 + 100));
            button2.Location = new Point((int)(max_x / 2 + 70), (int)(max_y * 3 / 5 + 100));
            label1.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5));

            command.Location = user.Location;
            textBox1.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5 + 50));
            except.Location = new Point((int)(max_x * 2 / 5) - 20, (int)(max_y * 3 / 5 + 120));
            button3.Location = new Point((int)(max_x * 2 / 5) + 250, (int)(max_y * 3 / 5 + 50));
            noitem.Location = except.Location;
            no49.Location = noitem.Location;

            rem_39.Location= new Point(100, 100);
            rem_49.Location = new Point(100, 150);

            sp_event.Location= new Point(100, 50);
            button4.Location = new Point(320, 50);

            qu39.Location= new Point((int)(max_x / 3), (int)(max_y * 3 / 5 + 200));
            qu49.Location= new Point((int)(max_x / 2 + 70), (int)(max_y * 3 / 5 + 200));

            //------------------------------------------------------
            //--------------------<設定是否顯示>--------------------
            user.Visible = false;
            condition.Visible = false;
            overtime.Visible = false;

            button1.Visible = false;
            button2.Visible = false;
            qu39.Visible = false;
            qu49.Visible = false;

            label1.Visible = false;


            except.Visible = false;
            noitem.Visible = false;
       
            command.Visible = true;
            textBox1.Visible = true;
            button3.Visible = true;

            no49.Visible = false;

            sp_event.Visible = true;
            button4.Visible = true;

            label2.Visible = false;//測試用，可隨意修改
            //------------------------------------------------------
            store_num(1);//read_or_write   1:讀取剩下的庫存  2:存入庫存量於檔案中

        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                Environment.Exit(Environment.ExitCode);
            }
            if (e.KeyCode == Keys.Enter)
            {
                af_inputID();
            }
        }

        public void Get_StudentData(string url)//GET此學生的相關參數
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                    request.Method = "GET";
                    request.Accept = "application/json";

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    StringBuilder output = new StringBuilder();
                    output.Append(reader.ReadToEnd());

                    string mycontent = output.ToString();
                    string[] tem_split = mycontent.Split(',', '"', '{', '}', ':', '[', ']');//字串中切掉的部分
                    int len = 0;
                    string[] tem2_split = new string[8];
                    for (int i = 0; i < tem_split.Length; i++)
                    {
                        if (tem_split[i] != "")
                        {
                            if (len == 8)
                            {
                                //error = true;
                                break;
                            }
                            tem2_split[len] = tem_split[i];
                            len++;
                        }

                    }
                    if (len != 8)
                    {
                        //error = true;
                    }
                    if (!error)
                    {
                        stu.userid = tem2_split[1];
                        stu.result = Convert.ToInt32(tem2_split[3]);
                        stu.status = Convert.ToInt32(tem2_split[5]);
                        stu.time = Convert.ToInt32(tem2_split[7]);
                    }
                    else
                    {
                        log("Error:Parameter identification failed");
                    }
                }
                catch (Exception ex)
                {
                    textBox1.Text = "";
                    stu.result = -1;
                    draw = false;
                    except.Visible = true;
                    timer2.Enabled = true;
                    //error = true;
                    log("在提取您所要求的" + url + "網頁時發生錯誤。" + "請檢查您所鍵入的 URL 以及 Internet 連線，並再次嘗試。WebException:" + ex.Message);
                }
            }
        }

        public void Post(string url)
        {
            string responseFromServer;
            JObject postData = new JObject();
            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(url);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.
                postData.Add(new JProperty("name", stu.userid));
                postData.Add(new JProperty("choose", Convert.ToString(choose)));
                byte[] byteArray = Encoding.UTF8.GetBytes(""+postData);


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

                check_post(responseFromServer);

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                



                timer2.Enabled = true;
                if (forever_skip)//紀錄開啟特殊模式後領餐的人
                    log("");
            }
            catch (Exception ex)
            {
                textBox1.Text = "";
                stu.result = -1;
                draw = false;
                //error = true;
                log("在提取您所要求的" + url + "網頁時發生錯誤。" + "請檢查您所鍵入的 URL 以及 Internet 連線，並再次嘗試。WebException:" + ex.Message);
            }


        }

        public void check_post(string responseFromServer)
        {
            string[] tem_split = responseFromServer.Split(',', '"', '{', '}', ':', '[', ']');//字串中切掉的部分
            int len = 0;
            string[] tem2_split = new string[2];
            for (int i = 0; i < tem_split.Length; i++)
            {
                if (tem_split[i] != "")
                {
                    if (len == 2)
                    {
                        //error = true;
                        log("Post Error");
                        break;
                    }
                    tem2_split[len] = tem_split[i];
                    len++;
                }

            }
            if (!error)
            {
                if (String.Compare("0", tem2_split[1]) == 0)
                    log("Post Fail");
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            
            error = false;//為了讓程式遇到error持續執行，測試用

            date.Text = DateTime.Now.ToString("yyyy-MM-dd(ddd)  HH:mm:ss");//現在時間
            
            if (except.Visible)
            {
                no49.Visible = false;
            }
            
            if (!error)
            {
                rem_39.Text = "#39元早餐:剩餘" + Convert.ToString(num_39) + "份";
                rem_49.Text = "#49元早餐:剩餘" + Convert.ToString(num_49) + "份";

                
                

                switch (stu.status)
                {
                    case -1:
                        text = "待命中";
                        break;
                    case 0:
                        text = "查無會員資料";
                        break;
                    case 1:
                        text = "可領餐";
                        break;
                    case 2:
                        text = "已領餐";
                        break;
                    case 3:
                        text = "上週未完成跑步要求";
                        break;
                    case 4:
                        text = "尚未跑步";
                        break;
                    case 5:
                        text = "等級未達要求";
                        break;
                    case 6:
                        text = "點數不足";
                        break;
                    case 7:
                        text = "跑步超時";
                        break;
                    case 8:
                        text = "時間錯誤";
                        break;
                    default:
                        text = "錯誤";
                        break;
                }

            }



            if (finish_choose && !error)
            {
                user.Text = "學號:" + stu.userid;
                condition.Text = "狀態:" + text;

                if (stu.status == 7)
                {
                    overtime.Text = "超時" + stu.time + "秒";
                }
                else if (stu.status == 1)
                {
                    if (choose == 1)
                        overtime.Text = "領取39元早餐";
                    else
                        overtime.Text = "領取49元早餐";
                }
                else
                {
                    overtime.Text = "";
                }
            }

            this.Invalidate();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (draw)//draw   是否顯示圖案   勾&叉
            {
                try
                {
                    if (error)
                        e.Graphics.DrawImage(imageList1.Images[2], max_x / 2 + 400, max_y / 2 - 100, 128, 128);
                    else if (stu.result == 0 || stu.result == 1)
                        e.Graphics.DrawImage(imageList1.Images[stu.result], max_x / 2 + 400, max_y / 2 - 50, 128, 128);
                    else
                        e.Graphics.DrawImage(imageList1.Images[1], max_x / 2 + 400, max_y / 2 - 50, 128, 128);
                }
                catch (Exception err)
                {
                    draw = false;
                    //error = true;
                    log("Picture error:" + err.Message);
                }
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (num_39 == 0)
            {
                return;
            }

            choose = 1;

            show();
            if (choose == 1 && stu.result != -1)
            {
                num_39--;
                store_num(2);//read_or_write   1:讀取剩下的庫存  2:存入庫存量於檔案中
            }
            Post("https://runway.nctu.edu.tw/api/check/" + stu.userid);

            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (num_49 == 0)
            {
                return;
            }

            choose = 2;

            show();
            if (choose == 2 && stu.result != -1)
            {
                num_49--;
                store_num(2);//read_or_write   1:讀取剩下的庫存  2:存入庫存量於檔案中
            }
            Post("https://runway.nctu.edu.tw/api/check/" + stu.userid);

            textBox1.Focus();
        }


        public void log(string data)//紀錄連線失敗的學號與使用F1時領餐的類別&時間    runway_log.txt、runway_error_id.txt
        {
            //----------------<runway_log.txt>-----------------
            string all_data = "";

            if (data != "")
            {
                // 先確定檔案存在
                if (File.Exists("..//..//Resources//runway_log.txt"))
                {
                    // 檔案設定
                    FileStream fileInput = new FileStream("..//..//Resources//runway_log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
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

                all_data = data + Environment.NewLine + all_data;

                // 檔案設定
                FileStream outFile = new FileStream("..//..//Resources//runway_log.txt", FileMode.Create, FileAccess.Write);

                // 建立檔案流
                StreamWriter streamOut = new StreamWriter(outFile);

                // 寫檔
                streamOut.WriteLine(date.Text + "  " + all_data);

                streamOut.Close();

            }




            //連線失敗學號紀錄
            if (forever_skip)
            {
                //----------------<runway_error_id.txt>-----------------
                all_data = "";

                // 先確定檔案存在
                if (File.Exists("..//..//Resources//runway_error_id.txt"))
                {
                    // 檔案設定
                    FileStream fileInput2 = new FileStream("..//..//Resources//runway_error_id.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    // 開啟檔案
                    StreamReader streamIn2 = new StreamReader(fileInput2);

                    // 讀檔
                    while (!streamIn2.EndOfStream)
                    {
                        all_data = all_data + streamIn2.ReadLine();
                        all_data = all_data + Environment.NewLine;
                    }
                    streamIn2.Close();
                }
                if(choose==1)
                    all_data = "  領取39元餐  ID: " + rec + Environment.NewLine + all_data;
                else
                    all_data = "  領取49元餐  ID: " + rec + Environment.NewLine + all_data;

                // 檔案設定
                FileStream outFile2 = new FileStream("..//..//Resources//runway_error_id.txt", FileMode.Create, FileAccess.Write);

                // 建立檔案流
                StreamWriter streamOut2 = new StreamWriter(outFile2);

                // 寫檔
                streamOut2.WriteLine(date.Text + "  " + all_data);

                streamOut2.Close();
            }
            

        }

        public void store_num(int read_or_write)//read_or_write   1:讀取剩下的庫存  2:存入庫存量於檔案中
        {
            string today = "";//檔案內的日期 EX:2017-02-08
            string remaind_39 = "";//檔案內的剩下39元早餐數 EX:40
            string remaind_49 = "";//檔案內的剩下49元早餐數 EX:20

            int count = 1;//讀取字串時確認讀到是哪一個

            // 先確定檔案存在
            if (File.Exists("..//..//Resources//runway_num.txt"))
            {
                // 檔案設定
                FileStream fileInput = new FileStream("..//..//Resources//runway_num.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                // 開啟檔案
                StreamReader streamIn = new StreamReader(fileInput);


                // 讀檔
                while (!streamIn.EndOfStream)
                {
                    if (count == 1)
                    {
                        today = streamIn.ReadLine();//讀取檔案內的日期 EX:2017-02-08    
                    }
                    else if (count == 2)
                    {
                        remaind_39= streamIn.ReadLine();//讀取檔案內的剩下39元早餐數 EX:40
                    }
                    else if (count == 3)
                    {
                        remaind_49 = streamIn.ReadLine();//讀取檔案內的剩下49元早餐數 EX:20
                    }
                    count++;
                }
                streamIn.Close();
            }

            if (String.Compare(DateTime.Now.ToString("yyyy-MM-dd"), today) == 0)//確認檔案內的日期是否與今日日期相同，"YES"則執行以下
            {
                if (read_or_write == 1)//read_or_write   1:讀取剩下的庫存
                {
                    num_39 = Convert.ToInt32(remaind_39);
                    num_49 = Convert.ToInt32(remaind_49);
                }
                else if (read_or_write == 2)//read_or_write   2:存入庫存量於檔案中
                {
                    // 檔案設定
                    FileStream outFile = new FileStream("..//..//Resources//runway_num.txt", FileMode.Create, FileAccess.Write);

                    // 建立檔案流
                    StreamWriter streamOut = new StreamWriter(outFile);

                    // 寫檔
                    for (int i = 0; i < 3; i++)
                    {
                        if (i == 0)
                            streamOut.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
                        else if (i == 1)
                            streamOut.WriteLine(Convert.ToString(num_39));
                        else
                            streamOut.WriteLine(Convert.ToString(num_49));
                    }

                    streamOut.Close();
                }
            }//確認檔案內的日期是否與今日日期相同，"NO"則執行以下
            else
            {
                num_39 = 30;//新的一天設置剩餘
                num_49 = 50;

                // 檔案設定
                FileStream outFile = new FileStream("..//..//Resources//runway_num.txt", FileMode.Create, FileAccess.Write);

                // 建立檔案流
                StreamWriter streamOut = new StreamWriter(outFile);

                // 寫檔
                for(int i = 0; i < 3; i++)
                {
                    if (i == 0)
                        streamOut.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
                    else if (i == 1)
                        streamOut.WriteLine(Convert.ToString(num_39));
                    else
                        streamOut.WriteLine(Convert.ToString(num_49));
                }
                

                streamOut.Close();
            }

                


        }

        private void button3_Click(object sender, EventArgs e)
        {
            af_inputID();
        }

        public void af_inputID()
        {

            if (!error && finish_choose)
            {
                try
                {

                    if (!error)
                    {
                        if (textBox1.Text.Length < 4)
                        {
                            textBox1.Text = "";
                            stu.result = -1;
                            draw = false;
                            except.Visible = true;
                            timer2.Enabled = true;
                            return;
                        }
                        if (forever_skip)
                        {
                            stu.userid = textBox1.Text;
                            if (judge_repeat_get(stu.userid))
                            {
                                stu.result = 0;
                                stu.status = 2;
                                stu.time = 0;
                            }
                            else
                            {
                                stu.result = 2;
                                stu.status = 1;
                                stu.time = 0;
                            }
                            
                        }
                        else
                        {

                            skip = false;
                            //特例設定----<因為網頁API有問題而設定，之後API正常後可刪除>----------

                            if (String.Compare(textBox1.Text, "0252001") == 0)
                            {
                                textBox1.Text = "40252001";
                            }

                            if (String.Compare(textBox1.Text, "20501") == 0)
                            {
                                stu.userid = "20501";
                                stu.result = 2;
                                stu.status = 1;
                                stu.time = 0;
                                skip = true;
                            }
                            //--------------------------------------------------------------------
                            if (!skip)
                            {
                                Get_StudentData("https://runway.nctu.edu.tw/api/check/" + textBox1.Text);
                            }

                            if (stu.status == 8)
                            {
                                stu.userid = textBox1.Text;
                                stu.result = 2;
                                stu.status = 1;
                                stu.time = 0;
                            }
                        }

                        rec = stu.userid;    

                        finish_choose = true;
                    }

                    //-------<For test>------
                    //stu.result = 2;
                    //stu.status = 1;

                    if (num_39 == 0 && num_49 == 0)
                    {
                        noitem.Text = "*錯誤:餐點皆已領畢";
                        noitem.Visible = true;
                        textBox1.Text = "";
                        return;
                    }
                    
                    if (num_49 == 0 && stu.result == 2)
                    {
                        stu.result = 1;

                    }
                    
                    if (stu.result == 2)
                    {
                        finish_choose = false;
                    }

                    

                    if (finish_choose && stu.result == 1)
                    {
                        if (num_39 == 0)
                        {
                            noitem.Text = "*錯誤:39元餐已領畢，你的等級不足以領49元餐";
                            noitem.Visible = true;
                            timer2.Enabled = true;
                            textBox1.Text = "";
                            return;

                        }


                        choose = 1;

                        show();

                        if (choose == 1 && stu.result != -1)
                        {
                            num_39--;
                            store_num(2);//read_or_write   1:讀取剩下的庫存  2:存入庫存量於檔案中
                        }

                        Post("https://runway.nctu.edu.tw/api/check/" + textBox1.Text);
                    }
                    else if (stu.result == 2 && !finish_choose)
                    {
                        button1.Visible = true;
                        button2.Visible = true;
                        qu39.Visible = true;
                        qu49.Visible = true;
                        label1.Visible = true;
                        button2.Focus();

                        user.Visible = false;
                        condition.Visible = false;
                        overtime.Visible = false;
                        timer1.Enabled = false;

                        except.Visible = false;
                        noitem.Visible = false;
                        command.Visible = false;
                        textBox1.Visible = false;
                        button3.Visible = false;
                        no49.Visible = false;
                    }
                    else if (stu.result == 0)
                    {
                        show();
                    }
                    textBox1.Text = "";

                }
                catch (WebException err)
                {
                    textBox1.Text = "";
                    stu.result = -1;
                    draw = false;
                    except.Visible = true;
                    timer2.Enabled = true;
                    //error = true;
                    if (err.Response == null)
                    {
                        log("No response:" + err.Message);
                    }
                    else
                    {
                        using (StreamReader sr = new StreamReader(err.Response.GetResponseStream()))
                        {
                            log(JObject.Parse(sr.ReadToEnd()).ToString());
                        }
                    }

                }
                catch (Exception err)
                {
                    textBox1.Text = "";
                    stu.result = -1;
                    draw = false;
                    except.Visible = true;
                    timer2.Enabled = true;
                    //error = true;
                    log("No response:" + err.Message);
                }
            }
        }

        public bool judge_repeat_get(string ID)//在特殊狀態開啟下，是否重複領餐   runway_error_id.txt
        {
            string all_data = "";//runway_error_id.txt的內容
            string record_date = "";
            string record_ID = "";

            // 先確定檔案存在
            if (File.Exists("..//..//Resources//runway_error_id.txt"))
            {
                // 檔案設定
                FileStream fileInput2 = new FileStream("..//..//Resources//runway_error_id.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                // 開啟檔案
                StreamReader streamIn2 = new StreamReader(fileInput2);

                // 讀檔
                while (!streamIn2.EndOfStream)
                {
                    all_data =streamIn2.ReadLine();

                    string[] tem_split = all_data.Split(' ');//字串中切掉的部分
                    int len = 0;
                    string[] tem2_split = new string[5];
                    for (int i = 0; i < tem_split.Length; i++)
                    {
                        if (tem_split[i] != "")
                        {
                            if (len == 5)
                            {
                                //error = true;
                                break;
                            }
                            tem2_split[len] = tem_split[i];
                            len++;
                        }

                    }
                    if (len != 5)
                    {
                        //error = true;
                    }
                    if (!error)
                    {
                        record_date = tem2_split[0];
                        record_ID = tem2_split[4];
                        if (String.Compare(record_date, DateTime.Now.ToString("yyyy-MM-dd(ddd)")) != 0)
                            break;
                        else
                        {
                            if (String.Compare(record_ID, ID) == 0)
                            {
                                streamIn2.Close();
                                return true;
                            }
                                
                        }
                    }
                    else
                    {
                        log("Error:Parameter identification failed");
                    }
                    
                }
                streamIn2.Close();
            }
            return false;

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            qu39.Visible = false;
            qu49.Visible = false;
            label1.Visible = false;

            except.Visible = false;
            noitem.Visible = false;
            command.Visible = true;
            textBox1.Visible = true;
            button3.Visible = true;
            if (num_49 == 0 && num_39 > 0)
            {
                no49.Visible = true;
            }

            user.Visible = false;
            condition.Visible = false;
            overtime.Visible = false;

            draw = false;

            if (num_39 == 0 && num_49 == 0)
            {
                noitem.Text = "*錯誤:餐點皆已領畢";
                noitem.Visible = true;
            }

            timer2.Enabled = false;
            textBox1.Focus();
        }

        public void show()
        {
            button1.Visible = false;
            button2.Visible = false;
            qu39.Visible = false;
            qu49.Visible = false;
            label1.Visible = false;

            except.Visible = false;
            noitem.Visible = false;
            command.Visible = false;
            textBox1.Visible = false;
            button3.Visible = false;
            no49.Visible = false;

            user.Visible = true;
            condition.Visible = true;
            overtime.Visible = true;

            timer1.Enabled = true;
            timer2.Enabled = true;

            finish_choose = true;

            draw = true;

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Environment.Exit(Environment.ExitCode);
            }
            if (e.KeyCode == Keys.Enter)
            {
                af_inputID();
            }
            if (e.KeyCode == Keys.F1 && textBox1.Visible)
            {
                forever_skip = !forever_skip;
                if (forever_skip)
                    sp_event.Text = "特殊狀態:開啟";
                else
                    sp_event.Text = "特殊狀態:關閉";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Visible)
            {
                forever_skip = !forever_skip;
                if (forever_skip)
                    sp_event.Text = "特殊狀態:開啟";
                else
                    sp_event.Text = "特殊狀態:關閉";
            }
            
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            
            if(e.KeyCode == Keys.NumPad1)
            {
                if (num_39 == 0)
                {
                    return;
                }

                choose = 1;

                show();
                if (choose == 1 && stu.result != -1)
                {
                    num_39--;
                    store_num(2);//read_or_write   1:讀取剩下的庫存  2:存入庫存量於檔案中
                }
                Post("https://runway.nctu.edu.tw/api/check/" + stu.userid);

                textBox1.Focus();
            }
            if (e.KeyCode == Keys.NumPad2)
            {
                if (num_49 == 0)
                {
                    return;
                }

                choose = 2;

                show();
                if (choose == 2 && stu.result != -1)
                {
                    num_49--;
                    store_num(2);//read_or_write   1:讀取剩下的庫存  2:存入庫存量於檔案中
                }
                Post("https://runway.nctu.edu.tw/api/check/" + stu.userid);

                textBox1.Focus();
            }
            
        }

        private void button4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && textBox1.Visible)
            {
                forever_skip = !forever_skip;
                if (forever_skip)
                    sp_event.Text = "特殊狀態:開啟";
                else
                    sp_event.Text = "特殊狀態:關閉";
            }
        }
    }
}
