using NUnit.Framework;
using System.Collections.Generic;
using MarketProcessor.Entities;
using MarketProcessor.MarketConditionQualifier.Implementation;

namespace MarketProcessor.Tests.MarketConditionQualifierTests
{
    public class ReccurentCandleIndicatorTests
    {
        private IList<CandleStickChart> _testedCandleSticks = new List<CandleStickChart>
        {
            new CandleStickChart { LowPrice = 57620.00, HighPrice = 58250.00 },
            new CandleStickChart { LowPrice = 57288.48, HighPrice = 57776.68 },
            new CandleStickChart { LowPrice = 56404.66, HighPrice = 57439.62 },
            new CandleStickChart { LowPrice = 55800.00, HighPrice = 57082.99 },
            new CandleStickChart { LowPrice = 55680.00, HighPrice = 55354.65 },
            new CandleStickChart { LowPrice = 55610.00, HighPrice = 56430.66 },
            new CandleStickChart { LowPrice = 56080.00, HighPrice = 56711.96 },
            new CandleStickChart { LowPrice = 56218.06, HighPrice = 56702.02 },
            new CandleStickChart { LowPrice = 55971.97, HighPrice = 56833.74 },
            new CandleStickChart { LowPrice = 56437.65, HighPrice = 56989.17 },
            new CandleStickChart { LowPrice = 56641.09, HighPrice = 57001.73 },
            new CandleStickChart { LowPrice = 56800.00, HighPrice = 57258.56 },
            new CandleStickChart { LowPrice = 56773.99, HighPrice = 57037.06 },
            new CandleStickChart { LowPrice = 56358.41, HighPrice = 56866.53 },
            new CandleStickChart { LowPrice = 56204.00, HighPrice = 56666.13 },
            new CandleStickChart { LowPrice = 55809.45, HighPrice = 56412.18 },
            new CandleStickChart { LowPrice = 55691.00, HighPrice = 56416.84 }
        };

        private IList<CandleStickChart> _desiredOutputList = new List<CandleStickChart>
        {
            new CandleStickChart { LowPrice = 57620.00, HighPrice = 58250.00 },
            new CandleStickChart { LowPrice = 57288.48, HighPrice = 57776.68 },
            new CandleStickChart { LowPrice = 56404.66, HighPrice = 57439.62 },
            new CandleStickChart { LowPrice = 55800.00, HighPrice = 57082.99 },
            new CandleStickChart { LowPrice = 55680.00, HighPrice = 55354.65 },
            new CandleStickChart { LowPrice = 55610.00, HighPrice = 56430.66, IsSupport = true },
            new CandleStickChart { LowPrice = 56080.00, HighPrice = 56711.96 },
            new CandleStickChart { LowPrice = 56218.06, HighPrice = 56702.02 },
            new CandleStickChart { LowPrice = 55971.97, HighPrice = 56833.74 },
            new CandleStickChart { LowPrice = 56437.65, HighPrice = 56989.17 },
            new CandleStickChart { LowPrice = 56641.09, HighPrice = 57001.73 },
            new CandleStickChart { LowPrice = 56800.00, HighPrice = 57258.56, IsResistance = true },
            new CandleStickChart { LowPrice = 56773.99, HighPrice = 57037.06 },
            new CandleStickChart { LowPrice = 56358.41, HighPrice = 56866.53 },
            new CandleStickChart { LowPrice = 56204.00, HighPrice = 56666.13 },
            new CandleStickChart { LowPrice = 55809.45, HighPrice = 56412.18 },
            new CandleStickChart { LowPrice = 55691.00, HighPrice = 56416.84 }
        };

        [Test]
        public void Process_SimpleValuesList_TestedAndDesiredListsAreEqual()
        {
            // Arrange
            var candleIndicator = new ReccurentCandleIndicator();

            // Act
            candleIndicator.Process(_testedCandleSticks);

            // Assert
            Assert.IsTrue(AreListsEqual(_testedCandleSticks, _desiredOutputList));
        }

        private bool AreListsEqual(IList<CandleStickChart> list1, IList<CandleStickChart> list2)
        {
            var isEqual = true;
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].IsSupport != list2[i].IsSupport || 
                    list1[i].IsResistance != list2[i].IsResistance)
                {
                    isEqual = false;
                    break;
                }
            }

            return isEqual;
        }
    }
}