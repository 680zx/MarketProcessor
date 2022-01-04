using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Implementation;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57620.00 }, MacdDelta =       0 },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 57288.48 }, MacdDelta =  -21.16 },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 56404.66 }, MacdDelta =  -93.89 },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55800.00 }, MacdDelta = -188.40 },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55680.00 }, MacdDelta = -267.96 },
            new MacdIndicatorBlock { CandleStickChart = new CandleStickChart { ClosePrice = 55610.00 }, MacdDelta = -331.70 }
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
                if (Math.Round(((MacdIndicatorBlock)list1[i]).MacdDelta, 2) != list2[i].MacdDelta)
                    return false;
            }

            return true;
        }
    }
}
