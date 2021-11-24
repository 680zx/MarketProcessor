using MarketProcessor.MarketIndicators.Implementation;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;

namespace MarketProcessor
{
    internal class StartUp
    {
        private static Dictionary<string, IMarketIndicator> _marketIndicators;
        internal static Dictionary<string, IMarketIndicator> MarketIndicators => _marketIndicators;

        internal static void Init()
        {
            _marketIndicators = new Dictionary<string, IMarketIndicator>
             {
                 { "Reccurent Candle Indicator", new ReccurentCandleIndicator() }
             };
        }

    }
}
