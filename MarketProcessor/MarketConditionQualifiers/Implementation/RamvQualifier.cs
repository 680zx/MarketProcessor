using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketConditionQualifiers.Interfaces;
using System;
using System.Collections.Generic;

namespace MarketProcessor.MarketConditionQualifiers.Implementation
{
    // R - Recurrent
    // A - Anomaly
    // M - MACD
    // V - Volume
    public class RamvQualifier : IMarketConditionQualifier
    {
        public MarketConditions GetCurrentMarketCondition(IDictionary<IndicatorType, IList<BaseIndicatorBlock>> processedCandleStickChartsDict)
        {
            foreach (var processedCandleStickCharts in processedCandleStickChartsDict)
            {
                if (processedCandleStickCharts.Key == IndicatorType.RecurrentCandle) { }
            }

            throw new NotImplementedException();
        }
    }
}
