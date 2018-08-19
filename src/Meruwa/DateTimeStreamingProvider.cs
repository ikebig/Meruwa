using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Meruwa
{
    public class DateTimeStreamingProvider : StreamingProviderBase
    {
        private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

        public override object GetData()
        {
            if (_stopwatch.ElapsedMilliseconds < 1000)
                return null;

            _stopwatch.Restart();

            return DateTime.Now;
        }
    }
}
