using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Loggers
{
    public class MyLoggerProvider : ILoggerProvider
    {
        private LogLevel minimumLevel;

        public MyLoggerProvider(LogLevel minimumLevel)
        {
            this.minimumLevel = minimumLevel;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger(categoryName, minimumLevel);
        }

        public void Dispose()
        {}
    }
}
