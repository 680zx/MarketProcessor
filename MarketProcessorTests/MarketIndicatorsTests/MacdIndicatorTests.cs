using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Implementation;
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

        private const int MA_INDICATOR_SHORT_PERIOD = 12;
        private const int MA_INDICATOR_LONG_PERIOD = 26;
        private const int MA_INDICATOR_SMOOTH = 9;

        private MacdIndicator _macdIndicator = new MacdIndicator(new MaIndicator(MA_INDICATOR_SHORT_PERIOD),
                new MaIndicator(MA_INDICATOR_LONG_PERIOD), new MaIndicator(MA_INDICATOR_SMOOTH));

        [Test]
        public void Process_SimpleValues_ProcessedAndDesiredListsAreEqual()
        {
            // Act
            var result = _macdIndicator.Process(_testedCandleSticks);

            // Assert
            Assert.IsTrue(AreListsEqual(result, _desiredOutputList));
        }

        private static bool AreListsEqual(IList<BaseIndicatorBlock> list1, IList<MacdIndicatorBlock> list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                Console.WriteLine($"result: {((MacdIndicatorBlock)list1[i]).MacdValue}\texpected: {list2[i].MacdValue}");
                if (Math.Round(((MacdIndicatorBlock)list1[i]).MacdValue, 2) != list2[i].MacdValue ||
                    Math.Round(((MacdIndicatorBlock)list1[i]).MacdDelta, 2) != list2[i].MacdDelta)
                    return false;
            }

            return true;
        }
    }
}
