using AutoMapper;
using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class MaIndicator : IMarketIndicator
    {
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BaseIndicatorBlock, RecurrentIndicatorBlock>()));

        public IndicatorType Type => IndicatorType.MA;

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
