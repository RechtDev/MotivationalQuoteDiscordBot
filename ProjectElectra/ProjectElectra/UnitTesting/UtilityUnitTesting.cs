using NUnit.Framework;
using ProjectElectra.Common;

namespace ProjectElectra.UnitTesting
{
    [TestFixture]
    public class UtilityUnitTesting
    {
        [Test]
        [TestCase(Enums.Hours.SevenAM)]
        [TestCase(Enums.Hours.SevenPM)]
        [TestCase(Enums.Hours.OnePM)]
        [TestCase(Enums.Hours.TwelveAM)]
        [TestCase(Enums.Hours.TwoAM)]
        [TestCase(Enums.Hours.TenPM)]
        public void CanProvideDateTimeFromEnum(Enums.Hours time)
        {
            //Arrange
            DateTime ExpectedResult;
            DateTime result;
            //Act
            result = Utility.ConvertDateEnumToDateTime(time);
            ExpectedResult = DateTime.Today.AddHours((int)time);
            if (ExpectedResult < DateTime.Now)
            {
                ExpectedResult = ExpectedResult.AddDays(1);
            }

            //Assert
            Assert.AreEqual(ExpectedResult, result);
        }
    }
}