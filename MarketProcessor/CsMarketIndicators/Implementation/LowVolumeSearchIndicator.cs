using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.CsMarketIndicators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketProcessor.CsMarketIndicators.Implementation
{
    internal class LowVolumeSearchIndicator : ICsIndicator
    {
        private double _maxToAvgVolumeDifference;
        private double _maxToLowVolumeDiference;

        public double MaxToAvgVolumeDifference => _maxToAvgVolumeDifference;

        public IndicatorType Type => IndicatorType.LowVolumeSearcher;

        public LowVolumeSearchIndicator(double maxToAvgVolumeDifference = 3, double maxToLowVolumeDiference = 5)
        {
            if (maxToAvgVolumeDifference <= 0)
                throw new ArgumentOutOfRangeException("Max to Average volume difference rate cannot be equal to 0 or less than 0", nameof(maxToAvgVolumeDifference));

            if (maxToLowVolumeDiference < 1)
                throw new ArgumentOutOfRangeException("Max to Low volume difference rate cannot be less than 1", nameof(maxToLowVolumeDiference));
            
            _maxToAvgVolumeDifference = maxToAvgVolumeDifference;
            _maxToLowVolumeDiference = maxToLowVolumeDiference;
        }

        public IList<BaseIndicatorBlock> GetProcessed(IList<BaseIndicatorBlock> candleSticks)
        {
            if (candleSticks == null || candleSticks.Count == 0)
                throw new ArgumentOutOfRangeException("Check the passed list of candlesticks. It's null or empty.");

            IList<VolumeIndicatorBlock> processedCandleSticks = candleSticks.Cast<VolumeIndicatorBlock>().ToList();

            var maxCandleStickVolume = processedCandleSticks.Max(i => i.CandleStickVolume);
            var maxVolumeCandleStick = processedCandleSticks.Where(i => i.CandleStickVolume == maxCandleStickVolume)
                .FirstOrDefault();
            var maxVolumeItemIndex = processedCandleSticks.IndexOf(maxVolumeCandleStick);
            var avgCandleStickVolume = processedCandleSticks.Average(i => i.CandleStickVolume);

            if (maxCandleStickVolume / avgCandleStickVolume > _maxToAvgVolumeDifference &&
                maxVolumeItemIndex != processedCandleSticks.Count - 1) // check that max volume block is not last on collection
            {
                for (int index = maxVolumeItemIndex + 1; index < processedCandleSticks.Count; index++)
                {
                    var containsOnlyLowVolumeCandleSticks = processedCandleSticks.TakeLast(processedCandleSticks.Count - index)
                        .All(i => i.CandleStickVolume <= maxCandleStickVolume / _maxToLowVolumeDiference);
                    if (containsOnlyLowVolumeCandleSticks)
                    {
                        for (int i = index; i < processedCandleSticks.Count; i++)
                            processedCandleSticks[i].IsLowVolume = true;
                    }
                }
            }

            return processedCandleSticks.Cast<BaseIndicatorBlock>().ToList();
        }
    }
}
