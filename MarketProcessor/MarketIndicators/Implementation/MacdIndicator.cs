using AutoMapper;
using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("MarketProcessor.Tests")]
namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class MacdIndicator : IMarketIndicator
    {
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BaseIndicatorBlock, RecurrentIndicatorBlock>()));

        public IndicatorType Type => IndicatorType.MACD;

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            List<MacdIndicatorBlock> processedCandleSticks = (List<MacdIndicatorBlock>)_mapper
                .Map<IList<BaseIndicatorBlock>, IList<MacdIndicatorBlock>>(candleSticks);



            return processedCandleSticks.ConvertAll(i => (BaseIndicatorBlock)i);
        }
    }
}
