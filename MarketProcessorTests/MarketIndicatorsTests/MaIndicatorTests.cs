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

        private static bool AreListsEqual(IList<BaseIndicatorBlock> list1, IList<MaIndicatorBlock> list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                System.Console.WriteLine($"1] {((MaIndicatorBlock)list1[i]).EmaValue}\t2] {list2[i].EmaValue}");
                if (Math.Round(((MaIndicatorBlock)list1[i]).EmaValue, 2) != list2[i].EmaValue)
                    return false;
            }

            return true;
        }
    }
}
