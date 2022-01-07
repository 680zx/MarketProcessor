using MarketProcessor.Enums;
using NUnit.Framework;

namespace MarketProcessor.Tests
{
    [TestFixture]
    internal class RegisterTests
    {
        [TestCase(IndicatorType.RecurrentCandle)]
        [TestCase(IndicatorType.MA)]
        [TestCase(IndicatorType.MACD)]
        [TestCase(IndicatorType.LowVolumeSearcher)]
        [TestCase(IndicatorType.PriceAnomalySearcher)]
        public void MarketIndicators_IndicatorTypeRecurrentCandle_ReturnedAndDesiredIndicatorsAreSame(IndicatorType indicator)
        {
            // Arrange
            var regIndicator = Register.MarketIndicators[indicator];

            // Act
            var regIndicatorType = regIndicator.Type;

            // Assert
            Assert.AreEqual(indicator, regIndicatorType);
        }
    }
}
