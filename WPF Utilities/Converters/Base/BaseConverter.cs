using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPFUtilities.Converters.Base
{
    /// <summary>
    /// Provides a generic base converter class that supports markup extension syntax
    /// </summary>
    public abstract class BaseConverter<From, To> : BaseConverterExtension
    {
        /// <summary>
        /// Default value to return if value provided to the Convert method is invalid
        /// </summary>
        private To m_defaultTo;

        /// <summary>
        /// Default value to return if value provided to the ConvertBack method is invalid
        /// </summary>
        private From m_defaultFrom;

        /// <summary>
        /// Default constructor, Convert and ConvertBack will return default(To)
        /// and default(From) respectively
        /// </summary>
        public BaseConverter() { }

        /// <summary>
        /// Initializes a new instance of base converter that returns the specified value
        /// when the value provided to Convert is invalid
        /// </summary>
        /// <param name="defTo">The value to return when the value provided to Convert is invalid</param>
        public BaseConverter(To defTo) : this(defTo, default(From)) { }

        /// <summary>
        /// Initializes a new instance of base converter that returns the specified values
        /// when the value provided to Convert and ConvertBack is invalid
        /// </summary>
        /// <param name="defTo">The value to return when the value provided to Convert is invalid</param>
        /// <param name="defFrom">The value to return when the value provided to ConvertBack is invalid</param>
        public BaseConverter(To defTo, From defFrom)
        {
            m_defaultTo = defTo;
            m_defaultFrom = defFrom;
        }

        /// <summary>
        /// Converts the value by invoking ConvertEx
        /// </summary>
        /// <param name="value">The value produced by the binding source</param>
        /// <param name="targetType">The type of the binding target property</param>
        /// <param name="parameter">The converter parameter to use</param>
        /// <param name="culture">The culture to use in the converter</param>
        /// <returns>The converted value</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is From) || !(targetType.IsAssignableFrom(typeof(To))))
            {
                return m_defaultTo;
            }

            return this.ConvertEx((From)value, parameter, culture);
        }

        /// <summary>
        /// Converts the value back using ConvertBackEx
        /// </summary>
        /// <param name="value">The value produced by the binding source</param>
        /// <param name="targetType">The type of the binding target property</param>
        /// <param name="parameter">The converter parameter to use</param>
        /// <param name="culture">The culture to use in the converter</param>
        /// <returns>The converted value</returns>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is To) || !(targetType.IsAssignableFrom(typeof(From))))
            {
                return m_defaultFrom;
            }

            return this.ConvertBackEx((To)value, parameter, culture);
        }
    
        /// <summary>
        /// Implemented by the dervied class, this method converts the value if it is valid
        /// </summary>
        /// <param name="value">The value produced by the binding source</param>
        /// <param name="parameter">The converter parameter to use</param>
        /// <param name="culture">The culture to use in the converter</param>
        /// <returns>The converted value</returns>
        protected abstract To ConvertEx(From value, object parameter, CultureInfo culture);

        /// <summary>
        /// Implemented by the derived class, this method converts the value back if it is valid
        /// </summary>
        /// <param name="value">The value produced by the binding source</param>
        /// <param name="parameter">The converter parameter to use</param>
        /// <param name="culture">The culture to use in the converter</param>
        /// <returns>The converted value</returns>
        protected abstract From ConvertBackEx(To value, object parameter, CultureInfo culture);
    }
}
