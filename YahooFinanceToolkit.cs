using System;

namespace Yahoo.Finance
{
    public class YahooFinanceToolkit
    {
        public static double GetMarketCapFromString(string representation)
        {
            double ToReturn = 0;

            string last_char = representation.Substring(representation.Length - 1, 1).ToLower();
            float mcf = System.Convert.ToSingle(representation.Substring(0, representation.Length - 1));
            if (last_char == "th")
            {
                ToReturn = System.Convert.ToDouble(mcf * 1000);
            }
            else if (last_char == "m")
            {
                ToReturn = System.Convert.ToDouble(mcf * 1000000);
            }
            else if (last_char == "b")
            {
                ToReturn = System.Convert.ToDouble(mcf * 1000000000);
            }
            else if (last_char == "t")
            {
                ToReturn = System.Convert.ToDouble(mcf * 1000000000000);
            }

            return ToReturn;
        }
    }
}