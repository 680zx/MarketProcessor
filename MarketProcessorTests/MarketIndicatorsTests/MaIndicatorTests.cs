using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Implementation;
using MarketProcessor.MarketIndicators.Interfaces;
using NUnit.Framework;
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
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57288.48 }, EmaValue =  57594.50},
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 56404.66 } },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55800.00 } },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55680.00 } },
            new MaIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55610.00 } }
        };

        private IMarketIndicator _candleIndicator = new MaIndicator();

        [Test]
        public void Process_SimpleValues_ProcessedAndDesiredListsAreEqual()
        {
            // Arrange


            // Act

            // Assert
        }
    }
}
