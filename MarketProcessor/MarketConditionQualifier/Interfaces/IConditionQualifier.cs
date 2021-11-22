using MarketProcessor.Enums;
using System.Data;

namespace MarketProcessor.MarketConditionQualifier.Interfaces
{
    public interface IConditionQualifier
    {
        public MarketConditions GetCurrentCondition(DataTable data);
    }
}
