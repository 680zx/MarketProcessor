using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Implementation;
using MarketProcessor.MarketIndicators.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarketProcessor.Tests.MarketIndicatorsTests
{
    [TestFixture]
    internal class PriceAnomalySearchIndicatorTests
    {
        private IList<BaseIndicatorBlock> _testedCandleSticks = new List<BaseIndicatorBlock>
        {
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 55975.33, ClosePrice = 53838.65 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 53838.65, ClosePrice = 53601.05 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 53601.05, ClosePrice = 52000.00 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 52000.01, ClosePrice = 47242.74 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 47242.75, ClosePrice = 46489.67 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 46489.67, ClosePrice = 48203.73 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 48203.74, ClosePrice = 49250.00 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 49250.00, ClosePrice = 49152.47 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 49152.47, ClosePrice = 49170.79 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 49170.79, ClosePrice = 49289.18 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 49289.18, ClosePrice = 49242.58 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 49242.58, ClosePrice = 47951.57 } }
        };

        private IList<PriceIndicatorBlock> _desiredOutputList = new List<PriceIndicatorBlock>
        {
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 55975.33, ClosePrice = 53838.65 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 53838.65, ClosePrice = 53601.05 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 53601.05, ClosePrice = 52000.00 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 52000.01, ClosePrice = 47242.74 }, IsAnomaly = true },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 47242.75, ClosePrice = 46489.67 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 46489.67, ClosePrice = 48203.73 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 48203.74, ClosePrice = 49250.00 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 49250.00, ClosePrice = 49152.47 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 49152.47, ClosePrice = 49170.79 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 49170.79, ClosePrice = 49289.18 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 49289.18, ClosePrice = 49242.58 } },
            new PriceIndicatorBlock { CandleStickChart = new CandleStickChart { OpenPrice = 49242.58, ClosePrice = 47951.57 } }
        };

        private const double PRICE_BORDER_RATE = 2.5;
        private IMarketIndicator _marketIndicator = new PriceAnomalySearchIndicator(PRICE_BORDER_RATE);

        [Test]
        public void Process_SimpleValues_ProcessedAndDesiredListsAreEqual()
        {
            // Arrange
            // Act
            var result = _marketIndicator.Process(_testedCandleSticks);

            // Assert
            Assert.IsTrue(AreListsEqual(result, _desiredOutputList));
        }

        private static bool AreListsEqual(IList<BaseIndicatorBlock> list1, IList<PriceIndicatorBlock> list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                if (((PriceIndicatorBlock)list1[i]).IsAnomaly != list2[i].IsAnomaly)
                    return false;
            }

            return true;
        }
    }
}
