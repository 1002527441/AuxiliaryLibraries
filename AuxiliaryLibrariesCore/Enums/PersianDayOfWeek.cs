using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryLibraries.Core.Enums
{
    /// <summary>
    /// Specifies the day of the week.
    /// </summary>
    [ComVisible(true)]
    public enum PersianDayOfWeek
    {
        /// <summary>
        /// Indicates Saturday.
        /// </summary>
        Shanbe = 0,
        /// <summary>
        /// Indicates Sunday.
        /// </summary>
        Yekshanbe = 1,
        /// <summary>
        /// Indicates Monday.
        /// </summary>
        Doshanbe = 2,
        /// <summary>
        /// Indicates Tuesday.
        /// </summary>
        Seshanbe = 3,
        /// <summary>
        /// Indicates Wednesday.
        /// </summary>
        Chaharshanbe = 4,
        /// <summary>
        /// Indicates Thursday.
        /// </summary>
        Panjshanbe = 5,
        /// <summary>
        /// Indicates Friday.
        /// </summary>
        Jomeh = 6
    }
}
