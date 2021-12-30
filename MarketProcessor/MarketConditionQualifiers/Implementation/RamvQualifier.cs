using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketConditionQualifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var result = MarketConditions.Undefined;

            foreach (var processedCandleStickCharts in processedCandleStickChartsDict)
            {
                if (processedCandleStickCharts.Key == IndicatorType.RecurrentCandle) 
                {
                    var recurrentCandleStickBlockList = processedCandleStickCharts.Value.Cast<RecurrentIndicatorBlock>().ToList();
                    foreach (var candleStickChart in recurrentCandleStickBlockList)
                    {
                        if (candleStickChart.IsSupport || candleStickChart.IsResistance)
                        {

                        }
                    }
                }
            }

            return result;
        }
    }
}
