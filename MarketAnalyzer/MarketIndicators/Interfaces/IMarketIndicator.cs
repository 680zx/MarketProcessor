using MarketAnalyzer.Entities;
using System.Collections.Generic;

namespace MarketAnalyzer.MarketIndicators.Interfaces
{
    public interface IMarketIndicator
    {
        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks);
    }
}
