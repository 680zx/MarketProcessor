using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketConditionQualifiers.Interfaces;
using System.Collections.Generic;

namespace MarketProcessor
{
    public class MarketProcessor
    {
        private MarketAnalyzer _analyzer = new MarketAnalyzer();
        private IMarketConditionQualifier _marketConditionQualifier;

        public MarketConditions GetCurrentMarketCondition()
        {
            foreach (var indicator in Register.MarketIndicators)
            {
                _analyzer.SelectMarketIndicator(indicator.Value);
                _analyzer.Process();
            }

            return _marketConditionQualifier.GetCurrentMarketCondition(_analyzer.ProcessedCandleSticks);
        }

        public void LoadData(IList<BaseIndicatorBlock> data)
        {
            _analyzer.LoadCandleStickCharts(data);
        }

        public void SelectMarketConditionQualifier(IMarketConditionQualifier marketConditionQualifier)
        {
            _marketConditionQualifier = marketConditionQualifier;
        }
    }
}
