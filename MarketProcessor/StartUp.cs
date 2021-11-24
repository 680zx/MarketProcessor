using MarketProcessor.MarketConditionQualifier.Implementation;
using MarketProcessor.MarketConditionQualifier.Interfaces;
using System.Collections.Generic;

namespace MarketProcessor
{
    internal class StartUp
    {
        private static Dictionary<string, IConditionQualifier> _marketQualifiers;
        internal static Dictionary<string, IConditionQualifier> MarketQualifiers => _marketQualifiers;

        internal static void Init()
        {
            _marketQualifiers = new Dictionary<string, IConditionQualifier>
             {
                 { "Reccurent Candle Indicator", new ReccurentCandleIndicator() }
             };
        }

    }
}
