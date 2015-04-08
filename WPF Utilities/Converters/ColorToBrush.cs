using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using WPFUtilities.Converters.Base;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// Provides a markup extension that converts a System.Media.Color to a System.Media.Brush
    /// </summary>
    [ValueConversion(typeof(Color), typeof(Brush))]
    public class ColorToBrush : BaseConverter<Color, Brush>
    {
        /// <summary>
        /// Converts a Color to the appropriate Brush
        /// </summary>
        protected override Brush ConvertEx(Color value, object parameter, System.Globalization.CultureInfo culture)
        {
            return new SolidColorBrush(value);
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        protected override Color ConvertBackEx(Brush value, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush scb = value as SolidColorBrush;
            if (scb == null)
                throw new ArgumentException("Unsupported type [" + value.GetType().Name + "]");
            return scb.Color;
        }
    }
}
