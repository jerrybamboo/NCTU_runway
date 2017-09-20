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
        Student stu = new Student { userid="4818" , name = "None", result = 1, status = 1, time = 0 };//當前顯示在螢幕上的學生

        int choose = 1;          //選擇兌換的早餐    1:39元早餐      2:49元早餐
        string pre_id = "";      //上一個GET的ID

        bool finish_choose= true;//是否選好領取39或49元早餐
        bool error = false;      //是否程式錯誤

        bool already=false;      //得到不同的ID將執行1次
        bool already_post = true;//是否已經post了
        string responseFromServer;



        public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();//Create Standalone SDK class dynamicly.
        bool bIsConnected = false;//the boolean value identifies whether the device is connected
        int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.
        int idwErrorCode = 0;

        //test
        string word= "http://localhost:11115/api/values";
        int test_data =1;

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
            //重要這邊一定要設成10
            axZKFPEngX1.FPEngineVersion = "10";
            //初始化USB指紋辨識SDK
            axZKFPEngX1.InitEngine();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;

            max_x = 1536;
            max_y = 801;

            label1.Text = "你的等級已達5級，可選擇39元早餐或49元早餐";
            date.Text = DateTime.Now.ToString("yyyy-MM-dd(ddd)  HH:mm:ss");

            //--------------------<設定位置>----------------------
            user.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5));
            condition.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5 + 100));
            overtime.Location = new Point((int)(max_x * 2 / 5), (int)(max_y * 3 / 5 + 200));
            date.Location= new Point(max_x - 400, 100);
            button1.Location = new Point((int)(max_x / 3 ), (int)(max_y * 3 / 5 + 100));
            button2.Location = new Point((int)(max_x / 2 + 70), (int)(max_y * 3 / 5 + 100));
            label1.Location = new Point((int)(max_x / 4), (int)(max_y * 3 / 5));


            //------------------------------------------------------

            button1.Visible = false;
            button2.Visible = false;

            label1.Visible = false;

        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape)
            {
                Environment.Exit(Environment.ExitCode);
            }
        }

        public void Get_ID(string mycontent)//由辨識API得到ID
        {
            string[] tem_split = mycontent.Split(',', '"', '{', '}', ':', '[', ']');
            int len = 0;
            string[] tem2_split = new string[6];

            for (int i = 0; i < tem_split.Length; i++)
            {
                if (tem_split[i] != "")
                {
                    if (len == 6)
                    {
                        error = true;
                        break;
                    }
                    tem2_split[len] = tem_split[i];
                    len++;
                }

            }
            if (len != 6)
            {
                error = true;
            }
            if (!error && len == 6)
            {
                stu.GetID_result = Convert.ToInt32(tem2_split[1]);
                stu.message = tem2_split[3];
                stu.userid = tem2_split[5];

            }
            else if (len == 4)
            {
                stu.GetID_result = Convert.ToInt32(tem2_split[1]);
                stu.message = tem2_split[3];

                log("Error:ID identification failed" + "message:" + stu.message);
                user.Text = "系統發生錯誤";
                user.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5));
                condition.Visible = false;
                overtime.Visible = false;
            }
            else
            {
                log("Error:ID identification failed");
                user.Text = "系統發生錯誤";
                user.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5));
                //user.Location = new Point(10, (int)(max_y * 3 / 5));
                condition.Visible = false;
                overtime.Visible = false;
            }



        }



        public void Get_StudentData(string url)//GET此學生的相關參數
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:11115/api/values");

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
                                error = true;
                                break;
                            }
                            tem2_split[len] = tem_split[i];
                            len++;
                        }

                    }
                    if (len != 8)
                    {
                        error = true;
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
                        user.Text = "系統發生錯誤";
                        user.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5));
                        //user.Location = new Point(10, (int)(max_y * 3 / 5));
                        condition.Visible = false;
                        overtime.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    error = true;
                    log("在提取您所要求的" + url + "網頁時發生錯誤。" + "請檢查您所鍵入的 URL 以及 Internet 連線，並再次嘗試。WebException:" + ex.Message);
                    user.Text = "系統發生錯誤";
                    user.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5));
                    //user.Location = new Point(10, (int)(max_y * 3 / 5));
                    condition.Visible = false;
                    overtime.Visible = false;
                }
            }
        }

        public void Post(string url)
        {
            try
            {
                // Create a request using a URL that can receive a post. 
                WebRequest request = WebRequest.Create(url);
                // Set the Method property of the request to POST.
                request.Method = "POST";
                // Create POST data and convert it to a byte array.
                string postData = "{\"choose\":"+Convert.ToString(choose)+"}";
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
                //user.Text = responseFromServer;
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

            }
            catch(Exception ex)
            {
                error = true;
                log("在提取您所要求的" + url + "網頁時發生錯誤。" + "請檢查您所鍵入的 URL 以及 Internet 連線，並再次嘗試。WebException:" + ex.Message);
                user.Text = "系統發生錯誤";
                user.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5));
                //user.Location = new Point(10, (int)(max_y * 3 / 5));
                condition.Visible = false;
                overtime.Visible = false;
            }


        }


        

        private void timer1_Tick(object sender, EventArgs e)
        {

            //error = false;//為了讓程式遇到error持續執行


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
            /*
            if(error)
                e.Graphics.DrawImage(imageList1.Images[2], max_x / 2 + 400, max_y / 2 - 100, 128, 128);
            else if (stu.result == 0 || stu.result == 1)
                e.Graphics.DrawImage(imageList1.Images[stu.result], max_x / 2 + 400, max_y / 2 - 50, 128, 128);
            else
                e.Graphics.DrawImage(imageList1.Images[1], max_x / 2 + 400, max_y / 2 - 50, 128, 128);
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            choose = 1;
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;

            user.Visible = true;
            condition.Visible = true;
            overtime.Visible = true;
            timer1.Enabled = true;

            finish_choose = true;

            Post("https://runway.nctu.edu.tw/api/check/" + stu.userid);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            choose = 2;
            button1.Visible = false;
            button2.Visible = false;
            label1.Visible = false;

            user.Visible = true;
            condition.Visible = true;
            overtime.Visible = true;
            timer1.Enabled = true;

            finish_choose = true;

            Post("https://runway.nctu.edu.tw/api/check/" + stu.userid);

        }


        //當按壓手指離開指紋儀表面時，觸發此事件
        
        private void axZKFPEngX1_OnFingerLeaving(object sender, EventArgs e)
        {

            if (!error && finish_choose)
            {
                object oImage = new object();
                //先看看能不能取得指紋圖像
                if (axZKFPEngX1.GetFingerImage(ref oImage))
                {
                    //擷取USB指紋儀圖片並顯示
                    //MemoryStream mStream = new MemoryStream((byte[])oImage);
                    //Image image = Image.FromStream(mStream);

                    //建立一個Request物件準備發送至server端程式
                    HttpWebRequest req = HttpWebRequest.Create("http://runway.nctu.edu.tw/api/finger") as HttpWebRequest;
                    req.Timeout = 10 * 1000;
                    req.Method = "POST";
                    req.ContentType = "application/json";
                    //用JSON格式發送資料
                    JObject joReqData = new JObject();
                    //取得base64指紋資料
                    joReqData["fingerData"] = axZKFPEngX1.GetTemplateAsString();
                    //發送&接收資料
                    try
                    {
                        /*
                        using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
                        {
                            sw.Write(joReqData.ToString());
                        }
                        HttpWebResponse res = req.GetResponse() as HttpWebResponse;
                        using (StreamReader sr = new StreamReader(res.GetResponseStream()))
                        {
                            
                            Get_ID(JObject.Parse(sr.ReadToEnd()).ToString());
                            if (!error)
                            {
                                Get_StudentData("https://runway.nctu.edu.tw/api/check/" + stu.userid);

                                finish_choose = true;
                            }
                            if (stu.result == 2)
                            {
                                finish_choose = false;
                            }
                            if (finish_choose && stu.result==1)
                            {
                                choose = 1;
                                Post("https://runway.nctu.edu.tw/api/check/" + stu.userid);
                            }
                            else if( stu.result == 2 &&!finish_choose)
                            {

                                button1.Visible = true;
                                button2.Visible = true;
                                label1.Visible = true;

                                user.Visible = false;
                                condition.Visible = false;
                                overtime.Visible = false;
                                timer1.Enabled = false;
                            }
                            
                            
                        }
                        */

                        //---------------if no 辨識API-----------
                        if (!error)
                        {
                            Get_StudentData(word);

                            finish_choose = true;
                        }
                        if (stu.result == 2)
                        {
                            finish_choose = false;
                        }
                        if (finish_choose && stu.result == 1)
                        {
                            choose = 1;
                            Post("https://runway.nctu.edu.tw/api/check/" + stu.userid);
                        }
                        else if (stu.result == 2 &&!finish_choose)
                        {

                            button1.Visible = true;
                            button2.Visible = true;
                            label1.Visible = true;

                            user.Visible = false;
                            condition.Visible = false;
                            overtime.Visible = false;
                            timer1.Enabled = false;
                        }

                        //----------------------------------------------
                    }
                    catch (WebException err)
                    {
                        error = true;
                        if (err.Response == null)
                        {
                            log("No response:" + err.Message);
                            user.Text = "系統發生錯誤";
                            user.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5));
                            //user.Location = new Point(10, (int)(max_y * 3 / 5));
                            condition.Visible = false;
                            overtime.Visible = false;

                        }
                        else
                        {
                            using (StreamReader sr = new StreamReader(err.Response.GetResponseStream()))
                            {
                                log(JObject.Parse(sr.ReadToEnd()).ToString());
                                user.Text = "系統發生錯誤";
                                user.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5));
                                //user.Location = new Point(10, (int)(max_y * 3 / 5));
                                condition.Visible = false;
                                overtime.Visible = false;
                            }
                        }

                    }
                    catch (Exception err)
                    {
                        error = true;
                        log("No response:" + err.Message);
                        user.Text = "系統發生錯誤";
                        user.Location = new Point((int)(max_x / 3), (int)(max_y * 3 / 5));
                        //user.Location = new Point(10, (int)(max_y * 3 / 5));
                        condition.Visible = false;
                        overtime.Visible = false;
                    }
                }
            }
        }
        public void log(string data)
        {
            // 檔案設定
            FileStream outFile = new FileStream("..//..//Resources//runway_log", FileMode.Create, FileAccess.Write);

            // 建立檔案流
            StreamWriter streamOut = new StreamWriter(outFile);

            // 寫檔
            streamOut.WriteLine(date.Text+"  " + data + "\n");

            streamOut.Close();


        }
    }
}
