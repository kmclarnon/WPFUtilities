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
    /// Provides a markup extension that allows chaining two value converters together
    /// </summary>
    [ValueConversion(typeof(object), typeof(object))]
    public class DualConverter : BaseConverter<object, object>
    {
        /// <summary>
        /// The first converter to be executed when converting a value
        /// </summary>
        public IValueConverter Stage1 { get; set; }

        /// <summary>
        /// The second converter to be executed when converting a value
        /// </summary>
        public IValueConverter Stage2 { get; set; }

        /// <summary>
        /// Converts the provided value by running it through both converters
        /// </summary>
        protected override object ConvertEx(object value, object parameter, System.Globalization.CultureInfo culture)
        {
            if (this.Stage1 == null || this.Stage2 == null)
                throw new InvalidOperationException("The Stage1 and Stage2 properties cannot be null");

            var interm = Stage1.Convert(value, typeof(object), parameter, culture);
            return Stage2.Convert(interm, typeof(object), parameter, culture);
        }

        /// <summary>
        /// Converts the provided value back by running it through both converters backwards
        /// </summary>
        protected override object ConvertBackEx(object value, object parameter, System.Globalization.CultureInfo culture)
        {
            if (this.Stage1 == null || this.Stage2 == null)
                throw new InvalidOperationException("The Stage1 and Stage2 properties cannot be null");

            var interm = Stage2.ConvertBack(value, typeof(object), parameter, culture);
            return Stage1.ConvertBack(interm, typeof(object), parameter, culture);
        }
    }
}
