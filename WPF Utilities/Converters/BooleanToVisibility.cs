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
    /// Provides a markup extension that converts between boolean and visibility values
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibility : BaseConverter<bool, Visibility>
    {
        /// <summary>
        /// The value of Visiblity to return if the provided value is false. Defaults to hidden
        /// </summary>
        public Visibility FalseVisibility { get; set; }

        /// <summary>
        /// The value of Visiblity to return if the provided value is false. Defaults to visible
        /// </summary>
        public Visibility TrueVisibility { get; set; }

        /// <summary>
        /// Initializes a new instance of the BoolToVisibility converter
        /// </summary>
        public BooleanToVisibility()
        {
            this.FalseVisibility = Visibility.Hidden;
            this.TrueVisibility = Visibility.Visible;
        }

        /// <summary>
        /// Converts the value between the boolean and the appropriately visibility
        /// </summary>
        protected override Visibility ConvertEx(bool value, object parameter, System.Globalization.CultureInfo culture)
        {
            return value ? this.TrueVisibility : this.FalseVisibility;
        }

        /// <summary>
        /// Converts the value between the visibility value and the appropriate bool
        /// </summary>
        protected override bool ConvertBackEx(Visibility value, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == this.TrueVisibility)
                return true;
            else if (value == this.FalseVisibility)
                return false;
            else
                throw new ArgumentException("Visibility value does not match the true or false visibility properties");
        }
    }
}
