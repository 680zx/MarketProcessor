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

            IList<MacdIndicatorBlock> processedCandleSticks = candleSticks
                .Cast<MacdIndicatorBlock>().ToList()
                .ConvertAll(stick => (MacdIndicatorBlock)stick.Clone());

            IList<BaseIndicatorBlock> GetNewMaIndicatorBlocks(IList<BaseIndicatorBlock> baseIndicatorBlocks)
            {
                return baseIndicatorBlocks.Cast<MaIndicatorBlock>().ToList()
                    .ConvertAll(block => (MaIndicatorBlock)block.Clone())
                    .Cast<BaseIndicatorBlock>().ToList();
            }

            IList<MaIndicatorBlock> shortPeriodMaProcessedCandleSticks = _shortPeriodMaIndicator.Process(GetNewMaIndicatorBlocks(candleSticks))
                .Cast<MaIndicatorBlock>().ToList();
            IList<MaIndicatorBlock> longPeriodMaProcessedCandleSticks = _longPeriodMaIndicator.Process(GetNewMaIndicatorBlocks(candleSticks))
                .Cast<MaIndicatorBlock>().ToList();

            for (int currentItemIndex = 0; currentItemIndex < processedCandleSticks.Count; currentItemIndex++)
            {
                // MACD = EMA_short(Price) - EMA_long(Price), where EMA - MA Indicator
                processedCandleSticks[currentItemIndex].MacdValue = shortPeriodMaProcessedCandleSticks[currentItemIndex].EmaValue -
                    longPeriodMaProcessedCandleSticks[currentItemIndex].EmaValue;

                // Signal_MACD = Ema_Smooth(EMA_short(Price) - EMA_long(Price)) = Ema_Smooth(MACD)
                if (currentItemIndex == 0)
                    processedCandleSticks[currentItemIndex].SignalMacdValue = processedCandleSticks[currentItemIndex].MacdValue;
                else processedCandleSticks[currentItemIndex].SignalMacdValue = _smoothMaIndicator.GetCurrentMaValue(
                    processedCandleSticks[currentItemIndex].MacdValue, 
                    processedCandleSticks[currentItemIndex].SignalMacdValue);

                // MACD Delta = MACD - Signal_MACD
                processedCandleSticks[currentItemIndex].MacdDelta = processedCandleSticks[currentItemIndex].MacdValue -
                    processedCandleSticks[currentItemIndex].SignalMacdValue;
            }

            return processedCandleSticks.Cast<BaseIndicatorBlock>().ToList();
        }
    }
}
