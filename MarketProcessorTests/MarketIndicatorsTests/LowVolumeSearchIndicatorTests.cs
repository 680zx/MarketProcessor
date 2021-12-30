using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Implementation;
using MarketProcessor.MarketIndicators.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

namespace MarketProcessor.Tests.MarketIndicatorsTests
{
    [TestFixture]
    internal class LowVolumeSearchIndicatorTests
    {
        private IList<BaseIndicatorBlock> _testedCandleSticks = new List<BaseIndicatorBlock>
        {
            new VolumeIndicatorBlock { CandleStickVolume = 89.579 },
            new VolumeIndicatorBlock { CandleStickVolume = 75.081 },
            new VolumeIndicatorBlock { CandleStickVolume = 63.890 },
            new VolumeIndicatorBlock { CandleStickVolume = 173.635 },
            new VolumeIndicatorBlock { CandleStickVolume = 745.266 },
            new VolumeIndicatorBlock { CandleStickVolume = 276.089 },
            new VolumeIndicatorBlock { CandleStickVolume = 228.622 },
            new VolumeIndicatorBlock { CandleStickVolume = 303.158 },
            new VolumeIndicatorBlock { CandleStickVolume = 267.636 },
            new VolumeIndicatorBlock { CandleStickVolume = 148.427 },
            new VolumeIndicatorBlock { CandleStickVolume = 174.081 },
            new VolumeIndicatorBlock { CandleStickVolume = 103.525 },
            new VolumeIndicatorBlock { CandleStickVolume = 105.516 },
            new VolumeIndicatorBlock { CandleStickVolume = 82.902 },
            new VolumeIndicatorBlock { CandleStickVolume = 81.505 },
            new VolumeIndicatorBlock { CandleStickVolume = 61.853 },
            new VolumeIndicatorBlock { CandleStickVolume = 53.722 },
            new VolumeIndicatorBlock { CandleStickVolume = 63.059 },
            new VolumeIndicatorBlock { CandleStickVolume = 64.952 },
            new VolumeIndicatorBlock { CandleStickVolume = 93.652 },
            new VolumeIndicatorBlock { CandleStickVolume = 101.925 },
            new VolumeIndicatorBlock { CandleStickVolume = 84.917 },
            new VolumeIndicatorBlock { CandleStickVolume = 67.958 },
            new VolumeIndicatorBlock { CandleStickVolume = 86.126 }
        };

        private IList<VolumeIndicatorBlock> _desiredCandleSticksOutput = new List<VolumeIndicatorBlock>
        {
            new VolumeIndicatorBlock { CandleStickVolume = 89.579 },
            new VolumeIndicatorBlock { CandleStickVolume = 75.081 },
            new VolumeIndicatorBlock { CandleStickVolume = 63.890 },
            new VolumeIndicatorBlock { CandleStickVolume = 173.635 },
            new VolumeIndicatorBlock { CandleStickVolume = 745.266 },
            new VolumeIndicatorBlock { CandleStickVolume = 276.089 },
            new VolumeIndicatorBlock { CandleStickVolume = 228.622 },
            new VolumeIndicatorBlock { CandleStickVolume = 303.158 },
            new VolumeIndicatorBlock { CandleStickVolume = 267.636 },
            new VolumeIndicatorBlock { CandleStickVolume = 148.427 },
            new VolumeIndicatorBlock { CandleStickVolume = 174.081 }, 
            new VolumeIndicatorBlock { CandleStickVolume = 103.525, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 105.516, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 82.902, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 81.505, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 61.853, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 53.722, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 63.059, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 64.952, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 93.652, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 101.925, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 84.917, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 67.958, IsLowVolume = true },
            new VolumeIndicatorBlock { CandleStickVolume = 86.126, IsLowVolume = true }
        };

        private IMarketIndicator _candleIndicator = new LowVolumeSearchIndicator();

        [Test]
        public void Process_SimpleValuesList_ProcessedAndDesiredListsAreEqual()
        {
            // Act
            var result = _candleIndicator.Process(_testedCandleSticks);

            // Assert
            Assert.IsTrue(AreListsEqual(result, _desiredCandleSticksOutput));
        }

        private static bool AreListsEqual(IList<BaseIndicatorBlock> list1, IList<VolumeIndicatorBlock> list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {              
                if (((VolumeIndicatorBlock)list1[i]).IsLowVolume != list2[i].IsLowVolume 
                    || ((VolumeIndicatorBlock)list1[i]).IsLowVolume != list2[i].IsLowVolume)               
                    return false;
            }

            return true;
        }
    }
}
