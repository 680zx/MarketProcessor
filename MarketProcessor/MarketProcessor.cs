using System;
using System.Collections.Generic;
using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Interfaces;

namespace MarketProcessor
{
    public class MarketProcessor
    {
        private IList<BaseIndicatorBlock> _candleSticks = new List<BaseIndicatorBlock>();
        private IMarketIndicator _marketIndicator;

        public IList<BaseIndicatorBlock> GetCandleStickCharts() => _candleSticks;

        public IDictionary<string, IMarketIndicator> GetRegisteredMarketIndicators() => StartUp.RegisteredMarketIndicators;

        public MarketProcessor()
        {
            StartUp.Init();
        }

        public void SelectMarketIndicator(IMarketIndicator marketIndicator)
        {
            _marketIndicator = marketIndicator ?? throw new ArgumentNullException(nameof(marketIndicator),
                    "Passed market indicator is null.");
        }

        public void Process()
        {
            if (_marketIndicator == null)
                throw new ArgumentNullException(nameof(_marketIndicator), 
                    "_marketIndicator is null. Select the market indicator before processing the data. " +
                    "Use SelectMarketIndicator method to select the indicator.");

            if (_candleSticks.Count == 0)
                throw new ArgumentException("Number of candlesticks is 0, pass correct data",
                    nameof(_candleSticks));

            _marketIndicator.Process(_candleSticks);
        }

        public void LoadCandleStickCharts(IList<BaseIndicatorBlock> candleSticks)
        {
            _candleSticks = candleSticks ?? throw new ArgumentNullException(nameof(candleSticks),
                    "Passed candlesticks data is null.");
        }
    }
}
