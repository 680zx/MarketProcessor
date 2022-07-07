using MarketProcessor.Entities;
using MarketProcessor.Enums;
using System.Collections.Generic;

namespace MarketProcessor.CsMarketIndicators.Interfaces
{
    public interface ICsIndicator
    {
        public IList<BaseIndicatorBlock> GetProcessed(IList<BaseIndicatorBlock> candleSticks);
        public IndicatorType Type { get; }
    }
}
