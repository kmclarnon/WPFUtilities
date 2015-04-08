using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WPFUtilities.Converters.Base;
using WPFUtilities.Extensions;

namespace WPFUtilities.Converters
{
    /// <summary>
    /// Converts an enum to a string using a description attribute if possible
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class EnumToDescription : BaseConverter<Enum, string>
    {
        /// <summary>
        /// Initializes a new instance of the EnumToDescription converter
        /// </summary>
        public EnumToDescription() : base(String.Empty) { }

        /// <summary>
        /// Converts an enum to a string
        /// </summary>
        /// <returns>The description tag of the enum if possible, otherwise the resutl of ToString()</returns>
        protected override string ConvertEx(Enum value, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.GetDescription();
        }
        
        /// <summary>
        /// Not implemented
        /// </summary>
        protected override Enum ConvertBackEx(string value, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();    
        }
    }
}
