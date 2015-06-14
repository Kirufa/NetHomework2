namespace ChatClient
{
    partial class Form_Client
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_TCP_Connect = new System.Windows.Forms.Button();
            this.textBox_ServerIP = new System.Windows.Forms.TextBox();
            this.panel_Display = new System.Windows.Forms.Panel();
            this.textBox_Text = new System.Windows.Forms.TextBox();
            this.button_SendPic = new System.Windows.Forms.Button();
            this.button_SendText = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label_IP = new System.Windows.Forms.Label();
            this.label_Name = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_TCP_Connect
            // 
            this.button_TCP_Connect.Location = new System.Drawing.Point(12, 70);
            this.button_TCP_Connect.Name = "button_TCP_Connect";
            this.button_TCP_Connect.Size = new System.Drawing.Size(75, 23);
            this.button_TCP_Connect.TabIndex = 0;
            this.button_TCP_Connect.Text = "連線";
            this.button_TCP_Connect.UseVisualStyleBackColor = true;
            this.button_TCP_Connect.Click += new System.EventHandler(this.button_TCP_Connect_Click);
            // 
            // textBox_ServerIP
            // 
            this.textBox_ServerIP.Location = new System.Drawing.Point(123, 9);
            this.textBox_ServerIP.Name = "textBox_ServerIP";
            this.textBox_ServerIP.Size = new System.Drawing.Size(100, 22);
            this.textBox_ServerIP.TabIndex = 1;
            this.textBox_ServerIP.Text = "192.168.0.100";
            this.textBox_ServerIP.TextChanged += new System.EventHandler(this.textBox_ServerIP_TextChanged);
            // 
            // panel_Display
            // 
            this.panel_Display.Location = new System.Drawing.Point(14, 99);
            this.panel_Display.Name = "panel_Display";
            this.panel_Display.Size = new System.Drawing.Size(511, 236);
            this.panel_Display.TabIndex = 2;
            // 
            // textBox_Text
            // 
            this.textBox_Text.Location = new System.Drawing.Point(14, 341);
            this.textBox_Text.Name = "textBox_Text";
            this.textBox_Text.Size = new System.Drawing.Size(426, 22);
            this.textBox_Text.TabIndex = 3;
            // 
            // button_SendPic
            // 
            this.button_SendPic.Location = new System.Drawing.Point(446, 370);
            this.button_SendPic.Name = "button_SendPic";
            this.button_SendPic.Size = new System.Drawing.Size(75, 23);
            this.button_SendPic.TabIndex = 4;
            this.button_SendPic.Text = "傳送圖片";
            this.button_SendPic.UseVisualStyleBackColor = true;
            this.button_SendPic.Click += new System.EventHandler(this.button_SendPic_Click);
            // 
            // button_SendText
            // 
            this.button_SendText.Location = new System.Drawing.Point(446, 341);
            this.button_SendText.Name = "button_SendText";
            this.button_SendText.Size = new System.Drawing.Size(75, 23);
            this.button_SendText.TabIndex = 5;
            this.button_SendText.Text = "傳送";
            this.button_SendText.UseVisualStyleBackColor = true;
            this.button_SendText.Click += new System.EventHandler(this.button_SendText_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(625, 99);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(314, 274);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(123, 40);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(100, 22);
            this.textBox_Name.TabIndex = 7;
            this.textBox_Name.Text = "小明";
            this.textBox_Name.TextChanged += new System.EventHandler(this.textBox_Name_TextChanged);
            // 
            // label_IP
            // 
            this.label_IP.AutoSize = true;
            this.label_IP.Location = new System.Drawing.Point(12, 12);
            this.label_IP.Name = "label_IP";
            this.label_IP.Size = new System.Drawing.Size(105, 12);
            this.label_IP.TabIndex = 8;
            this.label_IP.Text = "伺服器IP位址(IPv4)";
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Location = new System.Drawing.Point(10, 40);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(29, 12);
            this.label_Name.TabIndex = 9;
            this.label_Name.Text = "暱稱";
            // 
            // Form_Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 551);
            this.Controls.Add(this.label_Name);
            this.Controls.Add(this.label_IP);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_SendText);
            this.Controls.Add(this.button_SendPic);
            this.Controls.Add(this.textBox_Text);
            this.Controls.Add(this.panel_Display);
            this.Controls.Add(this.textBox_ServerIP);
            this.Controls.Add(this.button_TCP_Connect);
            this.Name = "Form_Client";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_Client_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_TCP_Connect;
        private System.Windows.Forms.TextBox textBox_ServerIP;
        private System.Windows.Forms.Panel panel_Display;
        private System.Windows.Forms.TextBox textBox_Text;
        private System.Windows.Forms.Button button_SendPic;
        private System.Windows.Forms.Button button_SendText;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label_IP;
        private System.Windows.Forms.Label label_Name;
    }
}

