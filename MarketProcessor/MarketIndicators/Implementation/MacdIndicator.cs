﻿using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarketProcessor.Tests")]
namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class MacdIndicator : IMarketIndicator
    {
        private MaIndicator _shortPeriodMaIndicator;
        private MaIndicator _longPeriodMaIndicator;
        private MaIndicator _smoothMaIndicator;

        public IndicatorType Type => IndicatorType.MACD;

        public MacdIndicator(MaIndicator shortPeriodMaIndicator, MaIndicator longPeriodMaIndicator,
            MaIndicator smoothMaIndicator)
        {
            _shortPeriodMaIndicator = shortPeriodMaIndicator; 
            _longPeriodMaIndicator = longPeriodMaIndicator;
            _smoothMaIndicator = smoothMaIndicator;
        }

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            IList<MacdIndicatorBlock> processedCandleSticks = candleSticks.Cast<MacdIndicatorBlock>().ToList();

            IList<MaIndicatorBlock> shortPeriodMaProcessedCandleSticks = _shortPeriodMaIndicator.Process(candleSticks)
                .Cast<MaIndicatorBlock>().ToList();
            IList<MaIndicatorBlock> longPeriodMaProcessedCandleSticks = _longPeriodMaIndicator.Process(candleSticks)
                .Cast<MaIndicatorBlock>().ToList();
            IList<MaIndicatorBlock> smoothMaProcessedCandleSticks = _smoothMaIndicator.Process(candleSticks)
                .Cast<MaIndicatorBlock>().ToList();

            for (int currentItemIndex = 0; currentItemIndex < processedCandleSticks.Count; currentItemIndex++)
            {
                // MACD = EMA_short(Price) - EMA_long(Price), where EMA - MA Indicator
                processedCandleSticks[currentItemIndex].MacdValue = shortPeriodMaProcessedCandleSticks[currentItemIndex].EmaValue -
                    longPeriodMaProcessedCandleSticks[currentItemIndex].EmaValue;

                // Signal_MACD = Ema_Smooth * (EMA_short(Price) - EMA_long(Price)) =
                // = Ema_Smooth * MACD
                processedCandleSticks[currentItemIndex].SignalMacdValue = processedCandleSticks[currentItemIndex].MacdValue *
                    smoothMaProcessedCandleSticks[currentItemIndex].EmaValue;

                // MACD Delta = MACD - Signal_MACD
                processedCandleSticks[currentItemIndex].MacdDelta = processedCandleSticks[currentItemIndex].MacdValue -
                    processedCandleSticks[currentItemIndex].SignalMacdValue;
            }

            return processedCandleSticks.Cast<BaseIndicatorBlock>().ToList();
        }
    }
}
