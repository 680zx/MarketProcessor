using AutoMapper;
using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class PriceAnomalySearchIndicator : IMarketIndicator<PriceIndicatorBlock>
    {
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BaseIndicatorBlock, VolumeIndicatorBlock>()));
        private double _lowPriceBorderCoefficient;

        public IndicatorType Type => IndicatorType.PriceAnomalySearcher;

        public PriceAnomalySearchIndicator(double lowPriceBorderCoefficient = 3)
        {
            _lowPriceBorderCoefficient = lowPriceBorderCoefficient;
        }
        
        public IList<PriceIndicatorBlock> Process(IList<PriceIndicatorBlock> candleSticks)
        {
            List<PriceIndicatorBlock> processedCandleSticks = (List<PriceIndicatorBlock>)candleSticks;

            var maxCandleStickRealBodyValue = processedCandleSticks.Max(i => i.CandleStickChart.RealBody);
            var maxCandleStickRealBodyItem = processedCandleSticks
                .Where(i => i.CandleStickChart.RealBody == maxCandleStickRealBodyValue)
                .FirstOrDefault();
            var maxCandleStickRealBodyIndex = processedCandleSticks.IndexOf(maxCandleStickRealBodyItem);
            var avgCandleStickRealBodyValue = processedCandleSticks.Average(i => i.CandleStickChart.RealBody);

            if (maxCandleStickRealBodyValue / avgCandleStickRealBodyValue > _lowPriceBorderCoefficient &&
                maxCandleStickRealBodyIndex != processedCandleSticks.Count - 1)
            {
                processedCandleSticks[maxCandleStickRealBodyIndex].IsAnomaly = true;
            }

            return processedCandleSticks;
        }
    }
}
