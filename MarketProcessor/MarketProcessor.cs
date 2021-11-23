using System.Collections.Generic;
using MarketProcessor.Entities;
using MarketProcessor.MarketConditionQualifier.Interfaces;

namespace MarketProcessor
{
    public class MarketProcessor
    {
        private IList<CandleStickChart> _candleSticks = new List<CandleStickChart>();
        private IConditionQualifier _conditionQualifier;

        public MarketProcessor(IConditionQualifier conditionQualifier)
        {
            _conditionQualifier = conditionQualifier;
        }

        public void Process()
        {
            _conditionQualifier.Process(_candleSticks);
        }

        public void Load(IList<CandleStickChart> candleSticks)
        {
            _candleSticks = candleSticks;
        }

        public IList<CandleStickChart> Get() => _candleSticks;
    }
}
