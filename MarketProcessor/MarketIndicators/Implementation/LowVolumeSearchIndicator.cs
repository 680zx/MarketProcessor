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
        private double _maxToLowVolumeDiference;

        public double MaxToAvgVolumeDifference
        {
            get { return _maxToAvgVolumeDifference; }
            set { _maxToAvgVolumeDifference = value; }
        }

        public IndicatorType Type => IndicatorType.LowVolumeSearcher;

        public LowVolumeSearchIndicator(double maxToAvgVolumeDifference = 3, double maxToLowVolumdeDiference = 5)
        {
            _maxToAvgVolumeDifference = maxToAvgVolumeDifference;
            _maxToLowVolumeDiference = maxToLowVolumdeDiference;
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

            return processedCandleSticks;
        }
    }
}
