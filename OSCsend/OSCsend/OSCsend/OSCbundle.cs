using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSCsend
{
    public class OSCbundle
    {
        public List<OSCmsg> bundle;

        public OSCbundle()
        {
            bundle = new List<OSCmsg>();
        }

        public void addOSCmsg(OSCmsg om)
        {
            this.bundle.Add(om);
        }

        public void clearAll()
        {
            this.bundle = new List<OSCmsg>();
        }

    }
}
