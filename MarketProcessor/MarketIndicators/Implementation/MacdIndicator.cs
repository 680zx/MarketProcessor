using AutoMapper;
using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarketProcessor.Tests")]
namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class MacdIndicator : IMarketIndicator
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

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            List<MacdIndicatorBlock> processedCandleSticks = (List<MacdIndicatorBlock>)_mapper
                .Map<IList<BaseIndicatorBlock>, IList<MacdIndicatorBlock>>(candleSticks);

            IList<MaIndicatorBlock> shortPeriodMaProcessedCandleSticks = _shortPeriodMaIndicator.ProcessWithMaIndicatorBlock(candleSticks);
            IList<MaIndicatorBlock> longPeriodMaProcessedCandleSticks = _longPeriodMaIndicator.ProcessWithMaIndicatorBlock(candleSticks);
            IList<MaIndicatorBlock> smoothMaProcessedCandleSticks = _smoothMaIndicator.ProcessWithMaIndicatorBlock(candleSticks);

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

            return processedCandleSticks.ConvertAll(i => (BaseIndicatorBlock)i);
        }
    }
}
