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
    /// Test methods for the ExpressionExtensions class
    /// </summary>
    [TestClass]
    public class ExpressionMethodsTests
    {
        private class TestClass
        {
            public string TestProperty { get; private set; }
        }

        /// <summary>
        /// Tests for the GetName expression extension
        /// </summary>
        [TestMethod]
        public void ExpressionExtensions_GetNameTest()
        {
            TestClass c = new TestClass();
            Assert.AreEqual("TestProperty", ExpressionMethods.GetName((() => c.TestProperty)),
                "GetName returned the incorrect name of the property in the selector method");
        }
    }
}
