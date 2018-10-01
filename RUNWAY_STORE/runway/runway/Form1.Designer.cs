namespace runway
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.condition = new System.Windows.Forms.Label();
            this.overtime = new System.Windows.Forms.Label();
            this.user = new System.Windows.Forms.Label();
            this.date = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.command = new System.Windows.Forms.Label();
            this.except = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.rem_39 = new System.Windows.Forms.Label();
            this.rem_49 = new System.Windows.Forms.Label();
            this.noitem = new System.Windows.Forms.Label();
            this.no49 = new System.Windows.Forms.Label();
            this.sp_event = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.qu39 = new System.Windows.Forms.Label();
            this.qu49 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // condition
            // 
            this.condition.AutoSize = true;
            this.condition.BackColor = System.Drawing.Color.Transparent;
            this.condition.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.condition.ForeColor = System.Drawing.SystemColors.ControlText;
            this.condition.Location = new System.Drawing.Point(499, 529);
            this.condition.Name = "condition";
            this.condition.Size = new System.Drawing.Size(236, 48);
            this.condition.TabIndex = 1;
            this.condition.Text = "condition";
            // 
            // overtime
            // 
            this.overtime.AutoSize = true;
            this.overtime.BackColor = System.Drawing.Color.Transparent;
            this.overtime.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.overtime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.overtime.Location = new System.Drawing.Point(499, 603);
            this.overtime.Name = "overtime";
            this.overtime.Size = new System.Drawing.Size(212, 48);
            this.overtime.TabIndex = 1;
            this.overtime.Text = "overtime";
            // 
            // user
            // 
            this.user.AutoSize = true;
            this.user.BackColor = System.Drawing.Color.Transparent;
            this.user.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.user.ForeColor = System.Drawing.SystemColors.ControlText;
            this.user.Location = new System.Drawing.Point(499, 459);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(116, 48);
            this.user.TabIndex = 1;
            this.user.Text = "user";
            // 
            // date
            // 
            this.date.AutoSize = true;
            this.date.BackColor = System.Drawing.Color.Transparent;
            this.date.Font = new System.Drawing.Font("標楷體", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.date.ForeColor = System.Drawing.SystemColors.ControlText;
            this.date.Location = new System.Drawing.Point(1077, 12);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(68, 28);
            this.date.TabIndex = 1;
            this.date.Text = "date";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "wrong.png");
            this.imageList1.Images.SetKeyName(1, "correct.png");
            this.imageList1.Images.SetKeyName(2, "errorstop.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(57, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "text";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(61, 173);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(310, 96);
            this.button1.TabIndex = 2;
            this.button1.Text = "39元早餐(1)";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(61, 315);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(310, 97);
            this.button2.TabIndex = 2;
            this.button2.Text = "49元早餐(2)";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button2_KeyDown);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("標楷體", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox1.Location = new System.Drawing.Point(87, 492);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(311, 52);
            this.textBox1.TabIndex = 4;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("標楷體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.Location = new System.Drawing.Point(92, 562);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(197, 56);
            this.button3.TabIndex = 5;
            this.button3.Text = "Enter鍵輸入";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // command
            // 
            this.command.AutoSize = true;
            this.command.BackColor = System.Drawing.Color.Transparent;
            this.command.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.command.ForeColor = System.Drawing.SystemColors.ControlText;
            this.command.Location = new System.Drawing.Point(84, 429);
            this.command.Name = "command";
            this.command.Size = new System.Drawing.Size(188, 48);
            this.command.TabIndex = 1;
            this.command.Text = "command";
            // 
            // except
            // 
            this.except.AutoSize = true;
            this.except.BackColor = System.Drawing.Color.Transparent;
            this.except.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.except.ForeColor = System.Drawing.Color.Red;
            this.except.Location = new System.Drawing.Point(291, 429);
            this.except.Name = "except";
            this.except.Size = new System.Drawing.Size(164, 48);
            this.except.TabIndex = 1;
            this.except.Text = "except";
            // 
            // timer2
            // 
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // rem_39
            // 
            this.rem_39.AutoSize = true;
            this.rem_39.BackColor = System.Drawing.Color.Transparent;
            this.rem_39.Font = new System.Drawing.Font("標楷體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rem_39.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rem_39.Location = new System.Drawing.Point(1077, 69);
            this.rem_39.Name = "rem_39";
            this.rem_39.Size = new System.Drawing.Size(137, 40);
            this.rem_39.TabIndex = 1;
            this.rem_39.Text = "rem_39";
            // 
            // rem_49
            // 
            this.rem_49.AutoSize = true;
            this.rem_49.BackColor = System.Drawing.Color.Transparent;
            this.rem_49.Font = new System.Drawing.Font("標楷體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rem_49.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rem_49.Location = new System.Drawing.Point(1077, 124);
            this.rem_49.Name = "rem_49";
            this.rem_49.Size = new System.Drawing.Size(137, 40);
            this.rem_49.TabIndex = 1;
            this.rem_49.Text = "rem_49";
            // 
            // noitem
            // 
            this.noitem.AutoSize = true;
            this.noitem.BackColor = System.Drawing.Color.Transparent;
            this.noitem.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.noitem.ForeColor = System.Drawing.Color.Red;
            this.noitem.Location = new System.Drawing.Point(291, 571);
            this.noitem.Name = "noitem";
            this.noitem.Size = new System.Drawing.Size(164, 48);
            this.noitem.TabIndex = 1;
            this.noitem.Text = "noitem";
            // 
            // no49
            // 
            this.no49.AutoSize = true;
            this.no49.BackColor = System.Drawing.Color.Transparent;
            this.no49.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.no49.ForeColor = System.Drawing.Color.Red;
            this.no49.Location = new System.Drawing.Point(831, 429);
            this.no49.Name = "no49";
            this.no49.Size = new System.Drawing.Size(116, 48);
            this.no49.TabIndex = 1;
            this.no49.Text = "no49";
            // 
            // sp_event
            // 
            this.sp_event.AutoSize = true;
            this.sp_event.BackColor = System.Drawing.Color.Transparent;
            this.sp_event.Font = new System.Drawing.Font("標楷體", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.sp_event.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sp_event.Location = new System.Drawing.Point(59, 39);
            this.sp_event.Name = "sp_event";
            this.sp_event.Size = new System.Drawing.Size(177, 40);
            this.sp_event.TabIndex = 1;
            this.sp_event.Text = "sp_event";
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("標楷體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button4.Location = new System.Drawing.Point(241, 39);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(107, 51);
            this.button4.TabIndex = 5;
            this.button4.Text = "F1變更";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button4_KeyDown);
            // 
            // qu39
            // 
            this.qu39.AutoSize = true;
            this.qu39.BackColor = System.Drawing.Color.Transparent;
            this.qu39.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.qu39.ForeColor = System.Drawing.Color.Black;
            this.qu39.Location = new System.Drawing.Point(928, 241);
            this.qu39.Name = "qu39";
            this.qu39.Size = new System.Drawing.Size(116, 48);
            this.qu39.TabIndex = 1;
            this.qu39.Text = "qu39";
            // 
            // qu49
            // 
            this.qu49.AutoSize = true;
            this.qu49.BackColor = System.Drawing.Color.Transparent;
            this.qu49.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.qu49.ForeColor = System.Drawing.Color.Black;
            this.qu49.Location = new System.Drawing.Point(928, 297);
            this.qu49.Name = "qu49";
            this.qu49.Size = new System.Drawing.Size(116, 48);
            this.qu49.TabIndex = 1;
            this.qu49.Text = "qu49";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(108, 263);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 48);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1249, 682);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.overtime);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rem_49);
            this.Controls.Add(this.rem_39);
            this.Controls.Add(this.date);
            this.Controls.Add(this.noitem);
            this.Controls.Add(this.no49);
            this.Controls.Add(this.except);
            this.Controls.Add(this.sp_event);
            this.Controls.Add(this.qu49);
            this.Controls.Add(this.qu39);
            this.Controls.Add(this.command);
            this.Controls.Add(this.user);
            this.Controls.Add(this.condition);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "runway";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label condition;
        private System.Windows.Forms.Label overtime;
        private System.Windows.Forms.Label user;
        private System.Windows.Forms.Label date;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label command;
        private System.Windows.Forms.Label except;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label rem_39;
        private System.Windows.Forms.Label rem_49;
        private System.Windows.Forms.Label noitem;
        private System.Windows.Forms.Label no49;
        private System.Windows.Forms.Label sp_event;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label qu39;
        private System.Windows.Forms.Label qu49;
        private System.Windows.Forms.Label label2;
    }
}

