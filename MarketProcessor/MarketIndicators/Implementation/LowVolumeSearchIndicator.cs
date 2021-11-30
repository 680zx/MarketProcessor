using AutoMapper;
using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class LowVolumeSearchIndicator : IMarketIndicator<VolumeIndicatorBlock>
    {
        private double _maxToAvgVolumeDifference;
        private double _highVolumeBorderCoefficient;

        public double MaxToAvgVolumeDifference
        {
            get { return _maxToAvgVolumeDifference; }
            set { _maxToAvgVolumeDifference = value; }
        }

        public IndicatorType Type => IndicatorType.LowVolumeSearcher;

        public LowVolumeSearchIndicator(double maxToAvgVolumeDifference = 3, double highVolumeBorderCoefficient = 3)
        {
            _maxToAvgVolumeDifference = maxToAvgVolumeDifference;
            _highVolumeBorderCoefficient = highVolumeBorderCoefficient;
        }

        public IList<VolumeIndicatorBlock> Process(IList<VolumeIndicatorBlock> candleSticks)
        {
            List<VolumeIndicatorBlock> processedCandleSticks = (List<VolumeIndicatorBlock>)candleSticks;
            var maxCandleStickVolume = processedCandleSticks.Max(i => i.CandleStickVolume);
            var maxVolumeCandleStick = processedCandleSticks.Where(i => i.CandleStickVolume == maxCandleStickVolume)
                .FirstOrDefault();
            var maxVolumeItemIndex = processedCandleSticks.IndexOf(maxVolumeCandleStick);
            var avgCandleStickVolume = processedCandleSticks.Average(i => i.CandleStickVolume);

            if (maxCandleStickVolume / avgCandleStickVolume > _maxToAvgVolumeDifference &&
                maxVolumeItemIndex != processedCandleSticks.Count - 1) // check that max volume item is not last
            {
                #region old code
                //for (int currentItemIndex = maxVolumeItemIndex + 1; currentItemIndex < processedCandleSticks.Count; currentItemIndex++)
                //{
                //    // if candlestick volume is between 0.8 * average_volume and 1.2 * average_volume
                //    // than it's in a valid range of values
                //    if (processedCandleSticks[currentItemIndex].CandleStickVolume >= LOW_COEFFICIENT_VOLUME_BORDER * avgCandleStickVolume &&
                //        processedCandleSticks[currentItemIndex].CandleStickVolume <= HIGH_COEFFICIENT_VOLUME_BORDER * avgCandleStickVolume)
                //    {
                //        processedCandleSticks[currentItemIndex].IsLowVolume = true;
                //    }
                //}
                #endregion

                // TakeLast expression is used to discard items prior to the item with the maximum volume
                var containsOnlyLowVolumeCandleSticks = processedCandleSticks.TakeLast(processedCandleSticks.Count - maxVolumeItemIndex - 1)
                    .All(i => i.CandleStickVolume <= _highVolumeBorderCoefficient * avgCandleStickVolume);

                if (containsOnlyLowVolumeCandleSticks)
                {
                    for (int i = maxVolumeItemIndex; i < processedCandleSticks.Count; i++)
                        processedCandleSticks[i].IsLowVolume = true;                    
                }
            }

            return processedCandleSticks;
        }
    }
}
