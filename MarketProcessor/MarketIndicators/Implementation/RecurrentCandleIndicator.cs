using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Interfaces;

[assembly: InternalsVisibleTo("MarketProcessor.Tests")]
namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class RecurrentCandleIndicator : IMarketIndicator
    {
        public void Process(IList<BaseIndicatorBlock> candleSticks)
        {
            // The indexer starts from 2 because we have to compare current candle value 
            // with two previous values. The comparing window includes 5 neighbour candles,
            // where current is the middle one.
            for (int i = 2; i < candleSticks.Count - 2; i++)
            {
                if (candleSticks[i].CandleStickChart.LowPrice < candleSticks[i - 1].CandleStickChart.LowPrice &&
                    candleSticks[i].CandleStickChart.LowPrice < candleSticks[i + 1].CandleStickChart.LowPrice &&
                    candleSticks[i - 1].CandleStickChart.LowPrice < candleSticks[i - 2].CandleStickChart.LowPrice &&
                    candleSticks[i + 1].CandleStickChart.LowPrice < candleSticks[i + 2].CandleStickChart.LowPrice)
                {
                    (candleSticks[i] as RecurrentIndicatorBlock).IsSupport = true;
                    continue;
                }

                if (candleSticks[i].CandleStickChart.HighPrice > candleSticks[i - 1].CandleStickChart.HighPrice &&
                    candleSticks[i].CandleStickChart.HighPrice > candleSticks[i + 1].CandleStickChart.HighPrice &&
                    candleSticks[i - 1].CandleStickChart.HighPrice > candleSticks[i - 2].CandleStickChart.HighPrice &&
                    candleSticks[i + 1].CandleStickChart.HighPrice > candleSticks[i + 2].CandleStickChart.HighPrice)
                {
                    (candleSticks[i] as RecurrentIndicatorBlock).IsResistance = true;
                    continue;
                }
            }
        }
    }
}
