using MarketProcessor.Entities;
using MarketProcessor.CsMarketIndicators.Implementation;
using MarketProcessor.CsMarketIndicators.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using MarketProcessor.MarketAnalyzers;

namespace MarketProcessor.Tests
{
    [TestFixture]
    internal class CsMarketAnalyzerTests
    {
        private IList<BaseIndicatorBlock> _testedCandleSticks = new List<BaseIndicatorBlock>
        {
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 57620.00, HighPrice = 58250.00 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 57288.48, HighPrice = 57776.68 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56404.66, HighPrice = 57439.62 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55800.00, HighPrice = 57082.99 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55680.00, HighPrice = 55354.65 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55610.00, HighPrice = 56430.66 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56080.00, HighPrice = 56711.96 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56218.06, HighPrice = 56702.02 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55971.97, HighPrice = 56833.74 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56437.65, HighPrice = 56989.17 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56641.09, HighPrice = 57001.73 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56800.00, HighPrice = 57258.56 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56773.99, HighPrice = 57037.06 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56358.41, HighPrice = 56866.53 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56204.00, HighPrice = 56666.13 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55809.45, HighPrice = 56412.18 } },
            new RecurrentIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55691.00, HighPrice = 56416.84 } }
        };

        [Test]
        public void Process_SimpleValues_ExpectedTypeIndicatorReturned()
        {
            // Arrange
            var analyzer = new CsMarketAnalyzer();
            ICsIndicator marketIndicator = new RecurrentCandleIndicator();
            var marketIndicatorType = marketIndicator.Type;

            // Act
            analyzer.SelectMarketIndicator(marketIndicator);
            analyzer.LoadCandleStickCharts(_testedCandleSticks);
            analyzer.Process();

            var processedData = analyzer.ProcessedCandleSticks;

            // Assert
            Assert.AreEqual(marketIndicatorType, processedData.Keys.FirstOrDefault());
        }
    }
}
