using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml.Serialization;
using ChatClient;

namespace Handle
{
    public class SocketHandle
    {
        [Serializable]
        public class Dgram
        {
            public byte[] Data = new byte[MAX_DATASIZE];
            public byte[] Name = new byte[STRING_SIZE];
            public int DataLength;
            public int NameLength;
            public int Type;
            //0: check alive, client <-> server
            //1: TCP Text Data, client -> server 
            //2: TCP Text Data, server -> client 
            //3: TCP UDP Image Size, follow the UDP Image, client -> server 
            //4: TCP UDP Image Size, follow the UDP Image, server -> client
            //5: UDP Image, client -> server
            //6: UDP Image, server -> client
            //7: TCP send Name client -> server
            //20: TCP System message server -> client
        }

        public const int MAX_DATASIZE = 2048;
        public const int STRING_SIZE = 512;

        public const int RECEIVE_LENGTH = 51200;



        public static void SendPicture(string name,Bitmap bmp)
        {
            List<byte[]> _Data = DivideBitmap(bmp);
            int Size = _Data.Count;
            SendToServer(StrToByte(name), StrToByte(Size.ToString()), 3);

           // List<Dgram> _Temp = new List<Dgram>();
            
            foreach(byte[] ite in _Data)
            {
                Dgram _D = new Dgram();
                Array.Copy(ite, _D.Data, ite.Length);
                _D.DataLength = ite.Length;
                //_Temp.Add(_D);
                SocketData.Server.TCPSendBmp(_D);
            }


           // SocketData.Server.UDPSend(_Temp);
            
        }

        private static byte[] DgramToByte(Dgram dgram)
        {
            MemoryStream _Ms = new MemoryStream();
            XmlSerializer _Xs = new XmlSerializer(typeof(Dgram));
            _Xs.Serialize(_Ms, dgram);
            return _Ms.ToArray();
        }

        private static Dgram ByteToDgram(byte[] _Arr, int _Len)
        {
            MemoryStream _Ms = new MemoryStream(_Arr, 0, _Len);
          XmlSerializer _Xs = new XmlSerializer(typeof(Dgram));
          //  MessageBox.Show(_Ms.ToArray().Length.ToString());

          /*  using (FileStream fs = new FileStream("123.xml", FileMode.Create))
            {
                fs.Write(_Ms.ToArray(), 0, (int)_Ms.Length);
                fs.Close();
            }
            */


          Dgram _Ret = (Dgram)_Xs.Deserialize(_Ms);
          return _Ret;
      }
        private static List<byte[]> DivideBitmap(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            byte[] _Arr = ms.ToArray();
            int index = 0;
            List<byte[]> _Ret = new List<byte[]>();
            while (true)
            {
                if (index + MAX_DATASIZE < _Arr.Length)
                {
                    byte[] _Temp = new byte[MAX_DATASIZE];
                    Array.Copy(_Arr, index, _Temp, 0, MAX_DATASIZE);
                    _Ret.Add(_Temp);

                    index += MAX_DATASIZE;
                }
                else
                {
                    int Len = _Arr.Length - index;

                    byte[] _Temp = new byte[Len];
                    Array.Copy(_Arr, index, _Temp, 0, Len);
                    _Ret.Add(_Temp);

                    break;
                }

            }

            return _Ret;
        }

        public static void InitialServer(string _IP)
        {
            SocketData.Server = new SocketData.ServerData();
            SocketData.ServerData.ServerIP = _IP;
            SocketData.Server.InitialTCPServer();
           // SocketData.Server.InitialUDPServer();
        }

        public static void SendText(string _Name,string _Str,int _Type)
        {
            byte[] _Name_Arr = StrToByte(_Name);
            byte[] _Str_Arr = StrToByte(_Str);
            SendToServer(_Name_Arr, _Str_Arr, _Type);

        }

        public static Bitmap RevertBitmap(List<Dgram> _List)
        {
            MemoryStream _Ms = new MemoryStream();

            foreach (Dgram ite in _List)
            {
                _Ms.Write(ite.Data, 0, ite.DataLength);
            }

            Bitmap _Ret = new Bitmap(_Ms);

            return _Ret;



        }
        private static byte[] StrToByte(string _Str)
        {
            return Encoding.Unicode.GetBytes(_Str);
        }

        private static string ByteToStr(byte[] _Arr, int Len)
        {
            return Encoding.Unicode.GetString(_Arr, 0, Len);
        }      

        private static void SendToServer(byte[] _Name,byte[] _Data,int _Type)
        {
            Dgram _Dg = new Dgram();

            Array.Copy(_Data, _Dg.Data, _Data.Length);
            Array.Copy(_Name, _Dg.Name, _Name.Length);
            _Dg.Type = _Type;
            _Dg.NameLength = _Name.Length;
            _Dg.DataLength = _Data.Length;

            SocketData.Server.TCPSend(_Dg);

        }

      
     
        public class SocketData
        {
            //TCP
            public static ServerData Server;
           
