using System;
using System.Collections.Generic;
using System.Text;

namespace Meruwa
{
    public class MeruwaOptions
    {
        public static MeruwaOptions Default { get { return new MeruwaOptions(); } }

        private string _path = "/meruwa";

        public string Path
        {
            get { return _path; }
            set { if (!string.IsNullOrWhiteSpace(value)) _path = value; }
        }
    }
}
