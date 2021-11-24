using MarketProcessor.Entities;
using System.Collections.Generic;

namespace MarketProcessor.MarketIndicators.Interfaces
{
    public interface IMarketIndicator
    {
        public void Process(IList<CandleStickChart> candleSticks);
    }
}
