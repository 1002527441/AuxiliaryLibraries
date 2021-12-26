using AuxiliaryLibraries.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Serialization;

namespace AuxiliaryLibraries.Core.AuxilaryServices
{
    /// <summary>
    /// Auxiliary Encryption
    /// </summary>
    public static class AuxiliaryEnumExtension
    {
        /// <summary>
        /// Convert Enum to int
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static int ToInt(this Enum enumValue)
        {
            return (int)(object)enumValue;
        }

        /// <summary>
        /// Get Enum Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (T val in values)
                {
                    if (val.ToInt32(CultureInfo.InvariantCulture) == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val) ?? string.Empty);

                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null; // could also return string.Empty
        }

        /// <summary>
        /// Get List of Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumToListModel> GetEnumList<T>() where T : IConvertible
        {
            var list = new List<EnumToListModel>();
            foreach (var t in (T[])Enum.GetValues(typeof(T)))
            {
                var item = new EnumToListModel()
                {
                    Key = t.ToString(),
                    Value = Convert.ToInt32(t),
                    Description = t.GetDescription()
                };
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// Get Enum value by its Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetValueFromDescription<T>(this string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            //return default(T);
        }

        public class EnumToListModel
        {
            public string Key { get; set; }
            public int Value { get; set; }
            public string Description { get; set; }
        }
    }
}