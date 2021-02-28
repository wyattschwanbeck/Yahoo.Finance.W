using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace Yahoo.Finance
{
    public class Equity
    {
        public string Symbol { get; set; }
        public EquitySummaryData Summary { get; set; }
        public EquityStatisticalData Statistics {get; set;}

        public static Equity Create(string stock_symbol)
        {
            Equity e = new Equity();
            e.Symbol = stock_symbol;
            return e;
        }

        public async Task DownloadSummaryAsync()
        {
            if (Symbol == "")
            {
                throw new Exception("Stock symbol not provided.");
            }

            try
            {
                Summary = await EquitySummaryData.CreateAsync(Symbol);
            }
            catch
            {
                throw new Exception("Fatal error while downloading summary data.");
            }

        }

        public async Task DownloadStatisticsAsync()
        {
            if (Symbol == "")
            {
                throw new Exception("Stock symbol not provided.");
            }
            try
            {
                Statistics = await EquityStatisticalData.CreateAsync(Symbol);
            }
            catch
            {
                throw new Exception("Fatal error while downloading statistic data.");
            }
        }


        private string GetDataByClassName(string web_data, string class_name)
        {
            int loc1 = web_data.IndexOf("class=\"" + class_name + "\"");
            if (loc1 == -1)
            {
                throw new Exception("Unable to find class with name '" + class_name + "' in the web data.");
            }
            loc1 = web_data.IndexOf(">", loc1 + 1);
            int loc2 = web_data.IndexOf("<", loc1 + 1);
            string middle = web_data.Substring(loc1 + 1, loc2 - loc1 - 1);
            return middle;
        }

        private string GetDataByDataTestName(string web_data, string data_test_name)
        {
            int loc1 = 0;
            int loc2 = 0;

            loc1 = web_data.IndexOf("data-test=\"" + data_test_name + "\"");
            if (loc1 == -1)
            {
                throw new Exception("Unable to find data with data test name '" + web_data + "' inside web data.");
            }

            loc1 = web_data.IndexOf("<span", loc1 + 1);
            loc1 = web_data.IndexOf(">", loc1 + 1);
            loc2 = web_data.IndexOf("<", loc1 + 1);

            string middle = web_data.Substring(loc1 + 1, loc2 - loc1 - 1);
            return middle;
        }

    }
}