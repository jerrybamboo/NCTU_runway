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
            this.condition.Location = new System.Drawing.Point(498, 528);
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
            this.overtime.Location = new System.Drawing.Point(498, 602);
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
            this.user.Location = new System.Drawing.Point(498, 460);
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
            this.date.Location = new System.Drawing.Point(1078, 13);
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
            this.label1.Location = new System.Drawing.Point(57, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "text";
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(62, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 95);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Font = new System.Drawing.Font("標楷體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(62, 315);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(229, 97);
            this.button2.TabIndex = 2;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("標楷體", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBox1.Location = new System.Drawing.Point(87, 493);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(310, 48);
            this.textBox1.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("標楷體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.Location = new System.Drawing.Point(87, 574);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 45);
            this.button3.TabIndex = 5;
            this.button3.Text = "確認";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // command
            // 
            this.command.AutoSize = true;
            this.command.BackColor = System.Drawing.Color.Transparent;
            this.command.Font = new System.Drawing.Font("標楷體", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.command.ForeColor = System.Drawing.SystemColors.ControlText;
            this.command.Location = new System.Drawing.Point(84, 430);
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
            this.except.Location = new System.Drawing.Point(290, 430);
            this.except.Name = "except";
            this.except.Size = new System.Drawing.Size(164, 48);
            this.except.TabIndex = 1;
            this.except.Text = "except";
            // 
            // timer2
            // 
            this.timer2.Interval = 5000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1249, 761);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.overtime);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.date);
            this.Controls.Add(this.except);
            this.Controls.Add(this.command);
            this.Controls.Add(this.user);
            this.Controls.Add(this.condition);
            this.DoubleBuffered = true;
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
    }
}

