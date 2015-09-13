using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtilities.Extensions
{
    /// <summary>
    /// Provides a convience method for avoiding null checks, since there is no
    /// null-coallescing operator for invocation
    /// </summary>
    public static class DelegateExtensions
    {
        /// <summary>
        /// Invokes the target Action if and only if it is not null
        /// </summary>
        /// <param name="act">The action to invoke</param>
        public static void SafeInvoke(this Action act)
        {
            if (act != null)
                act();
        }

        /// <summary>
        /// Invokes the target Action if and only if it is not null
        /// </summary>
        /// <param name="act">The action to invoke</param>
        /// <param name="param">The parameter required by the action</param>
        public static void SafeInvoke<T>(this Action<T> act, T param)
        {
            if (act != null)
                act(param);
        }

        /// <summary>
        /// Invokes the target Func if and only if it is not null
        /// </summary>
        /// <param name="func">The func to invoke</param>
        /// <returns>The result of the func if invoked, otherwise default(T)</returns>
        public static T SafeInvoke<T>(this Func<T> func)
        {
            if (func != null)
                return func();
            return default(T);
        }

        /// <summary>
        /// Invokes the target Func if and only if it is not null
        /// </summary>
        /// <param name="func">The func to invoke</param>
        /// <param name="param">The parameter required by the function</param>
        /// <returns>The result of the func if invoked, otherwise default(U)</returns>
        public static U SafeInvoke<T, U>(this Func<T, U> func, T param)
        {
            if (func != null)
                return func(param);
            return default(U);
        }

        /// <summary>
        /// Invokes the property changed handler if and only if the event handler is not null
        /// </summary>
        /// <param name="handler">The event handler to invoke</param>
        /// <param name="sender">The object sending the event</param>
        /// <param name="e">The event parameters</param>
        public static void SafeInvoke(this PropertyChangedEventHandler handler, object sender, PropertyChangedEventArgs e)
        {
            if (handler != null)
                handler(sender, e);
        }
    }
}
