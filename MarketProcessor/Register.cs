using MarketProcessor.MarketIndicators.Implementation;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;

namespace MarketProcessor
{
    internal class Register
    {
        private static Dictionary<string, IMarketIndicator> _marketIndicators = new Dictionary<string, IMarketIndicator>
        {
            { "Recurrent", new RecurrentCandleIndicator() }
        };

        internal static Dictionary<string, IMarketIndicator> MarketIndicators => _marketIndicators;
    }
}
