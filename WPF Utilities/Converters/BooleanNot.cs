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
    /// Provides a markup extension that negates a provided boolean value
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class BooleanNot : BaseConverter<bool, bool>
    {
        /// <summary>
        /// Negates the provided boolean
        /// </summary>
        protected override bool ConvertEx(bool value, object parameter, System.Globalization.CultureInfo culture)
        {
            return !value;
        }

        /// <summary>
        /// Negates the provided boolean
        /// </summary>
        protected override bool ConvertBackEx(bool value, object parameter, System.Globalization.CultureInfo culture)
        {
            return !value;
        }
    }
}
