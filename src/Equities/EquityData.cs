using System;

namespace Yahoo.Finance
{
    public class EquityData
    {
        public string QueriedSymbol {get; set;}
        public DateTimeOffset DataCollectedOn { get; set; }

        public EquityData()
        {
            DataCollectedOn = DateTimeOffset.Now;
        }
    }
}
