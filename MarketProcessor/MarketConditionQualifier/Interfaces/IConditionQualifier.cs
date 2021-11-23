using MarketProcessor.Entities;
using System.Collections.Generic;

namespace MarketProcessor.MarketConditionQualifier.Interfaces
{
    public interface IConditionQualifier
    {
        public void Process(IList<CandleStickChart> candleSticks);
    }
}
