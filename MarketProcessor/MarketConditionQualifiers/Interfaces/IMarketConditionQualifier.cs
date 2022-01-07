﻿using MarketProcessor.Entities;
using MarketProcessor.Enums;
using System.Collections.Generic;

namespace MarketProcessor.MarketConditionQualifiers.Interfaces
{
    public interface IMarketConditionQualifier
    {
        public MarketCondition GetCurrentMarketCondition(IDictionary<IndicatorType, IList<BaseIndicatorBlock>> processedCandleStickCharts);
    }
}
