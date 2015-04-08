using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtilities.Extensions
{
    /// <summary>
    /// Provides extension methods for Expressions
    /// </summary>
    public static class ExpressionMethods
    {
        /// <summary>
        /// Returns the name of the Property specified in the selector expression
        /// </summary>
        /// <param name="selector">Expression that selects the appropriate property</param>
        /// <returns>The name of the property selected by the selector expression</returns>
        public static string GetName<T>(Expression<Func<T>> selector)
        {
            if (selector == null)
                return String.Empty;

            MemberExpression b = selector.Body as MemberExpression;
            if (b == null)
                throw new ArgumentException("The body must be a member expression");
            return b.Member.Name;
        }
    }
}
