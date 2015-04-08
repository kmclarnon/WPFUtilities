using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUtilities.Converters.Base;

namespace WPFUtilities.Debug
{
    /// <summary>
    /// Provides a markup extension that invokes the debugger on covert and convertback
    /// </summary>
    public class DebugConverter : BaseConverter<object, object>
    {
        /// <summary>
        /// Invokes the debugger when the Convert method is called
        /// </summary>
        protected override object ConvertEx(object value, object parameter, System.Globalization.CultureInfo culture)
        {
            #if(DEBUG)
                Debugger.Break();
            #endif
            return value;
        }

        /// <summary>
        /// Invokes the debugger when the ConvertBack method is called
        /// </summary>
        protected override object ConvertBackEx(object value, object parameter, System.Globalization.CultureInfo culture)
        {
            #if(DEBUG)
                Debugger.Break();
            #endif
            return value;
        }
    }
}
