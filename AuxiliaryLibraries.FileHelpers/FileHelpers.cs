using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryLibraries.FileHelpers
{
    public static class FileHelpers
    {
        public static bool IsImage(string contentType)
        {
            const string pattern = "(image/(png|jpeg|gif|pjpeg|bmp|x-bmp|exif|icon|wmf|pjpeg|x-png))";
            var regex = new System.Text.RegularExpressions.Regex(pattern);

            return regex.IsMatch(contentType);
        }
    }
}
