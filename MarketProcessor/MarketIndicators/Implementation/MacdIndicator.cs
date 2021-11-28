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
        private MaIndicator _maIndicatorShortPeriod;
        private MaIndicator _maIndicatorLongPeriod;
        private MaIndicator _smoothMaIndicator;

        public IndicatorType Type => IndicatorType.MACD;

        public MacdIndicator(MaIndicator maIndicatorShortPeriod, MaIndicator maIndicatorLongPeriod,
            MaIndicator smoothMaIndicator)
        {
            _maIndicatorShortPeriod = maIndicatorShortPeriod; 
            _maIndicatorLongPeriod = maIndicatorLongPeriod;
            _smoothMaIndicator = smoothMaIndicator;
        }

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            List<MacdIndicatorBlock> processedCandleSticks = (List<MacdIndicatorBlock>)_mapper
                .Map<IList<BaseIndicatorBlock>, IList<MacdIndicatorBlock>>(candleSticks);

            var maProcessedCandleSticksShortPeriod = _maIndicatorShortPeriod.ProcessWithMaIndicatorBlock(candleSticks);
            var maProcessedCandleSticksLongPeriod = _maIndicatorLongPeriod.ProcessWithMaIndicatorBlock(candleSticks);
            var maProcessedCandleSticksSmooth = _smoothMaIndicator.ProcessWithMaIndicatorBlock(candleSticks);

            for (int currentItemIndex = 0; currentItemIndex < processedCandleSticks.Count; currentItemIndex++)
            {
                // MACD = EMA_short(Price) - EMA_long(Price), where EMA - MA Indicator
                processedCandleSticks[currentItemIndex].MacdValue = maProcessedCandleSticksShortPeriod[currentItemIndex].EmaValue -
                    maProcessedCandleSticksLongPeriod[currentItemIndex].EmaValue;

                // Signal_MACD = Ema_Smooth * (EMA_short(Price) - EMA_long(Price)) =
                // = Ema_Smooth * MACD
                processedCandleSticks[currentItemIndex].SignalMacdValue = processedCandleSticks[currentItemIndex].MacdValue *
                    maProcessedCandleSticksSmooth[currentItemIndex].EmaValue;

                // MACD Delta = MACD - Signal_MACD
                processedCandleSticks[currentItemIndex].MacdDelta = processedCandleSticks[currentItemIndex].MacdValue -
                    processedCandleSticks[currentItemIndex].SignalMacdValue;
            }

            return processedCandleSticks.ConvertAll(i => (BaseIndicatorBlock)i);
        }
    }
}
