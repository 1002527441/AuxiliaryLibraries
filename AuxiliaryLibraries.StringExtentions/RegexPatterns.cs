using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuxiliaryLibraries.StringExtentions
{
    public static class RegexPatterns
    {
        public const string Username = @"^[A-Za-z0-9_-]{3,16}$";
        public const string Email = @"^([\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+\.)*[\w\!\#$\%\&\'\*\+\-\/\=\?\^\`{\|\}\~]+@((((([a-zA-Z0-9]{1}[a-zA-Z0-9\-]{0,62}[a-zA-Z0-9]{1})|[a-zA-Z])\.)+[a-zA-Z]{2,6})|(\d{1,3}\.){3}\d{1,3}(\:\d{1,5})?)$";
        public const string MobileNumber = @"^(989|9|09|\+989|\u0660\u0669|\u0669\u0668\u0669|\u0669|\+\u0669\u0668\u0669)[0-9\u0660-\u0669]{9}$";
        public const string PhoneNumber = @"^((0|\u0660)[0-9\u0660-\u0669]+(\-|\s)?)?[0-9\u0660-\u0669]{8}$";
        public const string NationalID = @"^[0-9]{10}$";
    }
}