            //other
            public const int Port = 61361;
            public const int THREAD_WAIT_TIME = 50;   // msec
           

            public class ServerData
            {
                //Sokcet
                private Socket TCPServer;
                private Socket UDPServer;

                //IPEndPoint
                private IPEndPoint TCPEndPoint;
                private IPEndPoint UDPEndPoint;

                
                //other
                public static string ServerIP = "192.168.0.103";

                private Thread Recieve_Thread;

                private bool ReceiveRunning = true;
                                
                //*****************************
                //**********initial**********
                //*****************************
                public void InitialUDPServer()
                {
                    UDPServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    UDPEndPoint = new IPEndPoint(IPAddress.Parse(ServerIP), 0);
                }

                public void InitialTCPServer()
                {
                    TCPServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    TCPEndPoint = new IPEndPoint(IPAddress.Parse(ServerIP), SocketData.Port);
                    EndPoint _EndPoint = (EndPoint)TCPEndPoint;
                    TCPServer.Connect(_EndPoint);
                    ThreadInitial();

                }
                //******************************
                //**********\initial**********
                //******************************

                public void TCPSend(Dgram _Dgram)
                {
                    byte[] _Arr = SocketHandle.DgramToByte(_Dgram);

                    try
                    {
                        TCPServer.Send(BitConverter.GetBytes(_Arr.Length), 4, SocketFlags.None);
                        TCPServer.Send(_Arr, _Arr.Length, SocketFlags.None);
                    }
                    catch (Exception ex)
                    {
                        Form_Client.AddText("Except", ex.ToString());
                        this.ReceiveRunning = false;
                    }

                }

                public void TCPSendBmp(Dgram _Dgram)
                {
                    byte[] _Arr = SocketHandle.DgramToByte(_Dgram);

                    try
                    {
                        TCPServer.Send(BitConverter.GetBytes(_Arr.Length), 4, SocketFlags.None);
                        TCPServer.Send(_Arr, _Arr.Length, SocketFlags.None);
                    }
                    catch (Exception ex)
                    {
                        Form_Client.AddText("Except", ex.ToString());
                        this.ReceiveRunning = false;
                    }

                }
                public Bitmap TCPRecvBmp(int N)
                {
                    List<Dgram> _Temp = new List<Dgram>();

                    for (int i = 0; i != N; ++i)
                    {
                        byte[] _Arr = new byte[RECEIVE_LENGTH];
                        byte[] _Len = new byte[4];
                        TCPServer.Receive(_Len, 4, SocketFlags.None);
                        int recv = TCPServer.Receive(_Arr, BitConverter.ToInt32(_Len, 0), SocketFlags.None);
                        _Temp.Add(ByteToDgram(_Arr, recv));
                    }

                    return SocketHandle.RevertBitmap(_Temp);

                }

                public void UDPSend(List<Dgram> _Data)
                {
                    InitialUDPServer();

                    foreach (Dgram ite in _Data)
                    {
                        try
                        {
                            UDPServer.SendTo(DgramToByte(ite), UDPEndPoint);
                        }
                        catch
                        { return; }
                    }
                    UDPServer.Close();
                }

                private void ThreadInitial()
                {
                    Recieve_Thread = new Thread(new ThreadStart(Receive_Thread_Run));
                    Recieve_Thread.IsBackground = true;
                    Recieve_Thread.Start();
                }

                private void Receive_Thread_Run()
                {
                    while (ReceiveRunning)
                    {
                        //send to all online member
                        Dgram _Temp = new Dgram();
                        byte[] _Arr = new byte[RECEIVE_LENGTH];
                        int RecvSize;
                        try
                        {
                            byte[] _Len = new byte[4];
                            TCPServer.Receive(_Len, 4, SocketFlags.None);
                            RecvSize = TCPServer.Receive(_Arr, BitConverter.ToInt32(_Len, 0), SocketFlags.None);
                        }
                        catch (Exception ex)
                        {
                            Form_Client.AddText("Except", ex.ToString());
                            break;
                        }

                        _Temp = SocketHandle.ByteToDgram(_Arr, RecvSize);

                        switch (_Temp.Type)
                        {
                            case 0:
                                break;
                            case 2:
                                string _Str = ByteToStr(_Temp.Data, _Temp.DataLength);
                                string _Name = ByteToStr(_Temp.Name,_Temp.NameLength);
                                Form_Client.AddText(_Name, _Str);
                                break;
                            case 4:
                                string _Name3 = ByteToStr(_Temp.Name,_Temp.NameLength);
                                int Size = Convert.ToInt32(ByteToStr(_Temp.Data, _Temp.DataLength));
                                Form_Client.AddPic(_Name3, Server.TCPRecvBmp(Size));
                                break;
                            case 6:
                                break;
                            case 20:
                                string _Name2 = ByteToStr(_Temp.Name, _Temp.NameLength);
                                Form_Client.AddText(_Name2, " 加入聊天室");
                                break;
                            default:
                                break;
                        }
                    }
                }

            }



        }
    }




}
