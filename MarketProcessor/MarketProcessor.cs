using MarketProcessor.Entities;
using MarketProcessor.Enums;
using MarketProcessor.MarketConditionQualifiers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketProcessor
{
    public class MarketProcessor
    {
        private MarketAnalyzer _analyzer = new MarketAnalyzer();
        private IMarketConditionQualifier _marketConditionQualifier;

        //public MarketConditions GetCurrentMarketCondition()
        //{

        //}

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
