using MarketAnalyzer.MarketIndicators.Implementation;
using MarketAnalyzer.MarketIndicators.Interfaces;
using System.Collections.Generic;

namespace MarketAnalyzer
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
