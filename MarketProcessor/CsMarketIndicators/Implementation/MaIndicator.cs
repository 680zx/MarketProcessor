using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.CsMarketIndicators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarketProcessor.Tests")]
namespace MarketProcessor.CsMarketIndicators.Implementation
{
    // EMA_t = alpha * Price_t + (1 - alpha) * EMA_(t-1),
    // where t - value of price at a particular point
    internal class MaIndicator : ICsIndicator
    {
        private double _alphaRate;

        public double AlphaRate 
        { 
            get { return _alphaRate; } 
            set { _alphaRate = value; }
        }

        public IndicatorType Type => IndicatorType.MA;

        public MaIndicator(int period = 12)
        {
            if (period < 1)
                throw new ArgumentOutOfRangeException("Period must be greater than 1", nameof(period));

            _alphaRate = 2 / (double)(period + 1);
        }

        public IList<BaseIndicatorBlock> GetProcessed(IList<BaseIndicatorBlock> candleSticks)
        {
            if (candleSticks == null)
                throw new ArgumentNullException("Passed list of candlesticks equals null", nameof(candleSticks));

            if (candleSticks.Count == 0)
                throw new ArgumentException("Passed list of candlesticks is empty", nameof(candleSticks));

            List<MaIndicatorBlock> processedCandleSticks = candleSticks.Cast<MaIndicatorBlock>().ToList();

            // The first EMA value is usually equal to the price of the first value on the candlestick chart
            processedCandleSticks[0].EmaValue = processedCandleSticks[0].CandleStickChart.ClosePrice;

            for (int currentItemIndex = 1; currentItemIndex < processedCandleSticks.Count; currentItemIndex++)
            {
                processedCandleSticks[currentItemIndex].EmaValue = GetCurrentMaValue(
                    processedCandleSticks[currentItemIndex].CandleStickChart.ClosePrice,
                    processedCandleSticks[currentItemIndex - 1].EmaValue);
            }

            return processedCandleSticks.Cast<BaseIndicatorBlock>().ToList();
        }

        public double GetCurrentMaValue(double currentValue, double prevMaValue)
        {
            return _alphaRate * currentValue + (1 - _alphaRate) * prevMaValue;
        }
    }
}
