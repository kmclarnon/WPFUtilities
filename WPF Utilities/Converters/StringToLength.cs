using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFUtilities.Converters.Base;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// Provides a markup extension that converts a string to the length of the string
    /// </summary>
    [ValueConversion(typeof(string), typeof(int))]
    public class StringToLength : BaseConverter<string, int>
    {
        /// <summary>
        /// Initializes a new instance of the StringToLength converter
        /// </summary>
        public StringToLength() : base(0) { }

        /// <summary>
        /// Returns the length of the provided string
        /// </summary>
        protected override int ConvertEx(string value, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Length;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        protected override string ConvertBackEx(int value, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
