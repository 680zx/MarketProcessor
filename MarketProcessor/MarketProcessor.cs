using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketAnalyzers;
using MarketProcessor.MarketConditionQualifiers.Interfaces;
using System.Collections.Generic;

namespace MarketProcessor
{
    public class MarketProcessor
    {
        private CsMarketAnalyzer _analyzer = new CsMarketAnalyzer();
        private IMarketConditionQualifier _marketConditionQualifier;

        public MarketCondition GetCurrentMarketCondition()
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
