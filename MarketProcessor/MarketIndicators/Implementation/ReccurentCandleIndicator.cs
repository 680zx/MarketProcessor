using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Interfaces;

[assembly: InternalsVisibleTo("MarketProcessor.Tests")]
namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class ReccurentCandleIndicator : IMarketIndicator
    {
        public void Process(IList<CandleStickChart> candleSticks)
        {
            // The indexer starts from 2 because we have to compare current candle value 
            // with two previous values. The comparing window includes 5 neighbour candles,
            // where current is the middle one.
            for (int i = 2; i < candleSticks.Count - 2; i++)
            {
                if (candleSticks[i].LowPrice < candleSticks[i - 1].LowPrice &&
                    candleSticks[i].LowPrice < candleSticks[i + 1].LowPrice &&
                    candleSticks[i - 1].LowPrice < candleSticks[i - 2].LowPrice &&
                    candleSticks[i + 1].LowPrice < candleSticks[i + 2].LowPrice)
                {
                    candleSticks[i].IsSupport = true;
                    continue;
                }

                if (candleSticks[i].HighPrice > candleSticks[i - 1].HighPrice &&
                    candleSticks[i].HighPrice > candleSticks[i + 1].HighPrice &&
                    candleSticks[i - 1].HighPrice > candleSticks[i - 2].HighPrice &&
                    candleSticks[i + 1].HighPrice > candleSticks[i + 2].HighPrice)
                {
                    candleSticks[i].IsResistance = true;
                    continue;
                }
            }
        }
    }
}
