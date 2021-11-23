using MarketProcessor.Entities;
using MarketProcessor.Enums;
using System.Collections.Generic;

namespace MarketProcessor.MarketConditionQualifier.Interfaces
{
    public interface IConditionQualifier
    {
        public MarketConditions GetCurrentCondition(IList<CandleStickChart> candleSticks);
    }
}
