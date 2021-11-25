using MarketProcessor.Entities;
using System.Collections.Generic;

namespace MarketProcessor.MarketIndicators.Interfaces
{
    public interface IMarketIndicator
    {
        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks);
    }
}
