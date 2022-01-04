using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketConditionQualifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketProcessor.MarketConditionQualifiers.Implementation
{
    // R - Recurrent
    // A - price Anomaly
    // M - MACD
    // V - Volume anomaly
    public class RamvQualifier : IMarketConditionQualifier
    {
        private int _minRecurrentCandleStickPeriod = 4;
        private int _maxRecurrentCandleStickPeriod = 8;

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
