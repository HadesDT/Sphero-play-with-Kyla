using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace OSCsend
{
    /// <summary>
    /// IDisposable est utilisé pour pouvoir s'utiliser avec un USING.
    /// Ci dessous 2 exemples d'utilisation
    /// </summary>
    /// <code>
    ///        using (OSC nouveauOSC = new OSC("0", 10))
    ///        { 
    ///             // envoyer des messages et tout
    ///        } // automatiquement, nouveauOSC.Dispose() est appellé
    ///        
    /// 
    ///        OSC nouveauOSC2 = new OSC("0", 12);
    ///        try
    ///        {
    ///
     ///           // envoyer des messages et tout
    ///        }
    ///        finally
    ///        {
    ///            nouveauOSC2.Dispose();
    ///        }
    /// </code>
    public class OSC : IDisposable
    {
        public string ip;
        public int port;

        UdpClient server;
        
        public OSC(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            server = new UdpClient(ip, port);
        }



        public void sendOSCmsg(OSCmsg om)
        {
            byte[] Bval = new byte[4];
            byte[] data = new byte[4];
            string tt = ",";
            foreach (string s in om.typeTag) { tt = tt + s; }
            string cmd = formateMSG(om.msg) + formateMSG(tt);
            data = Encoding.ASCII.GetBytes(cmd);

            Bval = om.value[0];
            for (int i = 1; i < om.value.Count; i++ ) 
            {
                Bval = append(Bval, om.value[i]); 
            } 

            data = append(data, Bval);

            server.Send(data, data.Length);
        }

        public void sendOSCBundle(OSCbundle ob)
        {
            byte[] Bval = new byte[4];
            byte[] data = new byte[4];

            string cmd = formateMSG("#bundle");
            data = Encoding.ASCII.GetBytes(cmd);

            data = append(data, toByte(0));  
            data = append(data, toByte(0));

            for (int i = 0; i < ob.bundle.Count; i++)
            {
                string tt = ",";
                foreach (string s in ob.bundle[i].typeTag) { tt = tt + s; }
                string ms = formateMSG(ob.bundle[i].msg) + formateMSG(tt);
                Bval = Encoding.ASCII.GetBytes(ms);
                Bval = append(Bval , ob.bundle[i].value[0] );

                for (int j = 1; j < ob.bundle[i].value.Count; j++)
                {
                    Bval = append(Bval, ob.bundle[i].value[j]);
                }
                //data = append(data, toByte(Bval.Length / 8 + (align(Bval.Length / 8)))); 
                data = append(data, toByte(Bval.Length)); 
                data = append(data, Bval);
            }

            server.Send(data, data.Length);

        }

        public void sendOSCBundle(OSCbundle ob, int timetag)
        {
            byte[] Bval = new byte[4];
            byte[] data = new byte[4];

            string cmd = formateMSG("#bundle");
            data = Encoding.ASCII.GetBytes(cmd);

            data = append(data, toByte(0));  
            data = append(data, toByte(timetag));          

            for (int i = 0; i < ob.bundle.Count; i++)
            {
                string tt = ",";
                foreach (string s in ob.bundle[i].typeTag) { tt = tt + s; }
                string ms = formateMSG(ob.bundle[i].msg) + formateMSG(tt);
                Bval = Encoding.ASCII.GetBytes(ms);
                Bval = append(Bval, ob.bundle[i].value[0]);


                for (int j = 1; j < ob.bundle[i].value.Count; j++)
                {
                    Bval = append(Bval, ob.bundle[i].value[j]);
                }
                //data = append(data, toByte(Bval.Length/8 + (align(Bval.Length/8))));
                data = append(data, toByte(Bval.Length )); 
                data = append(data, Bval);
            }

            server.Send(data, data.Length);

        }

        private static int align(int a)
        {
            return (4 - (a % 4));
        }



        public void sendMsg(string msg, int val)
        {
            byte[] Bval = toByte(val);
            byte[] data = new byte[4];
            string cmd = formateMSG(msg) + formateMSG(",i");
            data = Encoding.ASCII.GetBytes(cmd);
            data = append(data, Bval);
            
            server.Send(data, data.Length);
        }


        public void sendMsg(string msg, float val)
        {
            byte[] Bval = toByte(val);
            byte[] data = new byte[4];

            string cmd = formateMSG(msg) + formateMSG(",f");
            data = Encoding.ASCII.GetBytes(cmd);
            data = append(data, Bval);

            server.Send(data, data.Length);
        }

        public void sendMsg(string msg, string val)
        {
            byte[] Bval = Encoding.ASCII.GetBytes(formateMSG(val));
            byte[] data = new byte[4];

            string cmd = formateMSG(msg) + formateMSG(",s");
            data = Encoding.ASCII.GetBytes(cmd);
            data = append(data, Bval);
            val.GetType();
            server.Send(data, data.Length);
        }

        public void sendMsg(string msg, int val1, int val2)
        {
            byte[] Bval1 = toByte(val1);
            byte[] Bval2 = toByte(val2);
            byte[] data = new byte[4];
            string cmd = formateMSG(msg) + formateMSG(",ii");
            data = Encoding.ASCII.GetBytes(cmd);
            data = append(data, Bval1, Bval2);

            server.Send(data, data.Length);
        }

        public void sendMsg(string msg, int val1, float val2)
        {
            byte[] Bval1 = toByte(val1);
            byte[] Bval2 = toByte(val2);
            byte[] data = new byte[4];
            string cmd = formateMSG(msg) + formateMSG(",if");
            data = Encoding.ASCII.GetBytes(cmd);
            data = append(data, Bval1, Bval2);

            server.Send(data, data.Length);
        }

        public void sendMsg(string msg, int val1, string val2)
        {
            byte[] Bval1 = toByte(val1);
            byte[] Bval2 = Encoding.ASCII.GetBytes(formateMSG(val2));
            byte[] data = new byte[4];
            string cmd = formateMSG(msg) + formateMSG(",is");
            data = Encoding.ASCII.GetBytes(cmd);
            data = append(data, Bval1, Bval2);

            server.Send(data, data.Length);
        }

        public void sendMsg(string msg, string val2, int val1)
        {
            byte[] Bval1 = toByte(val1);
            byte[] Bval2 = Encoding.ASCII.GetBytes(formateMSG(val2));
            byte[] data = new byte[4];
            string cmd = formateMSG(msg) + formateMSG(",si");
            data = Encoding.ASCII.GetBytes(cmd);
            data = append(data, Bval2, Bval1);

            server.Send(data, data.Length);
        }

        public void sendMsg(string msg, int val1, int val2, int val3)
        {
            byte[] Bval1 = toByte(val1);
            byte[] Bval2 = toByte(val2);
            byte[] Bval3 = toByte(val3);
            byte[] data = new byte[4];
            string cmd = formateMSG(msg) + formateMSG(",iii");
            data = Encoding.ASCII.GetBytes(cmd);
            data = append(data, Bval1, Bval2, Bval3);

            server.Send(data, data.Length);
        }

        private static byte[] append(byte[] abyte0, byte[] abyte1)
        {
            byte[] abyte2 = new byte[abyte0.Length + abyte1.Length];
            Array.Copy(abyte0, 0, abyte2, 0, abyte0.Length);
            Array.Copy(abyte1, 0, abyte2, abyte0.Length, abyte1.Length);
            return abyte2;
        }

        private static byte[] append(byte[] abyte0, byte[] abyte1, byte[] abyte2)
        {
            byte[] abyte3 = new byte[abyte0.Length + abyte1.Length + abyte2.Length];
            Array.Copy(abyte0, 0, abyte3, 0, abyte0.Length);
            Array.Copy(abyte1, 0, abyte3, abyte0.Length, abyte1.Length);
            Array.Copy(abyte2, 0, abyte3, abyte0.Length + abyte1.Length,
                             abyte2.Length);
            return abyte3;
        }

        private static byte[] append(byte[] abyte0, byte[] abyte1, byte[] abyte2, byte[] abyte3)
        {
            byte[] abyte4 = new byte[abyte0.Length + abyte1.Length + abyte2.Length + abyte3.Length];
            Array.Copy(abyte0, 0, abyte4, 0, abyte0.Length);
            Array.Copy(abyte1, 0, abyte4, abyte0.Length, abyte1.Length);
            Array.Copy(abyte2, 0, abyte4, abyte0.Length + abyte1.Length,
                             abyte2.Length);
            Array.Copy(abyte3, 0, abyte4, abyte0.Length + abyte1.Length + abyte2.Length,
                             abyte3.Length);
            return abyte4;
        }

        private static byte[] toByte(float real)
        {
            float vIn = real;
            byte[] vOut = BitConverter.GetBytes(vIn);
            byte[] temp = new byte[4];
            temp[0] = vOut[3];
            temp[1] = vOut[2];
            temp[2] = vOut[1];
            temp[3] = vOut[0];
            return temp;
        }

        private static byte[] toByte(int real)
        {
            int vIn = real;
            byte[] vOut = BitConverter.GetBytes(vIn);
            byte[] temp = new byte[4];
            temp[0] = vOut[3];
            temp[1] = vOut[2];
            temp[2] = vOut[1];
            temp[3] = vOut[0];
            return temp;
        }

        private static string formateMSG(string msg)
        {
            int lon = msg.Length;
            int reste = lon % 4;
            string forma = "";

            switch (reste)
            {

                case 0:
                    forma = msg + (char)0 + (char)0 + (char)0 + (char)0;
                    break;

                case 1:
                    forma = msg + (char)0 + (char)0 + (char)0;
                    break;

                case 2:
                    forma = msg + (char)0 + (char)0;
                    break;

                case 3:
                    forma = msg + (char)0;
                    break;
            }

            return forma;
        }


        public void Dispose()
        {
            Disconnect();
        }

        public void Disconnect()
        {
            server.Close();
        }
    }
}
