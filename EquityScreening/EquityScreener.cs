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
                loc1 = cols[1].IndexOf("a href");
                loc1 = cols[1].IndexOf(">", loc1 + 1);
                loc2 = cols[1].IndexOf("<", loc1 + 1);
                esd.StockSymbol = cols[1].Substring(loc1 + 1, loc2 - loc1 - 1);

                //Get name
                loc1 = cols[2].IndexOf("react-text");
                loc1 = cols[2].IndexOf(">", loc1 + 1);
                loc2 = cols[2].IndexOf("<", loc1 + 1);
                esd.Name = cols[2].Substring(loc1 + 1, loc2 - loc1 - 1);

                ToReturn.Add(esd);
            }


            return ToReturn.ToArray();
        }
    }
}