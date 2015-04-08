using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUtilities.Converters.Base;

namespace WPFUtilitiesTests.Converters.Base
{
    /// <summary>
    /// Tests for the BaseConverterExtension class
    /// </summary>
    [TestClass]
    public class BaseConverterExtensionTests
    {
        public class TestConverter : BaseConverterExtension
        {
            /// <summary>
            /// Converts the value by invoking ConvertEx
            /// </summary>
            public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return value;
            }

            /// <summary>
            /// Converts the value back using ConvertBackEx
            /// </summary>
            public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return value;
            }
        }

        /// <summary>
        /// Tests the ProvideValue method required by the MarkupExtension inheritance
        /// </summary>
        [TestMethod]
        public void BaseConverterExtension_ServiceProviderTests()
        {
            TestConverter conv = new TestConverter();
            Assert.AreEqual(conv, conv.ProvideValue(null));
        }
    }
}
