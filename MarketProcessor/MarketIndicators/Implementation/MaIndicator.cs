using AutoMapper;
using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;

namespace MarketProcessor.MarketIndicators.Implementation
{
    // EMA_t = alpha * Price_t + (1 - alpha) * EMA_(t-1),
    // where t - value of price at a particular point
    internal class MaIndicator : IMarketIndicator
    {
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BaseIndicatorBlock, RecurrentIndicatorBlock>()));
        private double _alphaRate;

        public IndicatorType Type => IndicatorType.MA;

        public MaIndicator(double alphaRate = 0.5)
        {
            _alphaRate = alphaRate;
        }

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {
            List<MaIndicatorBlock> processedCandleSticks = (List<MaIndicatorBlock>)_mapper
                .Map<IList<BaseIndicatorBlock>, IList<MaIndicatorBlock>>(candleSticks);

            // The first EMA value is usually equal to the price of the first value on the candlestick chart
            processedCandleSticks[0].EmaValue = processedCandleSticks[0].CandleStickChart.ClosePrice;

            for (int currentItemIndex = 1; currentItemIndex < processedCandleSticks.Count; currentItemIndex++)
            {
                processedCandleSticks[currentItemIndex].EmaValue = _alphaRate * processedCandleSticks[currentItemIndex].CandleStickChart.ClosePrice +
                        (1 - _alphaRate) * processedCandleSticks[currentItemIndex - 1].EmaValue;
            }

            return processedCandleSticks.ConvertAll(i => (BaseIndicatorBlock)i);
        }

        public IList<MaIndicatorBlock> ProcessWithMaIndicatorBlock(IList<BaseIndicatorBlock> candleSticks)
        {
            List<MaIndicatorBlock> processedCandleSticks = (List<MaIndicatorBlock>)_mapper
                .Map<IList<BaseIndicatorBlock>, IList<MaIndicatorBlock>>(candleSticks);

            processedCandleSticks[0].EmaValue = processedCandleSticks[0].CandleStickChart.ClosePrice;

            for (int currentItemIndex = 1; currentItemIndex < processedCandleSticks.Count; currentItemIndex++)
            {
                processedCandleSticks[currentItemIndex].EmaValue = _alphaRate * processedCandleSticks[currentItemIndex].CandleStickChart.ClosePrice +
                        (1 - _alphaRate) * processedCandleSticks[currentItemIndex - 1].EmaValue;
            }

            return processedCandleSticks;
        }
    }
}
