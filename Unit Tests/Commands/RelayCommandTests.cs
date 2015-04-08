using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUtilities.Commands;

namespace WPFUtilitiesTests.Commands
{
    /// <summary>
    /// Tests for the RelayCommand class
    /// </summary>
    [TestClass]
    public class RelayCommandTests
    {
        /// <summary>
        /// Tests the execute action of the relay command
        /// </summary>
        [TestMethod]
        public void RelayCommand_ExecutionTest()
        {
            int initVal = 0;
            int expectedVal = 10;
            RelayCommand command = new RelayCommand((x => initVal = (int)x));

            Assert.AreEqual(initVal, 0, "testVal was not initialized to the expected value");
            command.Execute(expectedVal);
            Assert.AreEqual(initVal, 10, "Relay command did not execute appropriately");
        }

        /// <summary>
        /// Tests the conditional excute action of the relay command
        /// </summary>
        [TestMethod]
        public void RelayCommand_ConditionalExecutionTests()
        {
            int initVal = 0;
            int invalidVal = 5;
            int validVal = 1;
            int expectedVal = 10;
            RelayCommand command = new RelayCommand((x => initVal = (int)x), (x => (int)x != invalidVal));

            Assert.AreEqual(0, initVal, "testVal was not initialized to the expected value");
            Assert.AreEqual(false, command.CanExecute(invalidVal), "CanExecute did not return false for an invalid value");
            Assert.AreEqual(true, command.CanExecute(validVal), "CanExecute did not return true for a valid value");
            command.Execute(expectedVal);
            Assert.AreEqual(10, initVal, "Relay command did not execute appropriately");
        }
    }
}
