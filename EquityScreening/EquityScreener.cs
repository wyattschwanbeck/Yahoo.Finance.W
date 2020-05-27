using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace Yahoo.Finance
{
    public class EquityScreener
    {
        
        public async Task<EquitySummaryData[]> GetEquitiesAsync(EquityScreen type)
        {
            string url = "";

            if (type == EquityScreen.Gainers)
            {
                url = "https://finance.yahoo.com/gainers";
            }
            else if (type == EquityScreen.Losers)
            {
                url = "https://finance.yahoo.com/losers";
            }
            else if (type == EquityScreen.MostActives)
            {
                url = "https://finance.yahoo.com/most-active";
            }
            else if (type == EquityScreen.Trending)
            {
                url = "https://finance.yahoo.com/trending-tickers";
            }
            else if (type == EquityScreen.TopEtfs)
            {
                url = "https://finance.yahoo.com/etfs";
            }

            HttpClient hc = new HttpClient();
            HttpResponseMessage hrm = await hc.GetAsync(url);
            string web = await hrm.Content.ReadAsStringAsync();

            EquitySummaryData[] data = GetEquitySummariesFromWebData(web);

            return data;
        }

        private EquitySummaryData[] GetEquitySummariesFromWebData(string web_data)
        {
            int loc1 = 0;
            int loc2 = 0;
            List<string> Splitter = new List<string>();

            loc1 = web_data.IndexOf("<tbody");
            loc2 = web_data.IndexOf("</tbody>", loc1 + 1);
            string table_data = web_data.Substring(loc1 + 1, loc2 - loc1 - 1);

            //Split into rows
            Splitter.Clear();
            Splitter.Add("<tr class");
            string[] rows = table_data.Split(Splitter.ToArray(), StringSplitOptions.None);

            //Get all data
            List<EquitySummaryData> ToReturn = new List<EquitySummaryData>();
            int r = 0;
            for (r=1;r<rows.Length;r++)
            {
                EquitySummaryData esd = new EquitySummaryData();
                
                Splitter.Clear();
                Splitter.Add("<td ");
                string[] cols = rows[r].Split(Splitter.ToArray(), StringSplitOptions.None);

                //Get symbol
                try
                {
                    loc1 = cols[1].IndexOf("a href");
                    loc1 = cols[1].IndexOf(">", loc1 + 1);
                    loc2 = cols[1].IndexOf("<", loc1 + 1);
                    esd.StockSymbol = cols[1].Substring(loc1 + 1, loc2 - loc1 - 1);
                }
                catch
                {
                    esd.StockSymbol = "?";
                }
                

                //Get name
                try
                {
                    loc1 = cols[2].IndexOf("react-text");
                    loc1 = cols[2].IndexOf(">", loc1 + 1);
                    loc2 = cols[2].IndexOf("<", loc1 + 1);
                    esd.Name = cols[2].Substring(loc1 + 1, loc2 - loc1 - 1);
                }
                catch
                {
                    esd.Name = "";
                }
                

                //Get price
                try
                {
                    loc1 = cols[3].IndexOf("span class");
                    loc1 = cols[3].IndexOf(">", loc1 + 1);
                    loc2 = cols[3].IndexOf("<", loc1 + 1);
                    esd.Price = Convert.ToSingle(cols[3].Substring(loc1 + 1, loc2 - loc1 - 1));
                }
                catch
                {
                    esd.Price = 0;
                }
                

                //Get dollar change
                try
                {
                    loc1 = cols[4].IndexOf("span class");
                    loc1 = cols[4].IndexOf(">", loc1 + 1);
                    loc2 = cols[4].IndexOf("<", loc1 + 1);
                    esd.DollarChange = Convert.ToSingle(cols[4].Substring(loc1 + 1, loc2 - loc1 - 1).Replace("+", ""));
                }
                catch
                {
                    esd.DollarChange = 0;
                }
                
                //Get Percent Change
                try
                {
                    loc1 = cols[5].IndexOf("span class");
                    loc1 = cols[5].IndexOf(">", loc1 + 1);
                    loc2 = cols[5].IndexOf("<", loc1 + 1);
                    string PercentChange = cols[5].Substring(loc1 + 1, loc2 - loc1 - 1).Replace("+", "").Replace("%", "");
                    float percent_change_perc = Convert.ToSingle(PercentChange);
                    percent_change_perc = percent_change_perc / 100;
                    esd.PercentChange = percent_change_perc;
                }
                catch
                {
                    esd.PercentChange = 0;
                }
                

                //Get volume
                try
                {
                    loc1 = cols[6].IndexOf("span class");
                    loc1 = cols[6].IndexOf(">", loc1 + 1);
                    loc2 = cols[6].IndexOf("<", loc1 + 1);
                    string vol = cols[6].Substring(loc1 + 1, loc2 - loc1 - 1);
                    esd.Volume = Convert.ToInt32(YahooFinanceToolkit.GetMarketCapFromString(vol));
                }
                catch
                {
                    esd.Volume = 0;
                }
                

                //Get average volume
                try
                {
                    loc1 = cols[7].IndexOf("react-text");
                    loc1 = cols[7].IndexOf(">", loc1 + 1);
                    loc2 = cols[7].IndexOf("<", loc1 + 1);
                    string avgvol = cols[7].Substring(loc1 + 1, loc2 - loc1 - 1);
                    esd.AverageVolume = Convert.ToInt32(YahooFinanceToolkit.GetMarketCapFromString(avgvol));
                }
                catch
                {
                    esd.AverageVolume = 0;
                }
                
                //Get market cap
                try
                {
                    loc1 = cols[8].IndexOf("span class");
                    loc1 = cols[8].IndexOf(">", loc1 + 1);
                    loc2 = cols[8].IndexOf("<", loc1 + 1);
                    string marcap = cols[8].Substring(loc1 + 1, loc2 - loc1 - 1);
                    esd.MarketCap = YahooFinanceToolkit.GetMarketCapFromString(marcap);
                }
                catch
                {
                    esd.MarketCap = 0;
                }
                

                //Get price earnings ratio
                try
                {
                    loc1 = cols[9].IndexOf("react-text");
                    loc1 = cols[9].IndexOf(">", loc1 + 1);
                    loc2 = cols[9].IndexOf("<", loc1 + 1);
                    string pestr = cols[9].Substring(loc1 + 1, loc2 - loc1 - 1);
                    esd.PriceEarningsRatio = Convert.ToSingle(cols[9].Substring(loc1 + 1, loc2 - loc1 - 1));
                }
                catch
                {
                    esd.PriceEarningsRatio = null;
                }
                
                

                ToReturn.Add(esd);
            }


            return ToReturn.ToArray();
        }
    }
}