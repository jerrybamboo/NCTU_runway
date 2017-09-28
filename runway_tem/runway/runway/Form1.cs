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


namespace runway
{
    public partial class Form1 : Form
    {

        string text = "待命中";//不同status所要顯示的文字
        int max_x;//全螢幕寬度
        int max_y;//全螢幕高度
        Student stu = new Student { userid = "4818", name = "None", result = -1, status = 1, time = 0 };//當前顯示在螢幕上的學生

        int choose = 1;          //選擇兌換的早餐    1:39元早餐      2:49元早餐

        bool finish_choose = true;//是否選好領取39或49元早餐
        bool error = false;      //是否程式錯誤，方便debug用
        bool draw = false;

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

            max_x = this.ClientSize.Width;
            max_y = this.ClientSize.Height;

            //max_x = 1536;
            //max_y = 801;

            label1.Text = "你的等級已達5級，可選擇39元早餐或49元早餐";
            date.Text = DateTime.Now.ToString("yyyy-MM-dd(ddd)  HH:mm:ss");
            command.Text = "請輸入學號";
            except.Text = "*錯誤:學號輸入錯誤";

            //--------------------<設定位置>----------------------
            user.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5));
            condition.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5 + 100));
            overtime.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5 + 200));
            date.Location = new Point(max_x - 400, 100);
            button1.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5 + 100));
            button2.Location = new Point((int)(max_x / 2 + 70), (int)(max_y * 3 / 5 + 100));
            label1.Location = new Point((int)(max_x / 4), (int)(max_y * 3 / 5));

            command.Location = user.Location;
            textBox1.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5 + 50));
            except.Location = new Point((int)(max_x * 2 / 5) - 20, (int)(max_y * 3 / 5 + 120));
            button3.Location = new Point((int)(max_x * 2 / 5) + 250, (int)(max_y * 3 / 5 + 50));
            //------------------------------------------------------

            user.Visible = false;
            condition.Visible = false;
            overtime.Visible = false;

            button1.Visible = false;
            button2.Visible = false;

            label1.Visible = false;


            except.Visible = false;
       
            command.Visible = true;
            textBox1.Visible = true;
            button3.Visible = true;

        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                Environment.Exit(Environment.ExitCode);
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
                    string[] tem_split = mycontent.Split(',', '"', '{', '}', ':', '[', ']');
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
                        stu.name = tem2_split[1];
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
                    stu.result = -1;
                    draw = false;
                    except.Visible = true;
                    //error = true;
                    log("在提取您所要求的" + url + "網頁時發生錯誤。" + "請檢查您所鍵入的 URL 以及 Internet 連線，並再次嘗試。WebException:" + ex.Message);
                }
            }
        }

        public void Post(string url)
        {
            string responseFromServer;
            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(url);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.
                string postData = "{\"choose\":" + Convert.ToString(choose) + "}";
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

                check_post(responseFromServer);

                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();



                timer2.Enabled = true;
            }
            catch (Exception ex)
            {
                stu.result = -1;
                draw = false;
                //error = true;
                log("在提取您所要求的" + url + "網頁時發生錯誤。" + "請檢查您所鍵入的 URL 以及 Internet 連線，並再次嘗試。WebException:" + ex.Message);
            }


        }

        public void check_post(string responseFromServer)
        {
            string[] tem_split = responseFromServer.Split(',', '"', '{', '}', ':', '[', ']');
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
            
            error = false;//為了讓程式遇到error持續執行


            date.Text = DateTime.Now.ToString("yyyy-MM-dd(ddd)  HH:mm:ss");
            if (!error)
            {

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
                user.Text = "學號:" + stu.name;
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
            if (draw)
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
            choose = 1;

            show();

            Post("https://runway.nctu.edu.tw/api/check/" + stu.userid);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            choose = 2;

            show();

            Post("https://runway.nctu.edu.tw/api/check/" + stu.userid);

        }


        public void log(string data)
        {
            string all_data = "";

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

        private void button3_Click(object sender, EventArgs e)
        {

            if (!error && finish_choose)
            {
                try
                {

                    if (!error)
                    {
                        Get_StudentData("https://runway.nctu.edu.tw/api/check/" + textBox1.Text);

                        finish_choose = true;
                    }
                    if (stu.result == 2)
                    {
                        finish_choose = false;
                    }
                    if (finish_choose && stu.result == 1)
                    {
                        choose = 1;

                        show();

                        Post("https://runway.nctu.edu.tw/api/check/" + textBox1.Text);
                    }
                    else if (stu.result == 2 && !finish_choose)
                    {

                        button1.Visible = true;
                        button2.Visible = true;
                        label1.Visible = true;

                        user.Visible = false;
                        condition.Visible = false;
                        overtime.Visible = false;
                        timer1.Enabled = false;

                        except.Visible = false;
                        command.Visible = false;
                        textBox1.Visible = false;
                        button3.Visible = false;
                    }
                    else if (stu.result == 0)
                    {
                        show();
                    }
                    textBox1.Text = "";
                    //----------------------------------------------
                }
                catch (WebException err)
                {
                    stu.result = -1;
                    draw = false;
                    except.Visible = true;
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
                    stu.result = -1;
                    draw = false;
                    except.Visible = true;
                    //error = true;
                    log("No response:" + err.Message);
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;

            except.Visible = false;
            command.Visible = true;
            textBox1.Visible = true;
            button3.Visible = true;

            user.Visible = false;
            condition.Visible = false;
            overtime.Visible = false;

            draw = false;

            timer2.Enabled = false;
        }

        public void show()
        {
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;

            except.Visible = false;
            command.Visible = false;
            textBox1.Visible = false;
            button3.Visible = false;

            user.Visible = true;
            condition.Visible = true;
            overtime.Visible = true;

            timer1.Enabled = true;
            timer2.Enabled = true;

            finish_choose = true;

            draw = true;

        }
    }
}
