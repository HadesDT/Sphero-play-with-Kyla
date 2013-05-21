using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSCsend
{
    public class OSCblob
    {
        public List<byte[]> blob;
        
        public OSCblob()
        {
            this.blob = new List<byte[]>();
        }

        public void addBlob(int val)
        {
            this.blob.Add(toByte(val));
        }

        public void addBlob(float val)
        {
            this.blob.Add(toByte(val));
        }

        public void addBlob(string val)
        {
            this.blob.Add(Encoding.ASCII.GetBytes(formateMSG(val)));
        }

        public void clearAll()
        {
            this.blob = new List<byte[]>();
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
