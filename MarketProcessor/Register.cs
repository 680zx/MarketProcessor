using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Implementation;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;

namespace MarketProcessor
{
    internal class Register
    {
        private static Dictionary<IndicatorType, IMarketIndicator> _marketIndicators = new Dictionary<IndicatorType, IMarketIndicator>
        {
            { IndicatorType.RecurrentCandle, new RecurrentCandleIndicator() },
            { IndicatorType.MA, new MaIndicator() }
        };

        internal static Dictionary<IndicatorType, IMarketIndicator> MarketIndicators => _marketIndicators;
    }
}
