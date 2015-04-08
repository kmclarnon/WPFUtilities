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
    /// Provides a converter that returns false if the object is null
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class NotNull : BaseConverter<object, bool>
    {
        /// <summary>
        /// Returns true if the provided value is not null
        /// </summary>
        protected override bool ConvertEx(object value, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        protected override object ConvertBackEx(bool value, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
