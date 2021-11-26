using MarketProcessor.Entities;
using MarketProcessor.Enums;
using System.Collections.Generic;

namespace MarketProcessor.MarketConditionQualifiers.Interfaces
{
    public interface IMarketConditionQualifier
    {
        public MarketConditions GetCurrentMarketCondition(IList<BaseIndicatorBlock> candleStickCharts);
    }
}
