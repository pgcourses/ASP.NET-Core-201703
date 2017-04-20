using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Loggers
{
    /// <summary>
    /// Ez az extension szolgál arra, hogy a Startup-ban 
    /// fel tudjuk venni a naplózónkat
    /// </summary>
    public static class MyLoggerExtensions
    {
        public static ILoggerFactory AddMyLogger(this ILoggerFactory factory, LogLevel minimumLevel = LogLevel.Debug)
        {
            factory.AddProvider(new MyLoggerProvider(minimumLevel));
            return factory;
        }
    }
}
