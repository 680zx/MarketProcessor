using AutoMapper;
using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarketProcessor.Tests")]
namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class MacdIndicator : IMarketIndicator<MacdIndicatorBlock>
    {
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BaseIndicatorBlock, RecurrentIndicatorBlock>()));
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

        public IList<MacdIndicatorBlock> Process(IList<MacdIndicatorBlock> candleSticks)
        {
            List<MacdIndicatorBlock> processedCandleSticks = (List<MacdIndicatorBlock>)(candleSticks);

            var maProcessedCandleSticks = ((List<BaseIndicatorBlock>)candleSticks).ConvertAll(i => (MaIndicatorBlock)i);
            IList<MaIndicatorBlock> shortPeriodMaProcessedCandleSticks = _shortPeriodMaIndicator.ProcessWithMaIndicatorBlock(maProcessedCandleSticks);
            IList<MaIndicatorBlock> longPeriodMaProcessedCandleSticks = _longPeriodMaIndicator.ProcessWithMaIndicatorBlock(maProcessedCandleSticks);
            IList<MaIndicatorBlock> smoothMaProcessedCandleSticks = _smoothMaIndicator.ProcessWithMaIndicatorBlock(maProcessedCandleSticks);

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

            return processedCandleSticks;
        }
    }
}
