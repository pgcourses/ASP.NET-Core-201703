using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyPhotosWithIdentity.Helpers
{
    /// <summary>
    /// Extension függvény osztálya, fontos feltételeket ellenőrző függvényeket tartalmaz
    /// </summary>
    public static class GuardHelper
    {
        /// <summary>
        /// Elvégzi a szükséges null vizsgálatot, és ha rendben van minden visszatér az eredeti értékkel.
        /// Ha a kapott paraméter null, akkor kivételt dob.
        /// </summary>
        /// <typeparam name="T">Az ellenőrizni kívánt paraméter típusa. Formális paraméter, 
        /// mivel extension függvényt írunk, ezért nem kell a híváskor megadni
        /// </typeparam>
        /// <param name="o">a formális paraméter, amire az extension-t hívjuk</param>
        /// <returns></returns>
        public static T ThrowIfNull<T>(this T o)
        {
            if (null==o) { throw new ArgumentNullException(typeof(T).Name); }
            return o;
        }
    }
}
