﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Interfaces;
using AutoMapper;

[assembly: InternalsVisibleTo("MarketProcessor.Tests")]
namespace MarketProcessor.MarketIndicators.Implementation
{
    internal class RecurrentCandleIndicator : IMarketIndicator
    {
        private Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<BaseIndicatorBlock, RecurrentIndicatorBlock>()));

        public IList<BaseIndicatorBlock> Process(IList<BaseIndicatorBlock> candleSticks)
        {

            List<RecurrentIndicatorBlock> processedCandleSticks = (List<RecurrentIndicatorBlock>)_mapper
                .Map<IList<BaseIndicatorBlock>, IList<RecurrentIndicatorBlock>>(candleSticks);

            // The indexer starts from 2 because we have to compare current candle value 
            // with two previous values. The comparing window includes 5 neighbour candles,
            // where current is the middle one.
            for (int i = 2; i < processedCandleSticks.Count - 2; i++)
            {
                if (processedCandleSticks[i].CandleStickChart.LowPrice < processedCandleSticks[i - 1].CandleStickChart.LowPrice &&
                    processedCandleSticks[i].CandleStickChart.LowPrice < processedCandleSticks[i + 1].CandleStickChart.LowPrice &&
                    processedCandleSticks[i - 1].CandleStickChart.LowPrice < processedCandleSticks[i - 2].CandleStickChart.LowPrice &&
                    processedCandleSticks[i + 1].CandleStickChart.LowPrice < processedCandleSticks[i + 2].CandleStickChart.LowPrice)
                {
                    processedCandleSticks[i].IsSupport = true;
                    continue;
                }

                if (processedCandleSticks[i].CandleStickChart.HighPrice > processedCandleSticks[i - 1].CandleStickChart.HighPrice &&
                    processedCandleSticks[i].CandleStickChart.HighPrice > processedCandleSticks[i + 1].CandleStickChart.HighPrice &&
                    processedCandleSticks[i - 1].CandleStickChart.HighPrice > processedCandleSticks[i - 2].CandleStickChart.HighPrice &&
                    processedCandleSticks[i + 1].CandleStickChart.HighPrice > processedCandleSticks[i + 2].CandleStickChart.HighPrice)
                {
                    processedCandleSticks[i].IsResistance = true;
                    continue;
                }
            }

            return processedCandleSticks.ConvertAll(i => (BaseIndicatorBlock)i);
        }
    }
}