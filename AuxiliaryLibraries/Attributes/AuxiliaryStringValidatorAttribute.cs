using AuxiliaryLibraries.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;


namespace AuxiliaryLibraries.Attributes
{
    /// <summary>
    /// This attribute checks the length of String. It checks also strings to be free of some illegal characters and words to prevent SQL injection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    [ComVisible(true)]
    public class AuxiliaryStringValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// The maximum of string length
        /// </summary>
        public int MaximumLength { get; }
        /// <summary>
        /// The minimum of string length
        /// </summary>
        public int MinimumLength { get; set; }
        /// <summary>
        /// The property name
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(ResponseMessages), ErrorMessageResourceName = "FieldIsRequired")]
        public string PropertyName { get; set; }
        private string invalidCharacters { get; set; }
        /// <summary>
        /// Invalid Characters
        /// </summary>
        public string InvalidCharacters
        {
            get
            {
                if (string.IsNullOrEmpty(invalidCharacters))
                    invalidCharacters = _invalidCharacters;
                return invalidCharacters;
            }
            set => invalidCharacters = value;
        }
        private const string _invalidCharacters = "[<>={}?;:\\+!#$%~^&]";

        private readonly List<string> InvalidWords = new List<string>() { "SELECT", "INSERT", "UPDATE", "DELETE", "WHERE", "ALTER", "DROP", "CREATE", "TRUNCATE", "AUTHORIZATION", "FROM", "BACKUP", "RESTRICT", "BEGIN", "FUNCTION", "PROC", "RETURN", "BREAK", "TRANSACTION", "ROLLBACK", "COMMIT", "DENY", "TABLE", "SAVE", "ADD", "WHILE", "EXCEPT", "ESCAPE", "WITH", "GROUP", "EXIT", "EXEC", "DUMP" };
        private string IllegalWords = string.Empty;
        private const string DefaultErrorMessage = "'{0}' must be at maximum {1} characters long.";
        private bool ExactMatch(string input, string match)
        {
            var isMatch = System.Text.RegularExpressions.Regex.IsMatch(input, string.Format(@"\b{0}\b", System.Text.RegularExpressions.Regex.Escape(match)));
            if (isMatch) IllegalWords = match;
            return isMatch;
        }
        private bool ExactMatch(string input, List<string> match)
        {
            return match.Any(x => ExactMatch(input, x));
        }

        /// <summary>
        /// In constroctor of this class, must pass 'maximumLength', cause it is required to be checked.
        /// </summary>
        /// <param name="maximumLength"></param>
        public AuxiliaryStringValidatorAttribute(int maximumLength) : base(DefaultErrorMessage)
        {
            MaximumLength = maximumLength;
        }

        /// <summary>
        /// Return the error message
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            var illegalWords = !string.IsNullOrEmpty(IllegalWords) ? $" ({IllegalWords}) " : string.Empty;
            IllegalWords = string.Empty;
            return string.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name, MaximumLength, MinimumLength, illegalWords);
        }

        /// <summary>
        /// Check if string passes validations or not?
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
                return MinimumLength == 0;

            var valueAsString = value.ToString();
            return (valueAsString != null) &&
                    !new System.Text.RegularExpressions.Regex(InvalidCharacters).IsMatch(valueAsString) &&
                    !ExactMatch(valueAsString.ToUpper(), InvalidWords) &&
                    (valueAsString.Length <= MaximumLength) &&
                    (MinimumLength > 0 ? valueAsString.Length >= MinimumLength : true);
        }

        /// <summary>
        ///  Replace Arabic characters with Persian ones, and replace all Persian and Arabic numbers to English numbers
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var prop = validationContext.ObjectType.GetProperty(PropertyName ?? validationContext.MemberName);
            if (prop != null)
            {
                var oldVal = prop.GetValue(validationContext.ObjectInstance) as string;
                var newVal = oldVal.ToPersianLetters().ToEnglishNumbers();
                prop.SetValue(validationContext.ObjectInstance, newVal);
                return base.IsValid(value, validationContext);
            }
            return base.IsValid(value, validationContext);
        }
    }
}
