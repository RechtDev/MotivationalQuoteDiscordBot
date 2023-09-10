using NUnit.Framework;
using ProjectOedipus.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOedipus.UnitTesting
{
    [TestFixture]
    public class UtilityUnitTesting
    {
        [Test]
        [TestCase(1, 3600000d)]
        [TestCase(3, 10800000d)]
        [TestCase(4, 14400000d)]
        public void CanConvertMsToSeconds(int hour, double expectedResult)
        {
            // act
            var result = Utility.ConvertHoursToMs(hour);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
