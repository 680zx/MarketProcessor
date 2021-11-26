﻿using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketConditionQualifiers.Interfaces;
using System;
using System.Collections.Generic;

namespace MarketProcessor.MarketConditionQualifiers.Implementation
{
    // R - Recurrent
    // A - Anomaly
    // M - MACD
    // V - Volume
    public class RamvQualifier : IMarketConditionQualifier
    {
        public MarketConditions GetCurrentMarketCondition(IDictionary<string, IList<BaseIndicatorBlock>> processedCandleStickCharts)
        {
            throw new NotImplementedException();
        }
    }
}
