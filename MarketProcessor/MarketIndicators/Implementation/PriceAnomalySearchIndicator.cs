using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class PriceAnomalySearchIndicator : IMarketIndicator
    {
        private double _lowPriceBorderCoefficient;

        public IndicatorType Type => IndicatorType.PriceAnomalySearcher;

        public PriceAnomalySearchIndicator(double lowPriceBorderCoefficient = 3)
        {
            if (lowPriceBorderCoefficient < 1)
                throw new ArgumentOutOfRangeException("Low price border rate cannot be less than 1", nameof(lowPriceBorderCoefficient));

            _lowPriceBorderCoefficient = lowPriceBorderCoefficient;
        }
        
        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            if (candleSticks == null || candleSticks.Count == 0)
                throw new ArgumentOutOfRangeException("Check the passed list of candlesticks. It's null or empty.");

            List<PriceIndicatorBlock> processedCandleSticks = candleSticks.Cast<PriceIndicatorBlock>().ToList();

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

            return processedCandleSticks.Cast<BaseIndicatorBlock>().ToList();
        }
    }
}
