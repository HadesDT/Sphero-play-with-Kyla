using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TargettingPoint
{
    public class ValueChangedEventArgs : EventArgs
    {
        public double Angle { get; set; }

        public double Power { get; set; }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
