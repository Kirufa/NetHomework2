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
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // button_TCP_Connect
            // 
            this.button_TCP_Connect.Location = new System.Drawing.Point(12, 12);
            this.button_TCP_Connect.Name = "button_TCP_Connect";
            this.button_TCP_Connect.Size = new System.Drawing.Size(75, 23);
            this.button_TCP_Connect.TabIndex = 0;
            this.button_TCP_Connect.Text = "連線";
            this.button_TCP_Connect.UseVisualStyleBackColor = true;
            this.button_TCP_Connect.Click += new System.EventHandler(this.button_TCP_Connect_Click);
            // 
            // textBox_ServerIP
            // 
            this.textBox_ServerIP.Location = new System.Drawing.Point(93, 14);
            this.textBox_ServerIP.Name = "textBox_ServerIP";
            this.textBox_ServerIP.Size = new System.Drawing.Size(100, 22);
            this.textBox_ServerIP.TabIndex = 1;
            this.textBox_ServerIP.Text = "127.0.0.1";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(217, 251);
            this.panel1.TabIndex = 2;
            // 
            // Form_Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 381);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox_ServerIP);
            this.Controls.Add(this.button_TCP_Connect);
            this.Name = "Form_Client";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_TCP_Connect;
        private System.Windows.Forms.TextBox textBox_ServerIP;
        private System.Windows.Forms.Panel panel1;
    }
}

