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
    /// Test for the BaseConverter class
    /// </summary>
    [TestClass]
    public class BaseConverterTests
    {
        /// <summary>
        /// Simple derived class to help test the base class
        /// </summary>
        public class TestConverter : BaseConverter<string, int>
        {
            public TestConverter() : base(0, String.Empty) { }
            public TestConverter(int defTo, string defFrom) : base(defTo, defFrom) { }

            /// <summary>
            /// Converts the provided string to an integer
            /// </summary>
            protected override int ConvertEx(string value, object parameter, CultureInfo culture)
            {
                return Int32.Parse(value);
            }

            /// <summary>
            /// Converts the provided integer to a string
            /// </summary>
            protected override string ConvertBackEx(int value, object parameter, CultureInfo culture)
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Tests the constructor and default behavior of the BaseConverter through the TestConverter
        /// </summary>
        [TestMethod]
        public void BaseConverter_ValueTests()
        {
            int defTestInt = 10;
            string defTestString = "Test string";

            TestConverter conv1 = new TestConverter();
            TestConverter conv2 = new TestConverter(defTestInt, defTestString);

            // test that the correct default value is returned when the provided value is invalid
            Assert.AreEqual(default(int), conv1.Convert(null, typeof(int), null, null),
                "Test converter Convert did not return the appropriate default value when given a null value");
            Assert.AreEqual(defTestInt, conv2.Convert(null, typeof(int), null, null),
                "Test converter Convert did not return the appropriate default value when given a null value");
            Assert.AreEqual(String.Empty, conv1.ConvertBack(null, typeof(string), null, null),
                "Test converter ConvertBack did not return the appropriate default value when given a null value");
            Assert.AreEqual(defTestString, conv2.ConvertBack(null, typeof(string), null, null),
                "Test converter ConvertBack did not return the appropriate default value when given a null value");
        }

        /// <summary>
        /// Tests the constructor and default behavior of the BaseConverter through the TestConverter
        /// </summary>
        [TestMethod]
        public void BaseConverter_TargetTypeTests()
        {
            int defTestInt = 10;
            string defTestString = "Test string";

            TestConverter conv1 = new TestConverter();
            TestConverter conv2 = new TestConverter(defTestInt, defTestString);

            // test that the correct default value is returned when the target type is incorrect
            Assert.AreEqual(default(int), conv1.Convert("1", typeof(double), null, null),
                "Test converter Convert did not return the appropriate default value when given an incorrect target type");
            Assert.AreEqual(defTestInt, conv2.Convert("1", typeof(double), null, null),
                "Test converter Convert did not return the appropriate default value when given an incorrect target type");
            Assert.AreEqual(String.Empty, conv1.ConvertBack(1, typeof(double), null, null),
                "Test converter ConvertBack did not return the appropriate default value when given an incorrect target type");
            Assert.AreEqual(defTestString, conv2.ConvertBack(1, typeof(double), null, null),
                "Test converter ConvertBack did not return the appropriate default value when given an incorrect target type");
        }

        /// <summary>
        /// Tests the constructor and default behavior of the BaseConverter through the TestConverter
        /// </summary>
        [TestMethod]
        public void BaseConverter_SourceTypeTests()
        {
            int defTestInt = 10;
            string defTestString = "Test string";

            TestConverter conv1 = new TestConverter();
            TestConverter conv2 = new TestConverter(defTestInt, defTestString);

            // test that the correct default value is returned when the source type is incorrect
            Assert.AreEqual(default(int), conv1.Convert(1, typeof(int), null, null),
                "Test converter Convert did not return the appropriate default value when given an incorrect source type");
            Assert.AreEqual(defTestInt, conv2.Convert(1, typeof(int), null, null),
                "Test converter Convert did not return the appropriate default value when given an incorrect source type");
            Assert.AreEqual(String.Empty, conv1.ConvertBack("1", typeof(string), null, null),
                "Test converter ConvertBack did not return the appropriate default value when given an incorrect source type");
            Assert.AreEqual(defTestString, conv2.ConvertBack("1", typeof(string), null, null),
                "Test converter ConvertBack did not return the appropriate default value when given an incorrect source type");
        }
    }
}
