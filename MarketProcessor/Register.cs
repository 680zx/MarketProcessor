using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketIndicators.Implementation;
using MarketProcessor.MarketIndicators.Interfaces;
using System.Collections.Generic;

namespace MarketProcessor
{
    internal class Register
    {
        private const int MA_INDICATOR_SHORT_PERIOD = 12;
        private const int MA_INDICATOR_LONG_PERIOD = 26;
        private const int MA_INDICATOR_SMOOTH = 9;
        private const double MAX_TO_AVG_DIFFERENCE_COEFFICIENT = 2.5;
        private const double HIGH_VOLUME_BORDER_COEFFICIENT = 2.5;
        private const double LOW_PRICE_BORDER_COEFFICIENT = 3;

        private static Dictionary<IndicatorType, IMarketIndicator<BaseIndicatorBlock>> _marketIndicators = new Dictionary<IndicatorType, IMarketIndicator<BaseIndicatorBlock>>
        {
            { IndicatorType.RecurrentCandle, new RecurrentCandleIndicator() as IMarketIndicator<BaseIndicatorBlock>},

            { IndicatorType.MA, new MaIndicator() as IMarketIndicator<BaseIndicatorBlock>},

            { IndicatorType.MACD, new MacdIndicator(new MaIndicator(MA_INDICATOR_SHORT_PERIOD),
                new MaIndicator(MA_INDICATOR_LONG_PERIOD), new MaIndicator(MA_INDICATOR_SMOOTH)) as IMarketIndicator<BaseIndicatorBlock>},

            { IndicatorType.LowVolumeSearcher, new LowVolumeSearchIndicator(MAX_TO_AVG_DIFFERENCE_COEFFICIENT, HIGH_VOLUME_BORDER_COEFFICIENT) as IMarketIndicator<BaseIndicatorBlock>},

            { IndicatorType.PriceAnomalySearcher, new PriceAnomalySearchIndicator(LOW_PRICE_BORDER_COEFFICIENT) as IMarketIndicator<BaseIndicatorBlock>}
        };

        internal static Dictionary<IndicatorType, IMarketIndicator<BaseIndicatorBlock>> MarketIndicators => _marketIndicators;
    }
}
