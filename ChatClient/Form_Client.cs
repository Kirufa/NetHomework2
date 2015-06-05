using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Drawing.Imaging;
using System.Xml.Serialization;
using Handle;
using Khenys.Controls;


namespace ChatClient
{
    public partial class Form_Client : Form
    {
        public Form_Client()
        {
            InitializeComponent();
        }

        public static ExRichTextBox ERT;

        private void button_TCP_Connect_Click(object sender, EventArgs e)
        {            
            try
            {
                /*
                Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IP = new IPEndPoint(IPAddress.Parse(textBox_ServerIP.Text), SocketHandle.SocketData.Port);
                // Server.Bind(IP);
                */

                SocketHandle.InitialServer(textBox_ServerIP.Text);
                SocketHandle.SendToServer(Encoding.Unicode.GetBytes(this.textBox_Name.Text), new byte[1], 7);
              
            }
            catch
            { return; }

            ERT.Text = "";

            foreach (Control i in this.Controls)
            {
                if (i is Button || i is TextBox)
                    i.Enabled = false;
            }

            button_SendPic.Enabled = true;
            button_SendText.Enabled = true;

        }
        
        private Bitmap getPic()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = null;
            
            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName != null)
            {
                Bitmap temp = new Bitmap(ofd.FileName);
                this.pictureBox1.Image = temp;
                return temp;
            }
            else
                return null;
        }

        private byte[] b2b(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Bmp);
            return ms.ToArray();
        }
        private void button2_Click(object sender, EventArgs e)
        {
           /* IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(textBox2.Text), 5555);
            Socket cli = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            Bitmap bmp = getPic();
            this.pictureBox1.Image = bmp;
            Byte[] Data = b2b(bmp);

            EndPoint endpoint = (EndPoint)ipep;
            cli.SendTo(Encoding.ASCII.GetBytes(Data.Length.ToString()), endpoint);
            cli.SendTo(Data, endpoint);
            cli.Close();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
          /*  EndPoint end = (EndPoint)IP;
            Socket UDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Bitmap bmp = new Bitmap(@"C:\Users\wang\Desktop\Untitled.png");
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);

            UDP.SendTo(ms.ToArray(), end);
            byte[] arr = new byte[2048];
            int n = UDP.ReceiveFrom(arr, ref end);
            ms = new MemoryStream(arr, 0, n);
            pictureBox1.Image = new Bitmap(ms);*/
        }

        private void button2_Click_1(object sender, EventArgs e)
        {/*
            string str = "Hello World!\n";
            SocketHandle.Dgram dgram = new SocketHandle.Dgram();
            
            
            byte [] _Arr = Encoding.Unicode.GetBytes(str);
            Array.Copy(_Arr, dgram.Data, _Arr.Length);
            dgram.Type = 1;
            dgram.DataLength = _Arr.Length;

            MemoryStream ms = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(SocketHandle.Dgram));
            xs.Serialize(ms, dgram);

            Server.Send(ms.ToArray());*/
           /* byte[] arr = new byte[2048];
            int n = Server.Receive(arr);
            this.textBox1.Text = Encoding.ASCII.GetString(arr, 0, n);*/
        }

        private void ExRichTextBoxInitial()
        {
            
        }

        private void Form_Client_Load(object sender, EventArgs e)
        {
            ERT = new ExRichTextBox();

            ERT.Size = panel_Display.Size;
            ERT.Location = new Point(0, 0);
            ERT.ReadOnly = true;
            this.panel_Display.Controls.Add(ERT);
            ERT.AppendText("Tip : 請輸入暱稱與伺服器IP位址後進行連線");

            foreach (Control i in this.Controls)
            {
                if(i is Button || i is TextBox)
                i.Enabled = false;
            }

            this.textBox_Name.Enabled = true;
            this.textBox_ServerIP.Enabled = true;

        }

        private void textBox_ServerIP_TextChanged(object sender, EventArgs e)
        {
            if (textBox_ServerIP.TextLength > 0 && textBox_Name.Text.Length > 0)
                this.button_TCP_Connect.Enabled = true;
            else
                this.button_TCP_Connect.Enabled = false;
        }

        private void textBox_Name_TextChanged(object sender, EventArgs e)
        {
            if (textBox_ServerIP.TextLength > 0 && textBox_Name.Text.Length > 0)
                this.button_TCP_Connect.Enabled = true;
            else
                this.button_TCP_Connect.Enabled = false;
        }

        public static void AddText(string _Query, string Text)
        {
            if (ERT.InvokeRequired)
            {
                ERT.Invoke((MethodInvoker)delegate
                {
                    string _Str = _Query + " : " + Text + '\n';
                    ERT.AppendText(_Str);
                });

            }
            else
            {
                string _Str = _Query + " : " + Text + '\n';
                ERT.AppendText(_Str);
            }
        }
    
    }




}
