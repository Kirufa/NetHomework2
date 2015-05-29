using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChatClient
{
    public class SocketHandle
    {
        public class Dgram
        {
            public const int MAX_DATASIZE = 2048;
            public int Size;
            public byte[] Data = new byte[MAX_DATASIZE];

        }

        

    }
}
