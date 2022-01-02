using System;

namespace MarketProcessor.Entities
{
    public class MacdIndicatorBlock : MaIndicatorBlock, ICloneable
    {
        public double MacdValue { get; set; }
        public double SignalMacdValue { get; set; }
        public double MacdDelta { get; set; }

        public object Clone()
        {
            return new MacdIndicatorBlock
            {
                CandleStickChart = (CandleStickChart)CandleStickChart.Clone(),
                CandleStickChartId = this.CandleStickChartId,
                CandleStickVolume = this.CandleStickVolume,
                EmaValue = this.EmaValue,
                MacdValue = this.MacdValue,
                SignalMacdValue = this.SignalMacdValue ,
                MacdDelta = this.MacdDelta
            };
        }
    }
}
