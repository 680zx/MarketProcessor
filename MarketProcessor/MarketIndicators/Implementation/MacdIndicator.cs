using AutoMapper;
using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MarketProcessor.Tests")]
namespace MarketProcessor.MarketIndicators.Implementation
{
    // MACD = EMA_short(Price) - EMA_long(Price), where
    // EMA - MA Indicator
    internal class MacdIndicator : IMarketIndicator
    {
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BaseIndicatorBlock, RecurrentIndicatorBlock>()));
        private MaIndicator _maIndicatorShortPeriod;
        private MaIndicator _maIndicatorLongPeriod;

        public IndicatorType Type => IndicatorType.MACD;

        public MacdIndicator(MaIndicator maIndicatorShortPeriod, MaIndicator maIndicatorLongPeriod)
        {
            _maIndicatorShortPeriod = maIndicatorShortPeriod; 
            _maIndicatorLongPeriod = maIndicatorLongPeriod;
        }

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            List<MacdIndicatorBlock> processedCandleSticks = (List<MacdIndicatorBlock>)_mapper
                .Map<IList<BaseIndicatorBlock>, IList<MacdIndicatorBlock>>(candleSticks);

            var maProcessedCandleSticksShortPeriod = _maIndicatorShortPeriod.ProcessWithMaIndicatorBlock(candleSticks);
            var maProcessedCandleSticksLongPeriod = _maIndicatorLongPeriod.ProcessWithMaIndicatorBlock(candleSticks);

            for (int currentItemIndex = 0; currentItemIndex < processedCandleSticks.Count; currentItemIndex++)
            {
                // TODO: implement the signal MACD line, then 
                // fill delta value (MACD value - signal MACD value)
                processedCandleSticks[currentItemIndex].MacdValue = maProcessedCandleSticksShortPeriod[currentItemIndex].EmaValue -
                    maProcessedCandleSticksLongPeriod[currentItemIndex].EmaValue;
            }

            return processedCandleSticks.ConvertAll(i => (BaseIndicatorBlock)i);
        }
    }
}
