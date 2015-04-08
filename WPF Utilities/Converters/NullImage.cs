using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using WPFUtilities.Converters.Base;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// Provides a convenience value converter to prevent a null image source 
    /// from creating errors when bound to an Image
    /// </summary>
    [ValueConversion(typeof(object), typeof(object))]
    public class NullImage : BaseConverter<object, object>
    {
        /// <summary>
        /// Initializes a new instance of the NullImage converter
        /// </summary>
        public NullImage() : base(DependencyProperty.UnsetValue) { }

        /// <summary>
        /// Handles supressing binding errors if the image source is null
        /// </summary>
        protected override object ConvertEx(object value, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        protected override object ConvertBackEx(object value, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
