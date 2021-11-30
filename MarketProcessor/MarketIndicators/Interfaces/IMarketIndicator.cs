using MarketProcessor.Entities;
using MarketProcessor.Enums;
using System.Collections.Generic;

namespace MarketProcessor.MarketIndicators.Interfaces
{
    public interface IMarketIndicator<T> //where T : BaseIndicatorBlock
    {
        public IList<T> Process(IList<T> candleSticks);
        public IndicatorType Type { get; } 
    }
}
