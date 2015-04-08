using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WPFUtilities.Converters;

namespace WPFUtilitiesTests.Converters
{
    /// <summary>
    /// Tests for the converters derived from BaseConverter class
    /// </summary>
    [TestClass]
    public class ConverterTests
    {
        /// <summary>
        /// Tests for the BooleanNot converter
        /// </summary>
        [TestMethod]
        public void Converter_BooleanNotTests()
        {
            BooleanNot conv = new BooleanNot();
            // Convert tests
            Assert.AreEqual(true, conv.Convert(false, typeof(bool), null, null), 
                "BooleanNot Convert returned false instead of true");
            Assert.AreEqual(false, conv.Convert(true, typeof(bool), null, null),
                "BooleanNot Convert returned true instead of false");

            // ConvertBack tests
            Assert.AreEqual(true, conv.ConvertBack(false, typeof(bool), null, null),
                "BooleanNot Convert returned false instead of true");
            Assert.AreEqual(false, conv.ConvertBack(true, typeof(bool), null, null),
                "BooleanNot Convert returned true instead of false");
        }

        /// <summary>
        /// Tests for the BooleanToVisibility converter
        /// </summary>
        [TestMethod]
        public void Converter_BooleanToVisibilityTests()
        {
            BooleanToVisibility conv = new BooleanToVisibility();
            BooleanToVisibility convEx = new BooleanToVisibility();
            convEx.TrueVisibility = Visibility.Visible;
            convEx.FalseVisibility = Visibility.Collapsed;

            // check Convert defaults
            Assert.AreEqual(Visibility.Visible,
                conv.Convert(true, typeof(Visibility), null, CultureInfo.CurrentCulture),
                "BooleanToVisibility Convert did not return the expected visibility to true");
            Assert.AreEqual(Visibility.Hidden,
                conv.Convert(false, typeof(Visibility), null, CultureInfo.CurrentCulture),
                "BooleanToVisibility Convert did not return the expected visibility to false");

            // check ConvertBack defaults
            Assert.AreEqual(true, 
                conv.ConvertBack(Visibility.Visible, typeof(bool), 
                    null, CultureInfo.CurrentCulture),
                "BooleanToVisibility ConvertBack did not return the expected boolean to Visibile");
            Assert.AreEqual(false,
                conv.ConvertBack(Visibility.Hidden, typeof(bool),
                    null, CultureInfo.CurrentCulture),
                "BooleanToVisibility ConvertBack did not return the expected bolean to Hidden");
            try
            {
                // this should throw an ArgumentException
                var val = conv.ConvertBack(Visibility.Collapsed, typeof(bool), null, CultureInfo.CurrentCulture);
                Assert.Fail("BooleanToVisibility ConvertBack should have thrown an argument exception");
            }
            catch (ArgumentException) { }
        
            // check Convert values
            Assert.AreEqual(Visibility.Visible,
                convEx.Convert(true, typeof(Visibility), null, CultureInfo.CurrentCulture),
                "BooleanToVisibility Convert did not return the expected visibility to true");
            Assert.AreEqual(Visibility.Collapsed,
                convEx.Convert(false, typeof(Visibility), null, CultureInfo.CurrentCulture),
                "BooleanToVisibility Convert did not return the expected visibility to false");

            // check ConvertBack values
            Assert.AreEqual(true,
                convEx.ConvertBack(Visibility.Visible, typeof(bool), null, CultureInfo.CurrentCulture),
                "BooleanToVisibility ConvertBack did not return the expected boolean to Visible");
            Assert.AreEqual(false,
                convEx.ConvertBack(Visibility.Collapsed, typeof(bool), null, CultureInfo.CurrentCulture),
                "BooleanToVisibility ConvertBack did not return the expected boolean to Collapsed");
            try
            {
                // this should throw an ArgumentException
                var val = convEx.ConvertBack(Visibility.Hidden, typeof(bool), null, CultureInfo.CurrentCulture);
                Assert.Fail("BooleanToVisibility ConvertBack should have thrown an argument exception");
            }
            catch (ArgumentException) { }
        }

        /// <summary>
        /// Tests for the ColorToBrush converter
        /// </summary>
        [TestMethod]
        public void Converter_ColorToBrushTests()
        {
            ColorToBrush conv = new ColorToBrush();
            var convBrush = conv.Convert(Colors.White, typeof(Brush), null, CultureInfo.CurrentCulture);
            var brush = new SolidColorBrush(Colors.White);
            Assert.AreEqual(brush.Color, ((SolidColorBrush)convBrush).Color,
                "ColorToBrush Convert did not return the correct brush");
            var clr = conv.ConvertBack(new SolidColorBrush(Colors.White), typeof(Color), null, CultureInfo.CurrentCulture);
            Assert.AreEqual(Colors.White, clr,
                "ColorToBrush ConvertBack did not return the correct color");
        }

