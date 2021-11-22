using MarketProcessor.Enums;
using MarketProcessor.MarketConditionQualifier.Interfaces;
using System;
using System.Data;

namespace MarketProcessor.MarketConditionQualifier.Implementation
{
    class ReccurentCandleIndicator : IConditionQualifier
    {
        public MarketConditions GetCurrentCondition(DataTable data)
        {
            throw new NotImplementedException();
        }
    }
}
