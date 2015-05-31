﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Handle
{
    public class SocketHandle
    {
        public class Dgram
        {
            public const int MAX_DATASIZE = 2048;
            public int Size;
            public byte[] Data = new byte[MAX_DATASIZE];

        }
        public void SendPicture(Socket des, Bitmap bmp)
        {
            List<byte[]> _Data = DivideBitmap(bmp);


        }

        private List<byte[]> DivideBitmap(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            byte[] _Arr = ms.ToArray();
            int index = 0;
            List<byte[]> _Ret = new List<byte[]>();
            while (true)
            {
                if (index + Dgram.MAX_DATASIZE < _Arr.Length)
                {
                    byte[] _Temp = new byte[Dgram.MAX_DATASIZE];
                    Array.Copy(_Arr, index, _Temp, 0, Dgram.MAX_DATASIZE);
                    _Ret.Add(_Temp);

                    index += Dgram.MAX_DATASIZE;
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
        public class SocketData
        {
            //TCP
            public static IPEndPoint TCPEndPoint;
            public static IPEndPoint UDPEndPoint;
            public static Socket TCPServer;
            public static List<ClientData> TCP_UDP_Client;
            private static Timer Accept_Timer;

            //UDP
            public static Socket UDPServer;

            //other
            public const int Port = 61361;
            public const int MAX_LISTEN_SIZE = 10;
            public const string ServerIP = "192.168.0.100";
            public const int TIMER_INTERVAL = 50;   // ms
            private static int ListenCount = MAX_LISTEN_SIZE;

            public class ClientData
            {
                public Socket Client;
                public int ID;
                public static int IDCount = 0;

                private Timer Receive_Timer;

                public ClientData(Socket socket)
                {
                    Client = socket;
                    ID = IDCount++;
                    Receive_Timer = new Timer();
                    Receive_Timer.Interval = SocketData.TIMER_INTERVAL;
                    Receive_Timer.Tick += new EventHandler(Receive_Timer_Tick);
                    Receive_Timer.Start();
                }

                ~ClientData()
                {
                    Client.Close();
                    Receive_Timer.Stop();
                    Receive_Timer.Dispose();
                }

                private void Receive_Timer_Tick(object sender, EventArgs e)
                {
                    //send to all online member

                }


            }

            public static void InitialUDPServer()
            {
                UDPServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                UDPEndPoint = new IPEndPoint(IPAddress.Parse(ServerIP), Port);
                UDPServer.Bind(UDPEndPoint);
            }
            private static void TCPReceiveInitial()
            {


            }

            public static void InitialTCPServer()
            {
                TCPServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                TCPEndPoint = new IPEndPoint(IPAddress.Parse(ServerIP), SocketData.Port);
                EndPoint _EndPoint = (EndPoint)TCPEndPoint;
                TCPServer.Bind(_EndPoint);
                TCPServer.Listen(MAX_LISTEN_SIZE);
                TCPAcceptInitial();
            }

            private static void TCPAcceptInitial()
            {
                Accept_Timer = new Timer();
                Accept_Timer.Interval = TIMER_INTERVAL;
                Accept_Timer.Tick += new EventHandler(Accept_Timer_Tick);
                Accept_Timer.Start();
            }



            private static void Accept_Timer_Tick(object sender, EventArgs e)
            {
                if (ListenCount > 0)
                {
                    /* ClientData _Client = new ClientData();
                     _Client.Client = TCPServer.Accept();
                     _Client.ID = ClientData.IDCount++;
                   
                     TCP_UDP_Client.Add(_Client);
                     ListenCount--;*/
                }
            }
        }
    }




}
