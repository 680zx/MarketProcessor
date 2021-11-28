namespace MarketProcessor.Entities
{
    public class CandleStickChart
    {
        public int Id { get; set; }
        public double HighPrice { get; set; }
        public double LowPrice { get; set; }
        public double OpenPrice { get; set; }
        public double ClosePrice { get; set; }
        public double UpperShadow { get; set; }
        public double LowerShadow { get; set; }
        public double RealBody { get; set; }
    }
}
