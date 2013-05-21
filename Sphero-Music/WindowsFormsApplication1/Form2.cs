using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TargettingPoint;
using OSCsend;
using System.Threading;

/*
Son Kyma :
Kyma:/Kyma Sound Library:/An Overview.kym/Granular classic no spectrum display

Paramètre son KymaX

D_Jitter /vcs 3145736
Density /vcs 3145730
P_Jitter /vcs 3145731
Frequency /vcs 3145733
F_Jitter /vcs 3145734
GrainDur /vcs 3145735
Amplitude /vcs 3145732
Volume /vcs 3145729

*/
namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {        
        Sphero.Sphero _s;
        private void Form2_Load(System.Object sender, System.EventArgs e)
        {
            _s = new Sphero.Sphero("");
        }

        private void Button1_Click(System.Object sender, System.EventArgs e)
        {
            ColorDialog1.ShowDialog();
            _s.Color = ColorDialog1.Color;
        }

        private void TrackBar3_Scroll(System.Object sender, System.EventArgs e)
        {
            _s.Color = Color.FromArgb(TrackBar1.Value, TrackBar2.Value, TrackBar3.Value);
        }

        private void CheckBox1_CheckedChanged(System.Object sender, System.EventArgs e)
        {
            if ((_s != null))
                _s.Stabilization = CheckBox1.Checked;
            if (CheckBox1.Checked == false)
            {
                _s.BackLedOutput = Convert.ToByte(255);
                _s.Color = Color.Black;
                checkBox2.Checked = true;
            }
            else
            {
                _s.BackLedOutput = Convert.ToByte(0);
                _s.Color = Color.Blue;
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if ((_s != null) && checkBox2.Checked == true)
                _s.BackLedOutput = Convert.ToByte(255);
            else
                _s.BackLedOutput = Convert.ToByte(0);      
        }

        ValueChangedEventArgs newValue = null;

        private void Timer1_Tick(System.Object sender, System.EventArgs e)
        {
            Label9.Text = Convert.ToString(_s.Pitch);
            Label10.Text = Convert.ToString(_s.Yaw);
            Label11.Text = Convert.ToString(_s.Roll);

            Label12.Text = Convert.ToString(_s.AccelX);
            Label13.Text = Convert.ToString(_s.AccelY);
            Label14.Text = Convert.ToString(_s.AccelZ);

            Label18.Text = Convert.ToString(_s.GyroX);
            Label19.Text = Convert.ToString(_s.GyroY);
            Label20.Text = Convert.ToString(_s.GyroZ);
            
            Label24.Text = Convert.ToString(_s.PowerState);
            Label25.Text = Convert.ToString(_s.BatteryVoltage);
            Label26.Text = Convert.ToString(_s.NumCharges);
            Label30.Text = Convert.ToString(_s.TimeSinceCharge);

            if (newValue != null)
            {
                _s.Speed = Convert.ToByte(newValue.Power);
                _s.Heading = Convert.ToInt32(newValue.Angle / 360 * 255);
            }
            newValue = null;
        }

        Thread oscMessThread = null;
        Thread chocthread = null;

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (oscm != null && oscMessThread == null)
            {
                oscMessThread = new Thread(oscMessThread_SendMessages);
                oscMessThread.Start();
            }
        }
        private void timer3_Tick_1(object sender, EventArgs e)
        {
            float choc = Math.Abs(Math.Abs(_s.AccelX) + Math.Abs(_s.AccelY) + Math.Abs(_s.AccelZ));
            if (choc >= 10000f && oscm != null && chocthread == null)
            {
                chocthread = new Thread(chocthread_send);
                chocthread.Start();
            }
        }

        private void chocthread_send()
        {
            float choc_f = Math.Abs(Math.Abs(_s.AccelX / 30000f) + Math.Abs(_s.AccelY / 30000f) + Math.Abs(_s.AccelZ / 30000f));
                OSCmsg omess7 = new OSCmsg("/vcs");
                omess7.addValue(3145729);
                omess7.addValue(choc_f);
                oscm.sendOSCmsg(omess7);
                chocthread = null;
        }
        
        private void oscMessThread_SendMessages()
        {
            OSCmsg omess0 = new OSCmsg("/vcs");
            omess0.addValue(3145733);
            omess0.addValue(0.015f);
            oscm.sendOSCmsg(omess0);

            Thread.Sleep(10);

            OSCmsg omess = new OSCmsg("/vcs");
            omess.addValue(3145731);
            float frequencef = 0.5f - Math.Abs(Math.Abs(_s.AccelX / 100000f) + Math.Abs(_s.AccelY / 100000f) + Math.Abs(_s.AccelZ / 100000f));
            omess.addValue(frequencef);
            oscm.sendOSCmsg(omess);

            Thread.Sleep(10);

            OSCmsg omess2 = new OSCmsg("/vcs");
            omess2.addValue(3145730);
            float densitef = 0.3f - Math.Abs(Math.Abs(_s.AccelX / 100000f) + Math.Abs(_s.AccelY / 100000f) + Math.Abs(_s.AccelZ / 100000f)) /*Math.Abs(_s.GyroZ / 10000f)*/;
            omess2.addValue(densitef);
            oscm.sendOSCmsg(omess2);

            OSCmvuybuibbioooob

            Thread.Sleep(10);

            OSCmsg omess3 = new OSCmsg("/vcs");
            omess3.addValue(3145732);
            float Amplitudef = 0.5f - Math.Abs(Math.Abs(_s.AccelX / 5000f) + Math.Abs(_s.AccelY / 5000f) + Math.Abs(_s.AccelZ / 5000f))/*0.2f + Math.Abs(_s.GyroX / 10000f)*/;
            omess3.addValue(Amplitudef);
            oscm.sendOSCmsg(omess3);
            
            Thread.Sleep(10);

            OSCmsg omess4 = new OSCmsg("/vcs");
            omess4.addValue(3145734);
            float amplitude = Math.Abs(_s.GyroY / 10000f);
            omess4.addValue(amplitude);
            oscm.sendOSCmsg(omess4);

            Thread.Sleep(10);

            OSCmsg omess5 = new OSCmsg("/vcs");
            omess5.addValue(3145729);
            float fjiterf = Math.Abs(_s.GyroX / 10000f);
            omess5.addValue(fjiterf);
            oscm.sendOSCmsg(omess5);

            Thread.Sleep(10);

            OSCmsg omess6 = new OSCmsg("/vcs");
            omess6.addValue(3145735);
            float ffjiterf = 0.5f + Math.Abs(_s.GyroZ / 50000f);
            omess6.addValue(ffjiterf);
            oscm.sendOSCmsg(omess6);
            oscMessThread = null;
        }
        private void Button2_Click(System.Object sender, System.EventArgs e)
        {
            TextBox1.Enabled = false;
            Button2.Enabled = false;
            Button3.Enabled = true;
            _s = new Sphero.Sphero(TextBox1.Text);
            _s.connect();
        }

        private void Button3_Click(System.Object sender, System.EventArgs e)
        {
            TextBox1.Enabled = true;
            Button2.Enabled = true;
            Button3.Enabled = false;
            _s.disconnect();
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void targettingPointControl1_ValueChanged(object sender, TargettingPoint.ValueChangedEventArgs args)
        {
            //label33.Text = args.Power.ToString();
            //label34.Text = args.Angle.ToString();
            newValue = args;
        }

        private void targettingPointControl1_MouseUp(object sender, MouseEventArgs e)
        {
            targettingPointControl1.ResetValue();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ip = textBox2.Text;
            int port = Convert.ToInt32(textBox3.Text);
            oscm = new OSC(ip, port);
        }

        private OSC oscm = null;

        private void button5_Click(object sender, EventArgs e)
        {
            oscMessThread = null;
            timer2.Enabled = false;
            OSCmsg omess7 = new OSCmsg("/vcs");
            omess7.addValue(3145729);
            omess7.addValue(0.001f);
            oscm.sendOSCmsg(omess7);
            oscm.Disconnect();
            
        }
    }

}
