using System;

namespace Yahoo.Finance
{
    public class EquityData
    {
        public DateTimeOffset DataCollectedOn { get; set; }

        public EquityData()
        {
            DataCollectedOn = DateTimeOffset.Now;
        }
    }
}
