using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TargettingPoint;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void targettingPointControl1_Load(object sender, EventArgs e)
        {

        }

        private void targettingPointControl1_ValueChanged(object sender, TargettingPoint.ValueChangedEventArgs args)
        {
            label1.Text = args.Power.ToString();
            label2.Text = args.Angle.ToString();
            newValue = args;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var value = targettingPointControl1.Value;
            label1.Text = value.Power.ToString();
            label2.Text = value.Angle.ToString();
            label5.Text = value.Power.ToString();
            label6.Text = value.Angle.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            targettingPointControl1.ResetValue();
        }

        private void targettingPointControl1_ValueReleased(object sender, TargettingPoint.ValueChangedEventArgs args)
        {
            label5.Text = args.Power.ToString();
            label6.Text = args.Angle.ToString();
            newValue = args;
        }

        ValueChangedEventArgs newValue = null;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (newValue != null)
            {
                label7.Text = newValue.Power.ToString();
                label8.Text = newValue.Angle.ToString();
                //envoie les infos à ton truc ici
            }
            newValue = null;
        }
    }
}
