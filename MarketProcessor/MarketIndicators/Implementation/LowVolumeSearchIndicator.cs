using AutoMapper;
using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class LowVolumeSearchIndicator : IMarketIndicator
    {
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BaseIndicatorBlock, VolumeIndicatorBlock>()));

        // The window period is indicated in number of canlde sticks
        // per timeframe. Example: if we use a 1-hour timeframe and
        // assume that we want to find the local maximum for the last 
        // 24 hours than the candlestick window period equals 24
        private int _candleStickWindowPeriod;
        private double _maxToAvgVolumeDifference;

        public int CandleStickWindowPeriod
        {
            get { return _candleStickWindowPeriod; }
            set { _candleStickWindowPeriod = value;}
        }

        public double MaxToAvgVolumeDifference
        {
            get { return _maxToAvgVolumeDifference; }
            set { _maxToAvgVolumeDifference = value; }
        }

        public IndicatorType Type => IndicatorType.LowVolumeSearcher;

        public LowVolumeSearchIndicator(int candleStickWindowPeriod = 24, double maxToAvgVolumeDifference = 3)
        {
            _candleStickWindowPeriod = candleStickWindowPeriod;
            _maxToAvgVolumeDifference = maxToAvgVolumeDifference;
        }

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            List<VolumeIndicatorBlock> processedCandleSticks = (List<VolumeIndicatorBlock>)_mapper
                .Map<IList<BaseIndicatorBlock>, IList<VolumeIndicatorBlock>>(candleSticks);

            var startBlockIndex = processedCandleSticks.Count - _candleStickWindowPeriod;

            var MaxCandleStickVolume = processedCandleSticks.Max(i => i.CandleStickVolume);
            var MaxVolumeItemIndex = processedCandleSticks.Where(i => i.CandleStickVolume == MaxCandleStickVolume)
                .FirstOrDefault().CandleStickChartId;
            var AvgCandleStickVolume = processedCandleSticks.Average(i => i.CandleStickVolume);

            if (MaxCandleStickVolume / AvgCandleStickVolume > _maxToAvgVolumeDifference)
            {

            }

            return processedCandleSticks.ConvertAll(i => (BaseIndicatorBlock)i);
        }
    }
}
