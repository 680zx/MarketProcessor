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

        public int MinRecurrentCandleStickPeriod
        {
            get => _minRecurrentCandleStickPeriod;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Period between two candlesticks cannot be negative", nameof(value));

                if (value > MaxRecurrentCandleStickPeriod)
                    throw new ArgumentException("Min period cannot be greater than max period", nameof(value));

                _minRecurrentCandleStickPeriod = value;
            }
        }

        public int MaxRecurrentCandleStickPeriod
        {
            get => _maxRecurrentCandleStickPeriod;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Period between two candlesticks cannot be negative", nameof(value));

                if (value < MinRecurrentCandleStickPeriod)
                    throw new ArgumentException("Max period cannot be less than min period", nameof(value));

                _maxRecurrentCandleStickPeriod = value;
            }
        }

        public MarketConditions GetCurrentMarketCondition(IDictionary<IndicatorType, IList<BaseIndicatorBlock>> processedCandleStickChartsDict)
        {
            var result = MarketConditions.Undefined;
            var totalScore = 0;

            foreach (var processedCandleStickCharts in processedCandleStickChartsDict)
            {
                if (processedCandleStickCharts.Key == IndicatorType.RecurrentCandle) 
                {
                    var recurrentCandleStickBlockList = processedCandleStickCharts.Value.Cast<RecurrentIndicatorBlock>().ToList();
                    var supportLevelIndices = recurrentCandleStickBlockList.Where(i => i.IsSupport).ToList();
                    var resistanceLevelIndices = recurrentCandleStickBlockList.Where(i => i.IsResistance).ToList();

                    if (resistanceLevelIndices.Count <= 1)
                        totalScore -= 5;
                    else
                        for (int i = 0; i < supportLevelIndices.Count; i++)
                        {
                            var periodDiff = supportLevelIndices[i + 1].CandleStickChartId - supportLevelIndices[i].CandleStickChartId;
                            if (periodDiff >= _minRecurrentCandleStickPeriod && periodDiff <= _maxRecurrentCandleStickPeriod)
                                totalScore++;
                            else
                                totalScore--;
                        }
                }
            }

            return result;
        }
    }
}
