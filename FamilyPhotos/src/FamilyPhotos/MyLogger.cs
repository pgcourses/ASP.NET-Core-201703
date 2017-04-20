using System;
using Microsoft.Extensions.Logging;

namespace FamilyPhotos.Loggers
{
    internal class MyLogger : ILogger
    {
        private string categoryName;
        private LogLevel minimumLevel = LogLevel.Debug; 

        public MyLogger(string categoryName)
        {
            this.categoryName = categoryName;
        }

        public MyLogger(string categoryName, LogLevel minimumLevel) : this(categoryName)
        {
            this.minimumLevel = minimumLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NoopDisposable.Instance;
        }

        /// <summary>
        /// A szűrés implementációja:
        /// Ha true-val térünk vissza, akkor meghívják a 
        /// Log metódust, ha false-al akkor pedig nem
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns>true: naplózunk, false: nem naplózunk ezen a LogLevel szinten</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            //Egy egyszerű szint (LogLevel) szűrés:
            //ha a beállított minimumnál nem kisebb, akkor 
            //van naplózás
            return logLevel >= minimumLevel;
        }


        /// <summary>
        /// Ezzel naplózunk
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //szűrés:
            if (!IsEnabled(logLevel))
            { //ha ki van szűrve ez a szint akkor nem naplózunk
                return;
            }

            if (formatter==null)
            { //nincs formatter, nem lesz üzenet, nincs napló
                return;
            }

            var message = formatter(state, exception);

            if (string.IsNullOrWhiteSpace(message))
            { //nincs üzenet, nincs napló
                return;
            }

            //Itt megvan a naplóüzenet, így például el tudjuk menteni.

            if (logLevel>LogLevel.Warning)
            { //itt akár adatbázisba menthetünk, vagy küldhetünk levelet.

            }
        }
    }
}