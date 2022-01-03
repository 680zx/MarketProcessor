using System;

namespace MarketProcessor.Entities
{
    public class MaIndicatorBlock : BaseIndicatorBlock, ICloneable
    {
        public double EmaValue { get; set; }

        public object Clone()
        {
            return new MacdIndicatorBlock
            {
                CandleStickChart = (CandleStickChart)CandleStickChart.Clone(),
                CandleStickChartId = this.CandleStickChartId,
                CandleStickVolume = this.CandleStickVolume,
                EmaValue = this.EmaValue
            };
        }
    }
}
