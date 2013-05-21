using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSCsend
{
    public class OSCmsg
    {
        public string msg;
        public List<string> typeTag;
        public List<byte[]> value;

        public OSCmsg(string msg)
        {
            this.msg = msg;
            this.typeTag = new List<string>();
            this.value = new List<byte[]>();
        }

        public void addValue( int val )
        {
            this.typeTag.Add("i");
            this.value.Add(toByte(val));
        }

        public void addValue(float val)
        {
            this.typeTag.Add("f");
            this.value.Add(toByte(val));
        }

        public void addValue(string val)
        {
            this.typeTag.Add("s");
            this.value.Add(Encoding.ASCII.GetBytes(formateMSG(val)));
        }

        public void addBlob(OSCblob oBlob)
        {
            this.typeTag.Add("b");
            byte[] temp = new byte[4];
            
            foreach (byte[] b in oBlob.blob)
            {
                temp = append(temp, b);
            }
            byte[] temp2 = new byte[4];
            temp2 = toByte(temp.Length + (align(temp.Length) % 4));
            temp = append(temp2, temp);
            this.value.Add(temp);
        }

        public void clearAll()
        {
            this.msg = "";
            this.typeTag = new List<string>();
            this.value = new List<byte[]>();
        }

        private static int align(int a)
        {
            return (4 - (a % 4));
        }

        private static byte[] toByte(float flo)
        {
            byte[] vOut = BitConverter.GetBytes(flo);
            byte[] temp = new byte[4];
            temp[0] = vOut[3];
            temp[1] = vOut[2];
            temp[2] = vOut[1];
            temp[3] = vOut[0];
            return temp;
        }

        private static byte[] toByte(int real)
        {
            byte[] vOut = BitConverter.GetBytes(real);
            byte[] temp = new byte[4];
            temp[0] = vOut[3];
            temp[1] = vOut[2];
            temp[2] = vOut[1];
            temp[3] = vOut[0];
            return temp;
        }

        private static byte[] append(byte[] abyte0, byte[] abyte1)
        {
            byte[] abyte2 = new byte[abyte0.Length + abyte1.Length];
            Array.Copy(abyte0, 0, abyte2, 0, abyte0.Length);
            Array.Copy(abyte1, 0, abyte2, abyte0.Length, abyte1.Length);
            return abyte2;
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

    }
}
