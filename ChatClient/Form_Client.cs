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
using System.Drawing.Drawing2D;
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

        //****************************
        //**********variable**********
        //****************************
        
        public static ExRichTextBox ERT;
        private static Dictionary<int, Bitmap> BmpDic;
        


        
        
        //*****************************
        //**********\variable**********
        //*****************************
        

        //****************************
        //**********Function**********
        //****************************

        private Bitmap getPic()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = null;

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName != null)
            {
                Bitmap temp = new Bitmap(ofd.FileName);
                //this.pictureBox1.Image = temp;
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


        public static void AddText(string _Name, string Text)
        {
            if (ERT.InvokeRequired)
            {
                ERT.Invoke((MethodInvoker)delegate
                {
                    string _Str = _Name + " : " + Text + '\n';
                    ERT.AppendText(_Str);
                });
            }
            else
            {
                string _Str = _Name + " : " + Text + '\n';
                ERT.AppendText(_Str);
            }
        }

        public static void AddPic(string _Name, Bitmap Bmp)
        {
            const int MAX_W_L = 200;

            Bitmap _Temp;

            if (Math.Max(Bmp.Width, Bmp.Height) > MAX_W_L)
            {
                int W;
                int H;

                if (Bmp.Height > Bmp.Width)
                {
                    H = MAX_W_L;
                    W = Bmp.Width / (Bmp.Height / MAX_W_L);
                }
                else
                {
                    W = MAX_W_L;
                    H = Bmp.Height / (Bmp.Width / MAX_W_L);
                }
                _Temp = new Bitmap(W, H);

                using (Graphics g = Graphics.FromImage(_Temp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(Bmp, new Rectangle(0, 0, _Temp.Width, _Temp.Height),
                        new Rectangle(0, 0, Bmp.Width, Bmp.Height), GraphicsUnit.Pixel);
                }


            }
            else
                _Temp = new Bitmap(Bmp);

            StringBuilder _Sb = new StringBuilder();
               

            if (ERT.InvokeRequired)
            {
                ERT.Invoke((MethodInvoker)delegate
                {
                    string _Str = _Name + "傳送了一張圖片\n";
                    ERT.AppendText(_Str);
                    ERT.Select(ERT.TextLength, 0);
                    ERT.InsertImage(_Temp, _Sb);
                    ERT.AppendText("\n");
                    ERT.Select(ERT.TextLength, 0);
                });
            }
            else
            {
                string _Str = _Name + "傳送了一張圖片\n";
                ERT.AppendText(_Str);
                ERT.Select(ERT.TextLength, 0);
                ERT.InsertImage(_Temp, _Sb);
                ERT.AppendText("\n");
                ERT.Select(ERT.TextLength, 0);
            }
            MessageBox.Show(_Sb.ToString());
            if (!BmpDic.ContainsKey(_Sb.ToString().GetHashCode()))
                BmpDic.Add(_Sb.ToString().GetHashCode(), _Temp);

        }

         private static byte[] StrToByte(string _Str)
        {
            return Encoding.Unicode.GetBytes(_Str);
        }

        private static string ByteToStr(byte[] _Arr,int Len)
        {           
            return Encoding.Unicode.GetString(_Arr, 0, Len);
        }

        //****************************
        //**********\Function**********
        //****************************

        //*************************
        //**********Event**********
        //*************************


        private void button_TCP_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                SocketHandle.InitialServer(textBox_ServerIP.Text);
                SocketHandle.SendText(textBox_Name.Text, "", 7);

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
            textBox_Text.Enabled = true;
           
            
        }
            
      
        private void button_SendText_Click(object sender, EventArgs e)
        {
            if (textBox_Text.Text.Length > 0)
            {

                SocketHandle.SendText(textBox_Name.Text, textBox_Text.Text, 1);
                textBox_Text.Text = "";
            }
            textBox_Text.Focus();

        }

        private void Form_Client_Load(object sender, EventArgs e)
        {
            //ERT initial
            ERT = new ExRichTextBox();

            ERT.Size = panel_Display.Size;
            ERT.Location = new Point(0, 0);
            ERT.ReadOnly = true;
            this.panel_Display.Controls.Add(ERT);
            ERT.AppendText("Tip : 請輸入暱稱與伺服器IP位址後進行連線");
            ERT.Click += new EventHandler(ERT_Click);
            ///

            //Dic initial
            BmpDic = new Dictionary<int, Bitmap>();
            ///


            foreach (Control i in this.Controls)
            {
                if (i is Button || i is TextBox)
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


        private void ERT_Click(object sender, EventArgs e)
        {
            if (ERT.SelectionType == RichTextBoxSelectionTypes.Object
                   && ERT.SelectedRtf.IndexOf(@"\pict\wmetafile") != -1)
            {
                MessageBox.Show(ERT.SelectedRtf.ToString());
                Bitmap bmp;
                if (BmpDic.TryGetValue(ERT.SelectedRtf.ToString().GetHashCode(),out bmp))
                    ShowBmp(bmp);
            }

        }

        private void ShowBmp(Bitmap bmp)
        {
            MessageBox.Show("123");
            PictureBox pic = new PictureBox();
            Form f = new Form();
            f.Size = bmp.Size;
            pic.Size = bmp.Size;
            pic.Location = new Point(0, 0);
            f.Controls.Add(pic);
            pic.Image = bmp;
            f.FormBorderStyle = FormBorderStyle.None;
            pic.Click += new EventHandler(pic_Click);
            f.Show();
        }

        private void pic_Click(object sender,EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            pic.FindForm().Close();
        }

        private void button_SendPic_Click(object sender, EventArgs e)
        {
            Bitmap bmp = getPic();
            if (bmp != null)
            {
                SocketHandle.SendPicture(textBox_Name.Text, bmp);
            }
        }
        
    }

  



}
