using System.IO;
using System;

namespace Yahoo.Finance
{
    public class HistoricalDataRecord
            {
                public DateTime Date { get; set; }
                public float Open { get; set; }
                public float High { get; set; }
                public float Low { get; set; }
                public float Close { get; set; }
                public float AdjustedClose { get; set; }
                public int Volume { get; set; }
            }
}