using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUtilities.Extensions;

namespace WPFUtilitiesTests.Extensions
{
    /// <summary>
    /// Tests for the EnumExtension methods
    /// </summary>
    [TestClass]
    public class EnumExtensionsTests
    {
        public enum TestEnum
        {
            [System.ComponentModel.DescriptionAttribute("Test Value 1")]
            TestValueOne,
            [System.ComponentModel.DescriptionAttribute("Test Value 2")]
            TestValueTwo,
            [System.ComponentModel.DescriptionAttribute("Test Value 3")]
            TestValueThree
        }

        [TestMethod]
        public void EnumExtensions_GetDescriptionTests()
        {
            Assert.AreEqual("Test Value 1", TestEnum.TestValueOne.GetDescription());
            Assert.AreEqual("Test Value 2", TestEnum.TestValueTwo.GetDescription());
            Assert.AreEqual("Test Value 3", TestEnum.TestValueThree.GetDescription());
        }
    }
}
