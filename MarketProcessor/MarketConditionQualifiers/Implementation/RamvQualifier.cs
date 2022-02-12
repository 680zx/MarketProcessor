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
        private int _flatMarketMinScoreBorder = 10;

        private int _minRecurrentCandleStickPeriod = 4;
        private int _maxRecurrentCandleStickPeriod = 8;
        private double _macdMaxDeviationFromAxisRatio = 0.2; // in percents; so 0.2 means 20% 

        public int Id { get; set; }

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

        public double MacdMaxDeviationFromAxisRatio
        {
            get => _macdMaxDeviationFromAxisRatio;
            set
            {
                if (value < -1)
                    throw new ArgumentException("MACD deviation from axis cannot be less than -1", nameof(value));

                if (value > 1)
                    throw new ArgumentException("MACD deviation from axis cannot be greater than 1", nameof(value));

                _macdMaxDeviationFromAxisRatio = value;
            }
        }

        public MarketCondition GetCurrentMarketCondition(IDictionary<IndicatorType, IList<BaseIndicatorBlock>> processedCandleStickChartsDict)
        {
            var result = MarketCondition.Undefined;
            // TODO: продумать алгоритм формирования баллов totalScore
            // возможно, стоит для каждого индикатора назначить переменную
            // типа bool, и уже по ним судить о состоянии рынка
            // Sorry for using Russian language, but I suggest you
            // to copy comment above and translate it using translate.google.com
            var totalScore = 0;
            int? anomalyIndex = null;

            foreach (var processedCandleStickCharts in processedCandleStickChartsDict)
            {
                switch (processedCandleStickCharts.Key) 
                {
                    case IndicatorType.RecurrentCandle:
                        totalScore += GetMarketConditionByRecurrentIndicator(processedCandleStickCharts.Value);
                        break;

                    case IndicatorType.PriceAnomalySearcher:
                        totalScore += GetCurrentConditionByPriceAnomalyIndicator(processedCandleStickCharts.Value, out anomalyIndex);
                        break;

                    case IndicatorType.MACD:
                        totalScore += GetCurrentConditionByMacdIndicator(processedCandleStickCharts.Value, anomalyIndex);
                        break;

                    case IndicatorType.LowVolumeSearcher:
                        totalScore += GetCurrentConditionByVolumeAnomalyIndicator(processedCandleStickCharts.Value, anomalyIndex);
                        break;
                }
            }

            if (totalScore > _flatMarketMinScoreBorder)
                return MarketCondition.Flat;
            else
                return MarketCondition.Undefined;
        }

        private int GetMarketConditionByRecurrentIndicator(IList<BaseIndicatorBlock> candleSticks)
        {
            int resultScore = 0;
            var recurrentCandleStickBlockList = candleSticks.Cast<RecurrentIndicatorBlock>().ToList();
            var supportLevelIndices = recurrentCandleStickBlockList.Where(i => i.IsSupport).ToList();
            var resistanceLevelIndices = recurrentCandleStickBlockList.Where(i => i.IsResistance).ToList();

            if (resistanceLevelIndices.Count <= 1 || supportLevelIndices.Count <= 1)
                resultScore -= 5;
            else
                for (int i = 0; i < supportLevelIndices.Count; i++)
                {
                    var periodDiff = supportLevelIndices[i + 1].CandleStickChartId - supportLevelIndices[i].CandleStickChartId;
                    if (periodDiff >= _minRecurrentCandleStickPeriod && periodDiff <= _maxRecurrentCandleStickPeriod)
                        resultScore++;
                    else
                        resultScore--;
                }

            return resultScore;
        }

        private int GetCurrentConditionByPriceAnomalyIndicator(IList<BaseIndicatorBlock> candleSticks, out int? anomalyIndex)
        {
            const int HAS_ANOMALY_SCORE = 5;
            const int NO_ANOMALY_SCORE = -5;
            var priceAnomalyCandleStickBlockList = candleSticks.Cast<PriceIndicatorBlock>().ToList();
            anomalyIndex = priceAnomalyCandleStickBlockList.Where(i => i.IsAnomaly).FirstOrDefault().CandleStickChartId;

            if (priceAnomalyCandleStickBlockList.Any(i => i.IsAnomaly))
                return HAS_ANOMALY_SCORE;
            else 
                return NO_ANOMALY_SCORE;
        }

        private int GetCurrentConditionByMacdIndicator(IList<BaseIndicatorBlock> candleSticks, int? anomalyIndex)
        {
            const int NO_ANOMALY_SCORE = -5;

            if (anomalyIndex == null)
                return NO_ANOMALY_SCORE;

            int resultScore = 0;
            var macdCandleStickBlockList = candleSticks.Cast<MacdIndicatorBlock>().ToList();
            var minMacdDeltaValue = Math.Abs(macdCandleStickBlockList.Min(i => i.MacdDelta));
            var maxMacdDeltaValue = Math.Abs(macdCandleStickBlockList.Max(i => i.MacdDelta));

            for (int i = anomalyIndex.Value; i < macdCandleStickBlockList.Count; i++)
            {
                if (Math.Abs(macdCandleStickBlockList[i].MacdDelta) <= minMacdDeltaValue * _macdMaxDeviationFromAxisRatio &&
                    Math.Abs(macdCandleStickBlockList[i].MacdDelta) <= maxMacdDeltaValue * _macdMaxDeviationFromAxisRatio)
                    resultScore++;
                else
                    resultScore--;
            }

            return resultScore;
        }

        private int GetCurrentConditionByVolumeAnomalyIndicator(IList<BaseIndicatorBlock> candleSticks, int? anomalyIndex)
        {
            const int NO_ANOMALY_SCORE = -5;

            if (anomalyIndex == null)
                return NO_ANOMALY_SCORE;

            int resultScore = 0;
            var volCandleStickBlockList = candleSticks.Cast<VolumeIndicatorBlock>().ToList();

            if (volCandleStickBlockList.Any(i => i.IsLowVolume))
            {
                for (int i = anomalyIndex.Value; i < volCandleStickBlockList.Count; i++)
                {
                    if (volCandleStickBlockList[i].IsLowVolume)
                        resultScore++;
                    else
                        resultScore--;
                }
            }
            else
                return NO_ANOMALY_SCORE;

            return resultScore;
        }
    }
}