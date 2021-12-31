using MarketProcessor.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketProcessor.Tests.MarketIndicatorsTests
{
    [TestFixture]
    internal class MacdIndicatorTests
    {
        private IList<BaseIndicatorBlock> _testedCandleSticks = new List<BaseIndicatorBlock>
        {
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57620.00 } },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57288.48 } },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 56404.66 } },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55800.00 } },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55680.00 } },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55610.00 } }
        };

        private IList<MacdIndicatorBlock> _desiredOutputList = new List<MacdIndicatorBlock>
        {
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57620.00 }, EmaValue = 57620.00 },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57288.48 }, EmaValue = 57594.50 },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 56404.66 }, EmaValue = 57502.97 },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55800.00 }, EmaValue = 57371.97 },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55680.00 }, EmaValue = 57241.82 },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55610.00 }, EmaValue = 57116.30 }
        };
    }
}
