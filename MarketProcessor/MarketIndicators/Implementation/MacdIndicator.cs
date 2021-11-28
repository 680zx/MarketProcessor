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
    // EMA_t = alpha * Price_t + (1 - alpha) * EMA_(t-1),
    // where t - value of price at a particular point
    internal class MacdIndicator : IMarketIndicator
    {
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BaseIndicatorBlock, RecurrentIndicatorBlock>()));
        private double _alphaRate;

        public IndicatorType Type => IndicatorType.MACD;

        public MacdIndicator(double alphaRate)
        {
            _alphaRate = alphaRate;
        }

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            List<MacdIndicatorBlock> processedCandleSticks = (List<MacdIndicatorBlock>)_mapper
                .Map<IList<BaseIndicatorBlock>, IList<MacdIndicatorBlock>>(candleSticks);



            return processedCandleSticks.ConvertAll(i => (BaseIndicatorBlock)i);
        }
    }
}
