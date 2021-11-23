using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketConditionQualifier.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace MarketProcessor.MarketConditionQualifier.Implementation
{
    public class ReccurentCandleIndicator : IConditionQualifier
    {
        public MarketConditions GetCurrentCondition(IList<CandleStickChart> candleSticks)
        {
            // The indexer starts from 2 because we have to compare current candle value 
            // with two previous values. The comparing window includes 5 neighbour candles,
            // where current is the middle one.
            for (int i = 2; i < data.Rows.Count - 2; i++)
            {
                if (data.Rows[i][1] > data.Rows[i][2])
            }
        }
    }
}
