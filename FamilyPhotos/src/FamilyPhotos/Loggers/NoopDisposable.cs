using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotos.Loggers
{
    /// <summary>
    /// Ezt az osztályt arra tartsuk, hogy ne kelljen IDisposable osztályt gyártanunk
    /// Ő egy singleton, aminek üres a Dispose függvénye, így bátran hívható, kárt nem okoz
    /// </summary>
    public class NoopDisposable : IDisposable
    {
        /// <summary>
        /// Ez a singleton minta megvalósítása: 
        /// nem lehet közvetlenül példányosítani kívülről
        /// </summary>
        private NoopDisposable()
        { }

        public static IDisposable Instance = new NoopDisposable();

        public void Dispose()
        { }
    }
}
