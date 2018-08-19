using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace Meruwa
{
    public abstract class StreamingProviderBase : IStreamingProvider
    {
        #region Const

        private const int DATA_POLLING_INTERVAL_SECONDS = 5000;

        #endregion

        #region Fields

        private readonly Lazy<Timer> _timer;
        private readonly ConcurrentDictionary<StreamWriter, StreamWriter> _outputs;

        #endregion

        #region Ctor

        public StreamingProviderBase()
        {
            _timer = new Lazy<Timer>(() => new Timer(TimerCallback, null, 0, DATA_POLLING_INTERVAL_SECONDS));
            _outputs = new ConcurrentDictionary<StreamWriter, StreamWriter>();
        }

        #endregion

        #region Utils

        private void TimerCallback(object state)
        {
            var data = GetData();
            if (data == null)
            {
                //no data available at this moment
                return;
            }

            foreach (var kvp in _outputs.ToArray())
            {
                StreamWriter responseStreamWriter = kvp.Value;

                try
                {
                    responseStreamWriter.Write(data);
                    responseStreamWriter.Flush();
                }
                catch { }
            }
        }

        private void CancellationRequested(object state)
        {
            StreamWriter responseStreamWriter = state as StreamWriter;

            if (responseStreamWriter != null)
            {
                _outputs.TryRemove(responseStreamWriter, out responseStreamWriter);
            }
        }

        #endregion

        #region IStreamingProvider Impl

        public Timer Timer { get { return _timer.Value; } }

        public virtual string MediaType { get { return "text/plain"; } }

        public void RegisterOutput(Stream outputStream, CancellationToken cancellationToken)
        {
            StreamWriter responseStreamWriter = new StreamWriter(outputStream);

            // Register a callback which gets triggered when a client disconnects
            cancellationToken.Register(CancellationRequested, responseStreamWriter);

            _outputs.TryAdd(responseStreamWriter, responseStreamWriter);
        }

        #endregion

        public abstract object GetData();
    }
}
