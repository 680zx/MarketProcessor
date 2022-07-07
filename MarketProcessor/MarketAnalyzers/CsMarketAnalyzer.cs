using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.CsMarketIndicators.Interfaces;
using System;
using System.Collections.Generic;

namespace MarketProcessor.MarketAnalyzers
{
    public class CsMarketAnalyzer
    {
        private IList<BaseIndicatorBlock> _candleSticks;
        private ICsIndicator _marketIndicator;

        internal IDictionary<IndicatorType, IList<BaseIndicatorBlock>> ProcessedCandleSticks = new Dictionary<IndicatorType, IList<BaseIndicatorBlock>>();

        public void SelectMarketIndicator(ICsIndicator marketIndicator)
        {
            _marketIndicator = marketIndicator ??
                throw new ArgumentNullException("Passed market indicator is null.", nameof(marketIndicator));
        }

        public void Process()
        {
            if (_marketIndicator == null)
                throw new ArgumentNullException("_marketIndicator is null. Select the market indicator before processing the data. Use SelectMarketIndicator method to select the indicator.", 
                    nameof(_marketIndicator));

            if (_candleSticks.Count == 0)
                throw new ArgumentException("Number of candlesticks is 0, pass correct data", nameof(_candleSticks));

            var processedCandleSticks = _marketIndicator.GetProcessed(_candleSticks);
            ProcessedCandleSticks.Add(_marketIndicator.Type, processedCandleSticks);
        }

        public void LoadCandleStickCharts(IList<BaseIndicatorBlock> candleSticks)
        {
            _candleSticks = candleSticks ?? throw new ArgumentNullException("Passed candlesticks data is null.", nameof(candleSticks));
        }
    }
}
