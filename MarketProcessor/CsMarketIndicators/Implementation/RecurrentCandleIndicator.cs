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
    internal class RecurrentCandleIndicator : ICsIndicator
    {
        public IndicatorType Type => IndicatorType.RecurrentCandle;

        public IList<BaseIndicatorBlock> GetProcessed(IList<BaseIndicatorBlock> candleSticks)
        {
            if (candleSticks == null || candleSticks.Count == 0)
                throw new ArgumentOutOfRangeException("Check the passed list of candlesticks. It's null or empty.");

            IList<RecurrentIndicatorBlock> processedCandleSticks = candleSticks.Cast<RecurrentIndicatorBlock>().ToList();

            // The index starts from 2 because we have to compare current candle value 
            // with two previous values. The comparing window includes 5 neighbour candles,
            // where current is the middle one.
            for (int i = 2; i < processedCandleSticks.Count - 2; i++)
            {
                if (processedCandleSticks[i].CandleStickChart.LowPrice < processedCandleSticks[i - 1].CandleStickChart.LowPrice &&
                    processedCandleSticks[i].CandleStickChart.LowPrice < processedCandleSticks[i + 1].CandleStickChart.LowPrice &&
                    processedCandleSticks[i - 1].CandleStickChart.LowPrice < processedCandleSticks[i - 2].CandleStickChart.LowPrice &&
                    processedCandleSticks[i + 1].CandleStickChart.LowPrice < processedCandleSticks[i + 2].CandleStickChart.LowPrice)
                {
                    processedCandleSticks[i].IsSupport = true;
                    continue;
                }

                if (processedCandleSticks[i].CandleStickChart.HighPrice > processedCandleSticks[i - 1].CandleStickChart.HighPrice &&
                    processedCandleSticks[i].CandleStickChart.HighPrice > processedCandleSticks[i + 1].CandleStickChart.HighPrice &&
                    processedCandleSticks[i - 1].CandleStickChart.HighPrice > processedCandleSticks[i - 2].CandleStickChart.HighPrice &&
                    processedCandleSticks[i + 1].CandleStickChart.HighPrice > processedCandleSticks[i + 2].CandleStickChart.HighPrice)
                {
                    processedCandleSticks[i].IsResistance = true;
                    continue;
                }
            }

            return processedCandleSticks.Cast<BaseIndicatorBlock>().ToList();
        }      
    }
}
