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
    /// Provides a converter that checks if an object is the provided type
    /// </summary>
    [ValueConversion(typeof(object), typeof(bool))]
    public class IsType : BaseConverter<object, bool>
    {
        /// <summary>
        /// The expected type of the value provided to the Convert method
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the IsType converter
        /// </summary>
        public IsType() : base(false) { }

        /// <summary>
        /// Determines if the provided value is of the expected type
        /// </summary>
        protected override bool ConvertEx(object value, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.GetType() == this.Type;
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
