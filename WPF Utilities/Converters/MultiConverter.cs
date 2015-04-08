using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using WPFUtilities.Converters.Base;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// Provides a converter that allows for chaining converters together
    /// </summary>
    [ContentProperty("Converters")]
    [ContentWrapper(typeof(ConverterCollection))]
    public class MultiConverter : BaseConverterExtension
    {
        /// <summary>
        /// Converters to execute in order
        /// </summary>
        public ConverterCollection Converters { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MultiConverter
        /// </summary>
        public MultiConverter()
        {
            this.Converters = new ConverterCollection();
        }

        /// <summary>
        /// Runs the provided value through the list of converters provided
        /// </summary>
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            foreach (var conv in this.Converters)
                value = conv.Convert(value, targetType, parameter, culture);
            return value;
        }

        /// <summary>
        /// Rusn the provided value through the list of converters provided in reverse
        /// </summary>
        public override object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            foreach (var conv in this.Converters.Reverse())
                value = conv.ConvertBack(value, targetType, parameter, culture);
            return value;
        }
    }

    /// <summary>
    /// Utility class for the MultiConverter
    /// </summary>
    public sealed class ConverterCollection : Collection<IValueConverter> { }
}
