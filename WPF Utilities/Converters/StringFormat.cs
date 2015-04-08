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
    /// Provides a markup extension that formats the given string value
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class StringFormat : BaseConverter<string, string>
    {
        public string Format { get; set; }

        /// <summary>
        /// Initializes a new instance of the StringFormat converter
        /// </summary>
        public StringFormat() : base(String.Empty) { }

        /// <summary>
        /// Formats the provided value string
        /// </summary>
        protected override string ConvertEx(string value, object parameter, System.Globalization.CultureInfo culture)
        {
            if (this.Format == null || this.Format == String.Empty)
                return value;
            else
                return String.Format(this.Format, value);
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        protected override string ConvertBackEx(string value, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
