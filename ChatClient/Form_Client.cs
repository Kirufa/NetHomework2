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

namespace ChatClient
{
    public partial class Form_Client : Form
    {
        public Form_Client()
        {
            InitializeComponent();
        }

        private void button_TCP_Connect_Click(object sender, EventArgs e)
        {
            Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IP = new IPEndPoint(IPAddress.Parse(textBox_ServerIP.Text), SocketHandle.SocketData.Port);
           // Server.Bind(IP);

            Server.Connect(IP);


        }


        Socket Server;
        IPEndPoint IP;


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
            EndPoint end = (EndPoint)IP;
            Socket UDP = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Bitmap bmp = new Bitmap(@"C:\Users\wang\Desktop\Untitled.png");
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);

            UDP.SendTo(ms.ToArray(), end);
            byte[] arr = new byte[2048];
            int n = UDP.ReceiveFrom(arr, ref end);
            ms = new MemoryStream(arr, 0, n);
            pictureBox1.Image = new Bitmap(ms);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string str = "Hello World!\n";
            SocketHandle.Dgram dgram = new SocketHandle.Dgram();
            
            
            byte [] _Arr = Encoding.Unicode.GetBytes(str);
            Array.Copy(_Arr, dgram.Data, _Arr.Length);
            dgram.Type = 1;
            dgram.DataLength = _Arr.Length;

            MemoryStream ms = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(SocketHandle.Dgram));
            xs.Serialize(ms, dgram);

            Server.Send(ms.ToArray());
           /* byte[] arr = new byte[2048];
            int n = Server.Receive(arr);
            this.textBox1.Text = Encoding.ASCII.GetString(arr, 0, n);*/
        }
    }
}
