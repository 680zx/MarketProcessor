using MarketProcessor.MarketIndicators.Implementation;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;

namespace MarketProcessor
{
    internal class StartUp
    {
        private static Dictionary<string, IMarketIndicator> _registeredMarketIndicators;
        internal static Dictionary<string, IMarketIndicator> RegisteredMarketIndicators => _registeredMarketIndicators;

        internal static void Init()
        {
            _registeredMarketIndicators = new Dictionary<string, IMarketIndicator>
             {
                 { "Recurrent Candle Indicator", new RecurrentCandleIndicator() }
             };
        }
    }
}
