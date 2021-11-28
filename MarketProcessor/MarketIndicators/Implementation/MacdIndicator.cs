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

        public IndicatorType Type => IndicatorType.MACD;

        public MacdIndicator()
        {
        }

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            List<MacdIndicatorBlock> processedCandleSticks = (List<MacdIndicatorBlock>)_mapper
                .Map<IList<BaseIndicatorBlock>, IList<MacdIndicatorBlock>>(candleSticks);

            for (int currentItemIndex = 0; currentItemIndex < processedCandleSticks.Count; currentItemIndex++)
            {

            }

            return processedCandleSticks.ConvertAll(i => (BaseIndicatorBlock)i);
        }
    }
}
