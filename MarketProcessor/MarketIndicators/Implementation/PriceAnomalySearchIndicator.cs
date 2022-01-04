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
        private double _priceBorderCoefficient;

        public double PriceBorderCoefficient
        {
            get => _priceBorderCoefficient;
            set
            {
                if (value < 1)
                    throw new ArgumentException("The low price border coefficient cannot be less than 1", nameof(value));
                _priceBorderCoefficient = value;
            }
        }

        public IndicatorType Type => IndicatorType.PriceAnomalySearcher;

        // TODO: Extend the indicator with low price anomaly search algorithm
        public PriceAnomalySearchIndicator(double priceBorderCoefficient = 3)
        {
            PriceBorderCoefficient = priceBorderCoefficient;
        }
        
        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            if (candleSticks == null || candleSticks.Count == 0)
                throw new ArgumentException("Check the passed list of candlesticks. It's null or empty.");

            List<PriceIndicatorBlock> processedCandleSticks = candleSticks.Cast<PriceIndicatorBlock>().ToList();

            var maxCandleStickRealBodyValue = processedCandleSticks.Max(i => i.CandleStickChart.RealBody);
            var maxCandleStickRealBodyItem = processedCandleSticks
                .Where(i => i.CandleStickChart.RealBody == maxCandleStickRealBodyValue)
                .FirstOrDefault();
            var maxCandleStickRealBodyIndex = processedCandleSticks.IndexOf(maxCandleStickRealBodyItem);
            var avgCandleStickRealBodyValue = processedCandleSticks.Average(i => i.CandleStickChart.RealBody);

            if (maxCandleStickRealBodyValue / avgCandleStickRealBodyValue > _priceBorderCoefficient &&
                maxCandleStickRealBodyIndex != processedCandleSticks.Count - 1)
            {
                processedCandleSticks[maxCandleStickRealBodyIndex].IsAnomaly = true;
            }

            return processedCandleSticks.Cast<BaseIndicatorBlock>().ToList();
        }
    }
}