        /// <summary>
        /// Tests for the IsType converter
        /// </summary>
        [TestMethod]
        public void Converter_IsTypeTests()
        {
            IsType conv = new IsType();
            conv.Type = typeof(double);
            double dval = 1.0f;
            float fval = 1.0f;

            Assert.AreEqual(true, conv.Convert(dval, typeof(bool), null, CultureInfo.CurrentCulture),
                "IsType Convert did not return the correct value");
            Assert.AreEqual(false, conv.Convert(fval, typeof(bool), null, CultureInfo.CurrentCulture),
                "IsType Convert did not return the correct value");
            try
            {
                conv.ConvertBack(true, typeof(object), null, CultureInfo.CurrentCulture);
                Assert.Fail("IsType ConvertBack did not throw a NotImplemented exception");
            }
            catch (NotImplementedException) { }
        }

        /// <summary>
        /// Tests for the NotNull converter
        /// </summary>
        [TestMethod]
        public void Converter_NotNullTests()
        {
            NotNull conv = new NotNull();

            Assert.AreEqual(true, conv.Convert(new object(), typeof(bool), null, CultureInfo.CurrentCulture),
                "NotNull Convert did not return the correct value");
            Assert.AreEqual(false, conv.Convert(null, typeof(bool), null, CultureInfo.CurrentCulture),
                "NotNull Convert did not return the correct value");
            try
            {
                conv.ConvertBack(true, typeof(object), null, CultureInfo.CurrentCulture);
                Assert.Fail("NotNull ConvertBack did not throw a NotImplemented exception");
            }
            catch (NotImplementedException) { }
        }

        /// <summary>
        /// Tests for the NotNullOrEmpty
        /// </summary>
        [TestMethod]
        public void Converter_NotNullOrEmptyTests()
        {
            NotNullOrEmpty conv = new NotNullOrEmpty();
            string emptyVal = String.Empty;
            string nullVal = null;
            string val = "test";

            Assert.AreEqual(true, conv.Convert(val, typeof(bool), null, CultureInfo.CurrentCulture),
                "");
            Assert.AreEqual(false, conv.Convert(emptyVal, typeof(bool), null, CultureInfo.CurrentCulture),
                "");
            Assert.AreEqual(false, conv.Convert(nullVal, typeof(bool), null, CultureInfo.CurrentCulture),
                "");
            try
            {
                conv.ConvertBack(true, typeof(string), null, CultureInfo.CurrentCulture);
                Assert.Fail("NotNullOrEmpty ConvertBack did not throw a NotImplemented exception");
            }
            catch (NotImplementedException) { }
        }

        /// <summary>
        /// Tests for the NullImage converter
        /// </summary>
        [TestMethod]
        public void Converter_NullImageTests()
        {
            NullImage conv = new NullImage();
            Object testObj = new Object();

            Assert.AreEqual(testObj, conv.Convert(testObj, typeof(object), null, CultureInfo.CurrentCulture),
                "NullImage Convert did not return the appropriate value");
            Assert.AreEqual(DependencyProperty.UnsetValue, conv.Convert(null, typeof(object), null, CultureInfo.CurrentCulture),
                "NullImage Convert did not return the DependecyProperty.UnsetValue");
            try
            {
                conv.ConvertBack(testObj, typeof(object), null, CultureInfo.CurrentCulture);
                Assert.Fail("NullImage ConvertBack did not throw a NotImplemented exception");
            }
            catch (NotImplementedException) { }
        }

        /// <summary>
        /// Tests for the StringFormat converter
        /// </summary>
        [TestMethod]
        public void Converter_StringFormatTests()
        {
            StringFormat conv = new StringFormat();
            string val = "1";
            string fmt = "test {0} value";

            conv.Format = fmt;


            Assert.AreEqual(String.Format(fmt, val), conv.Convert(val, typeof(string), null, CultureInfo.CurrentCulture),
                "StringFormat Convert did not return the appropriate formated string");
            Assert.AreEqual(String.Format(fmt, String.Empty), conv.Convert(String.Empty, typeof(string), null, CultureInfo.CurrentCulture),
                "StringFormat Convert did not return the appropriate formated string");
            try
            {
                conv.ConvertBack(val, typeof(string), null, CultureInfo.CurrentCulture);
                Assert.Fail("StringFormat ConvertBack did not throw a NotImplemented exception");
            }
            catch (NotImplementedException) { }
        }

        /// <summary>
        /// Tests for the StringToLength converter
        /// </summary>
        [TestMethod]
        public void Converter_StringToLengthTests()
        {
            StringToLength conv = new StringToLength();
            string val = "test value";
            int len = val.Length;

            Assert.AreEqual(len, conv.Convert(val, typeof(int), null, CultureInfo.CurrentCulture),
                "StringToLength Convert did not return the appropriate length");
            Assert.AreEqual(0, conv.Convert(String.Empty, typeof(int), null, CultureInfo.CurrentCulture),
                "StringToLength Convert did not return the appropriate length");
            Assert.AreEqual(0, conv.Convert(null, typeof(int), null, CultureInfo.CurrentCulture),
                "StringToLength Convert did not return the appropriate length");
            try
            {
                conv.ConvertBack(10, typeof(string), null, CultureInfo.CurrentCulture);
                Assert.Fail("StringToLength ConvertBack did not throw a NotImplemented exception");
            }
            catch (NotImplementedException) { }
        }
    }
}
