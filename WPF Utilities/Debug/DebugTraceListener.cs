using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtilities.Debug
{
    /// <summary>
    /// TraceListener that invokes the Debugger when WriteLine is called 
    /// </summary>
    public class DebugTraceListener : TraceListener
    {
        /// <summary>
        /// Does nothing with the given message
        /// </summary>
        public override void Write(string message)
        {
        }

        /// <summary>
        /// Invokes the Debugger
        /// </summary>
        public override void WriteLine(string message)
        {
            Debugger.Break();
        }
    }
}
