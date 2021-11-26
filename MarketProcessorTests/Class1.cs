using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Implementation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProcessor.Tests
{
    [TestFixture]
    internal class Class1
    {
        private IList<BaseIndicatorBlock> _testedCandleSticks = new List<BaseIndicatorBlock>
        {
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 57620.00, HighPrice = 58250.00 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 57288.48, HighPrice = 57776.68 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56404.66, HighPrice = 57439.62 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55800.00, HighPrice = 57082.99 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55680.00, HighPrice = 55354.65 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55610.00, HighPrice = 56430.66 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56080.00, HighPrice = 56711.96 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56218.06, HighPrice = 56702.02 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55971.97, HighPrice = 56833.74 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56437.65, HighPrice = 56989.17 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56641.09, HighPrice = 57001.73 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56800.00, HighPrice = 57258.56 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56773.99, HighPrice = 57037.06 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56358.41, HighPrice = 56866.53 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 56204.00, HighPrice = 56666.13 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55809.45, HighPrice = 56412.18 } },
            new BaseIndicatorBlock { CandleStickChart = new CandleStickChart { LowPrice = 55691.00, HighPrice = 56416.84 } }
        };

        [Test]
        public void Test()
        {
            var analyzer = new MarketAnalyzer();

            analyzer.SelectMarketIndicator(new RecurrentCandleIndicator());
            analyzer.LoadCandleStickCharts(_testedCandleSticks);
            analyzer.Process();

            var processedDataWithIndicatorName = analyzer.ProcessedCandleSticks;
        }
    }
}
