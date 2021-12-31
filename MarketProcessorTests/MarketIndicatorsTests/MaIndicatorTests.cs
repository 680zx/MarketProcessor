using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Implementation;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MarketProcessor.Tests.MarketIndicatorsTests
{
    [TestFixture]
    internal class MaIndicatorTests
    {
        private IList<BaseIndicatorBlock> _testedCandleSticks = new List<BaseIndicatorBlock>
        {
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57620.00 } },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57288.48 } },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 56404.66 } },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55800.00 } },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55680.00 } },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55610.00 } }
        };

        private IList<MaIndicatorBlock> _desiredOutputList = new List<MaIndicatorBlock>
        {
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57620.00 }, EmaValue = 57620.00 },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57288.48 }, EmaValue = 57594.50 },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 56404.66 }, EmaValue = 57502.97 },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55800.00 }, EmaValue = 57371.97 },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55680.00 }, EmaValue = 57241.82 },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55610.00 }, EmaValue = 57116.30 }
        };

        private MaIndicator _maIndicator = new MaIndicator();

        [Test]
        public void Process_SimpleValues_ProcessedAndDesiredListsAreEqual()
        {
            // Arrange
            _maIndicator.AlphaRate = 1.0 / 13.0; // Desired output list EmaValues are calclated using this rate

            // Act
            var result = _maIndicator.Process(_testedCandleSticks);

            // Assert
            Assert.IsTrue(AreListsEqual(result, _desiredOutputList));
        }

        [Test]
        public void Constructor_PeriodLessThan1_ExceptionThrown()
        {
            // Arrange
            const int PERIOD = -1;

            // Act
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var maIndicator = new MaIndicator(PERIOD);
            });
        }

        [Test]
        public void Process_CandleSticksEqualsNull_ExceptionThrown()
        {
            // Arrange
            List<BaseIndicatorBlock> candleSticks = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _maIndicator.Process(candleSticks);
            });
        }
        
        [Test]
        public void Process_CandleSticksListIsEmpty_ExceptionThrown()
        {
            // Arrange
            var candleSticks = new List<BaseIndicatorBlock>();

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _maIndicator.Process(candleSticks);
            });
        }

        private static bool AreListsEqual(IList<BaseIndicatorBlock> list1, IList<MaIndicatorBlock> list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                if (Math.Round(((MaIndicatorBlock)list1[i]).EmaValue, 2) != list2[i].EmaValue)
                    return false;
            }

            return true;
        }
    }
}
