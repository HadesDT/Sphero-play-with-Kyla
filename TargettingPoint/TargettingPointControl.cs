using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TargettingPoint
{
    public partial class TargettingPointControl : UserControl
    {
        public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs args);
        public event ValueChangedEventHandler ValueChanged;

        public event ValueChangedEventHandler ValueReleased;

        private void TargettingPointControl_MouseUp(object sender, MouseEventArgs e)
        {
            ValueReleased(this, Value);
        }

        public ValueChangedEventArgs Value
        {
            get
            {
                Rectangle drawRect = new Rectangle(
                     this.ClientRectangle.X,
                     this.ClientRectangle.Y,
                     this.ClientRectangle.Width - 1,
                     this.ClientRectangle.Height - 1);

                int halfWidth = drawRect.Width / 2;
                int halfHeight = drawRect.Height / 2;

                int x = position.X;// (int)Math.Round(position.X * 100f / drawRect.Width - 50) * 2;
                int y = position.Y;// -(int)Math.Round(position.Y * 100f / drawRect.Height - 50) * 2;

                double h = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                double angle = Math.Atan2(y, x);
                if (h > 100)
                {
                    x = (int)Math.Round(Math.Cos(angle) * 100);
                    y = (int)Math.Round(Math.Sin(angle) * 100);
                    h = 100;
                }

                return new ValueChangedEventArgs()
                {
                    X = position.X,
                    Y = position.Y,
                    Power = h / 100 * 255,
                    Angle = angle * (180.0 / Math.PI)
                };
            }
        }

        public void ResetValue()
        {
            position.X = 0;
            position.Y = 0;

            ValueChanged(this, Value);

            this.Invalidate();
        }

        public TargettingPointControl()
        {
            InitializeComponent();
            ValueChanged += TargettingPointControl_ValueChanged;
            ValueReleased += TargettingPointControl_ValueReleased;
        }

        void TargettingPointControl_ValueReleased(object sender, ValueChangedEventArgs args)
        {
        }

        void TargettingPointControl_ValueChanged(object sender, ValueChangedEventArgs args)
        {
        }


        // point will be -100 to 100
        // 0;0 is the center
        private Point position = new Point(0, 0);
        private int weight = 4;
        private int halfWeight;

        private void TargettingPointControl_Load(object sender, EventArgs e)
        {
            halfWeight = weight / 2;
        }

        private void TargettingPointControl_Paint(object sender, PaintEventArgs e)
        {
            Rectangle drawRect = new Rectangle(
                e.ClipRectangle.X,
                e.ClipRectangle.Y,
                e.ClipRectangle.Width - 1,
                e.ClipRectangle.Height - 1);

            e.Graphics.DrawEllipse(Pens.White, drawRect);

            int halfWidth = drawRect.Width / 2;
            int halfHeight = drawRect.Height / 2;

            e.Graphics.FillEllipse(Brushes.Red,
                position.X / 100f * halfWidth + halfWidth - halfWeight,
                (-position.Y) / 100f * halfHeight + halfHeight - halfWeight,
                weight, weight);
        }


        private void TargettingPointControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }
            Rectangle drawRect = new Rectangle(
                this.ClientRectangle.X,
                this.ClientRectangle.Y,
                this.ClientRectangle.Width - 1,
                this.ClientRectangle.Height - 1);

            int halfWidth = drawRect.Width / 2;
            int halfHeight = drawRect.Height / 2;

            int x = (int)Math.Round(e.X * 100f / drawRect.Width - 50) * 2;
            int y = -(int)Math.Round(e.Y * 100f / drawRect.Height - 50) * 2;

            double h = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            double angle = Math.Atan2(y, x);
            if (h > 100)
            {
                x = (int)Math.Round(Math.Cos(angle) * 100);
                y = (int)Math.Round(Math.Sin(angle) * 100);
                h = 100;
            }

            position.X = x;
            position.Y = y;

            ValueChanged(this, new ValueChangedEventArgs()
            {
                X = position.X,
                Y = position.Y,
                Power = h / 100 * 255,
                Angle = angle * (180.0 / Math.PI)
            });

            this.Invalidate();
        }
    }
}
