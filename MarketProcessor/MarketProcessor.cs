using System.Collections.Generic;
using MarketProcessor.Entities;
using MarketProcessor.MarketIndicators.Interfaces;

namespace MarketProcessor
{
    public class MarketProcessor
    {
        private IList<CandleStickChart> _candleSticks = new List<CandleStickChart>();
        private IMarketIndicator _marketIndicator;

        public IList<CandleStickChart> GetCandleStickCharts() => _candleSticks;

        public IDictionary<string, IMarketIndicator> GetRegisteredMarketIndicators() => StartUp.MarketIndicators;

        public MarketProcessor(IMarketIndicator marketIndicator)
        {
            StartUp.Init();
            _marketIndicator = marketIndicator;
        }

        public void SelectMarketIndicator(IMarketIndicator marketIndicator)
        {
            _marketIndicator = marketIndicator;
        }

        public void Process()
        {
            _marketIndicator.Process(_candleSticks);
        }

        public void LoadCandleStickCharts(IList<CandleStickChart> candleSticks)
        {
            _candleSticks = candleSticks;
        }        
    }
}
