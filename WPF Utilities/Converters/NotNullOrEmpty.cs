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
    /// Provides a converter that returns false if the string is null or empty
    /// </summary>
    [ValueConversion(typeof(string), typeof(bool))]
    public class NotNullOrEmpty : BaseConverter<string, bool>
    {
        /// <summary>
        /// Returns true if the provided value is not null
        /// </summary>
        protected override bool ConvertEx(string value, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null && value.Length != 0;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        protected override string ConvertBackEx(bool value, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
