﻿using System.IO;
using System.Threading;

namespace Meruwa
{
    public interface IStreamingProvider
    {
        string MediaType { get; }
        Timer RegisterOutput(Stream outputStream, CancellationToken cancellationToken);
    }
}
