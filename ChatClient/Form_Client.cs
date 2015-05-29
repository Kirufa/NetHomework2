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
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(textBox2.Text), 5555);
            Socket cli = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            Bitmap bmp = getPic();
            this.pictureBox1.Image = bmp;
            Byte[] Data = b2b(bmp);

            EndPoint endpoint = (EndPoint)ipep;
            cli.SendTo(Encoding.ASCII.GetBytes(Data.Length.ToString()), endpoint);
            cli.SendTo(Data, endpoint);
            cli.Close();
        }
    }
}
