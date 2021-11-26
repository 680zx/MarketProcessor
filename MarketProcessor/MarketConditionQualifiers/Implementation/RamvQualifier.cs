using AutoMapper;
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
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BaseIndicatorBlock, RecurrentIndicatorBlock>()));

        public MarketConditions GetCurrentMarketCondition(IDictionary<IndicatorType, IList<BaseIndicatorBlock>> processedCandleStickChartsDict)
        {
            var result = MarketConditions.Undefined;

            foreach (var processedCandleStickCharts in processedCandleStickChartsDict)
            {
                if (processedCandleStickCharts.Key == IndicatorType.RecurrentCandle) 
                {
                    var recurrentCandleStickBlockList = _mapper.Map<IList<BaseIndicatorBlock>, IList<RecurrentIndicatorBlock>>(processedCandleStickCharts.Value);
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
