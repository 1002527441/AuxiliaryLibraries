using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryLibraries
{
    /// <summary>
    /// Everything about files such as validations
    /// </summary>
    public static class FileHelpers
    {
        /// <summary>
        /// Undrasting this file is image or not
        /// </summary>
        /// <param name="contentType">Your file</param>
        /// <returns>If the file be png, jpeg, gif, bmp or icon, it will return true.</returns>
        public static bool IsImage(this string contentType)
        {
            const string pattern = "(image/(png|jpeg|gif|pjpeg|bmp|x-bmp|exif|icon|wmf|pjpeg|x-png))";
            var regex = new System.Text.RegularExpressions.Regex(pattern);

            return regex.IsMatch(contentType);
        }
    }
}
