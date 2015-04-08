using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtilities.Extensions
{
    /// <summary>
    /// Provides extension methods for the Enumeration class
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the Description attribute attached to the given enumeration value
        /// </summary>
        /// <returns>The description string</returns>
        public static string GetDescription(this Enum e)
        {
            if (e == null)
                return String.Empty;

            FieldInfo fInfo = e.GetType().GetField(e.ToString());
            object[] attribs = fInfo.GetCustomAttributes(false);

            if (attribs.Length == 0)
                return e.ToString();
            else
            {
                var res = attribs.Where(o => o is DescriptionAttribute);
                if (!res.Any())
                    return e.ToString();
                else
                    return ((DescriptionAttribute)res.First()).Description;
            }
        }
    }
}
