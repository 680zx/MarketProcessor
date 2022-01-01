using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System;
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
            if (shortPeriodMaIndicator == null)
                throw new ArgumentNullException("Short Period Ma Indicator cannot be null", nameof(shortPeriodMaIndicator));
            if (longPeriodMaIndicator == null)
                throw new ArgumentNullException("Long Period Ma Indicator cannot be null", nameof(longPeriodMaIndicator));
            if (smoothMaIndicator == null)
                throw new ArgumentNullException("Smooth Ma Indicator cannot be null", nameof(smoothMaIndicator));

            _shortPeriodMaIndicator = shortPeriodMaIndicator; 
            _longPeriodMaIndicator = longPeriodMaIndicator;
            _smoothMaIndicator = smoothMaIndicator;
        }

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            if (candleSticks == null || candleSticks.Count == 0)
                throw new ArgumentOutOfRangeException("Check the passed list of candlesticks. It's null or empty.");

            //var convertedCandleSticks = candleSticks.Cast<MacdIndicatorBlock>().ToList();

            IList<MacdIndicatorBlock> processedCandleSticks = new List<MacdIndicatorBlock>();
            IList<MacdIndicatorBlock> processedCandleSticks1 = candleSticks
                .Cast<MacdIndicatorBlock>().ToList()
                .ConvertAll(stick => new MacdIndicatorBlock { });
            //convertedCandleSticks.ForEach(i => 
            //{
            //    processedCandleSticks.Add(i);
            //});
            foreach (var item in candleSticks)
            {
                processedCandleSticks.Add((MacdIndicatorBlock)item);
            }

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
