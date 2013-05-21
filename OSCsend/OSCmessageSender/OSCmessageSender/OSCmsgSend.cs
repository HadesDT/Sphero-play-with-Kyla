using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OSCsend;

namespace OSCmessageSender
{
    public partial class OSCmsgSend : Form
    {
        private OSC oscm;

        string ip = "localhost";
        int port = 8000;
        string msg = "";


        public OSCmsgSend()
        {
            InitializeComponent();
            TBIp.Text = ip;
            TBPort.Text = port.ToString();
        }

        private void BSend_Click(object sender, EventArgs e)
        {
            ip = TBIp.Text;
            port = int.Parse(TBPort.Text);
            oscm = new OSC(ip, port);

            if (CBString.Checked)
            {
                OSCmsg omess = new OSCmsg(TBMsg.Text);
                omess.addValue(TBValue.Text);
                oscm.sendOSCmsg(omess);
            }
            else
            {
                OSCmsg omess = new OSCmsg(TBMsg.Text);
                omess.addValue(float.Parse(TBValue.Text));
                oscm.sendOSCmsg(omess);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ip = TBIp.Text;
            port = int.Parse(TBPort.Text);
            oscm = new OSC(ip, port);
            string m = "/test send int";

            OSCmsg omess = new OSCmsg(m);
            omess.addValue(245);
            oscm.sendOSCmsg(omess);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ip = TBIp.Text;
            port = int.Parse(TBPort.Text);
            oscm = new OSC(ip, port);

            OSCmsg omess = new OSCmsg("/test send int float");
            omess.addValue(357);
            omess.addValue(25.75f);
            oscm.sendOSCmsg(omess);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ip = TBIp.Text;
            port = int.Parse(TBPort.Text);
            oscm = new OSC(ip, port);

            OSCmsg omess = new OSCmsg("/test send blob");
            OSCblob oscb = new OSCblob();
            oscb.addBlob(258);
            oscb.addBlob(2.3f);
            oscb.addBlob("yahoo!");
            omess.addBlob(oscb);
            oscm.sendOSCmsg(omess);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ip = TBIp.Text;
            port = int.Parse(TBPort.Text);
            oscm = new OSC(ip, port);

            OSCbundle obu = new OSCbundle();
            OSCmsg omess = new OSCmsg("/test bundel 1");
            omess.addValue(357);
            omess.addValue(25.75f);
            obu.addOSCmsg(omess);

            OSCmsg omess2 = new OSCmsg("/test bundel 2");
            omess2.addValue(85);
            omess2.addValue(0.3f);
            obu.addOSCmsg(omess2);
            oscm.sendOSCBundle(obu);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ip = TBIp.Text;
            port = int.Parse(TBPort.Text);
            oscm = new OSC(ip, port);
            oscm.sendMsg("/test ancien methode", 42);
        }

        
    }
}
