using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace Yahoo.Finance
{
    public class Equity
    {
        public string StockSymbol { get; set; }
        public EquitySummaryData Summary { get; set; }

        public static Equity Create(string stock_symbol)
        {
            Equity e = new Equity();
            e.StockSymbol = stock_symbol;
            return e;
        }

        public async Task DownloadSummaryAsync()
        {
            //Error testing
            if (StockSymbol == "")
            {
                throw new Exception("Stock symbol not provided.");
            }

            //Create the class
            Summary = new EquitySummaryData();

            //Get from yahoo finance
            string url = "https://finance.yahoo.com/quote/" + StockSymbol;
            HttpClient cl = new HttpClient();
            HttpResponseMessage hrm = await cl.GetAsync(url);
            string web = await hrm.Content.ReadAsStringAsync();


            //Test for invalid
            if (web.ToLower().Contains(">Please check your spelling.".ToLower()))
            {
                throw new Exception("Stock '" + StockSymbol + "' is not a valid stock symbol.");
            }


            int loc1;
            int loc2;
            List<string> splitter = new List<string>();

            //Get data collected time
            Summary.DataCollectedOn = DateTimeOffset.Now;

            //The name and price were left outside of a try bracket intentionally.
            //This should be the minimum amount of information that can be accessed.  If you can't get this, fail.

            //Get name and stock symbol
            string name_and_stock_symbol = GetDataByClassName(web, "D(ib) Fz(18px)");
            loc1 = name_and_stock_symbol.IndexOf("(");
            Summary.Name = name_and_stock_symbol.Substring(0, loc1 - 1).Trim();
            Summary.Name = Summary.Name.Replace("&amp;", "&");
            Summary.Name = Summary.Name.Replace("&#x27;", "'");
            loc2 = name_and_stock_symbol.IndexOf(")", loc1 + 1);
            Summary.StockSymbol = name_and_stock_symbol.Substring(loc1 + 1, loc2 - loc1 - 1);
            

            


            //Get price
            Summary.Price = System.Convert.ToSingle(GetDataByClassName(web, "Trsdu(0.3s) Fw(b) Fz(36px) Mb(-4px) D(ib)"));

            
            //Get day change
            try
            {
                loc1 = web.IndexOf("Trsdu(0.3s) Fw(500) Pstart(10px) Fz(24px)");
                loc1 = web.IndexOf(">", loc1 + 1);
                loc2 = web.IndexOf("<", loc1 + 1);
                string dayc = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                splitter.Clear();
                splitter.Add(" ");
                string[] partsdc = dayc.Split(splitter.ToArray(), StringSplitOptions.None);
                Summary.DollarChange = System.Convert.ToSingle(partsdc[0].Replace("+", ""));
                Summary.PercentChange = System.Convert.ToSingle(partsdc[1].Replace("+", "").Replace("(","").Replace(")", "").Replace("%","").Trim())/100;
            }
            catch
            {
                Summary.DollarChange = 0;
                Summary.PercentChange = 0;
            }
            
            //Get previous close
            try
            {
                Summary.PreviousClose = System.Convert.ToSingle(GetDataByDataTestName(web, "PREV_CLOSE-value"));
            }
            catch
            {
                Summary.PreviousClose = 0;
            }
            
            
            //Get open
            try
            {
                Summary.Open = System.Convert.ToSingle(GetDataByDataTestName(web, "OPEN-value"));
            }
            catch
            {
                Summary.Open = 0;
            }
            

            

            //Get bid information
            try
            {
                loc1 = 0;
                loc2 = 0;
                loc1 = web.IndexOf("BID-value");
                loc1 = web.IndexOf(">", loc1 + 1);
                loc2 = web.IndexOf("<", loc1 + 1);
                string bid_info = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                if (bid_info == "")
                {
                    loc1 = web.IndexOf(">", loc2 + 1);
                    loc2 = web.IndexOf("<", loc1 + 1);
                    bid_info = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                }
                splitter.Clear();
                splitter.Add(" x ");
                string[] parts = bid_info.Split(splitter.ToArray(), StringSplitOptions.None);
                Summary.BidPrice = System.Convert.ToSingle(parts[0]);
                Summary.BidQuantity = System.Convert.ToInt32(parts[1]);
            }
            catch
            {
                Summary.BidPrice = 0;
                Summary.BidQuantity = 0;
            }


            

            //Get ask informmation
            try
            {
                loc1 = 0;
                loc2 = 0;
                loc1 = web.IndexOf("ASK-value");
                loc1 = web.IndexOf(">", loc1 + 1);
                loc2 = web.IndexOf("<", loc1 + 1);
                string ask_info = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                if (ask_info == "")
                {
                    loc1 = web.IndexOf(">", loc2 + 1);
                    loc2 = web.IndexOf("<", loc1 + 1);
                    ask_info = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                }
                splitter.Clear();
                splitter.Add(" x ");
                string[] parts2 = ask_info.Split(splitter.ToArray(), StringSplitOptions.None);
                Summary.AskPrice = System.Convert.ToSingle(parts2[0]);
                Summary.AskQuantity = System.Convert.ToInt32(parts2[1]);
            }
            catch
            {
                Summary.AskPrice = 0;
                Summary.AskQuantity = 0;
            }

            

            //Get Day Range information
            try
            {
                loc1 = web.IndexOf("DAYS_RANGE-value");
                loc1 = web.IndexOf(">", loc1 + 1);
                loc2 = web.IndexOf("<", loc1 + 1);
                string day_range_info = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                day_range_info = day_range_info.Replace(" ", "");
                splitter.Clear();
                splitter.Add("-");
                string[] parts3 = day_range_info.Split(splitter.ToArray(), StringSplitOptions.None);
                Summary.DayRangeLow = System.Convert.ToSingle(parts3[0].Trim());
                Summary.DayRangeHigh = System.Convert.ToSingle(parts3[1].Trim());
            }
            catch
            {
                Summary.DayRangeLow = 0;
                Summary.DayRangeHigh = 0;
            }
            

            //Get Day Range information
            try
            {
                loc1 = web.IndexOf("FIFTY_TWO_WK_RANGE-value");
                loc1 = web.IndexOf(">", loc1 + 1);
                loc2 = web.IndexOf("<", loc1 + 1);
                string year_range_info = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                year_range_info = year_range_info.Replace(" ", "");
                splitter.Clear();
                splitter.Add("-");
                string[] parts4 = year_range_info.Split(splitter.ToArray(), StringSplitOptions.None);
                Summary.YearRangeLow = System.Convert.ToSingle(parts4[0].Trim());
                Summary.YearRangeHigh = System.Convert.ToSingle(parts4[1].Trim());
            }
            catch
            {
                Summary.YearRangeLow = 0;
                Summary.YearRangeHigh = 0;
            }
            

            

            //Get volume
            try
            {
                Summary.Volume = System.Convert.ToInt32(GetDataByDataTestName(web, "TD_VOLUME-value").Replace(",",""));
            }
            catch
            {
                Summary.Volume = 0;
            }
            

            //Get average volume
            try
            {
                Summary.AverageVolume = System.Convert.ToInt32(GetDataByDataTestName(web, "AVERAGE_VOLUME_3MONTH-value").Replace(",", ""));

            }
            catch
            {
                Summary.AverageVolume = 0;
            }

            //Get market cap
            try
            {
                string market_cap = GetDataByDataTestName(web, "MARKET_CAP-value");
                string last_char = market_cap.Substring(market_cap.Length - 1, 1).ToLower();
                float mcf = System.Convert.ToSingle(market_cap.Substring(0, market_cap.Length - 1));
                if (last_char == "th")
                {
                    Summary.MarketCap = System.Convert.ToDouble(mcf * 1000);
                }
                else if (last_char == "m")
                {
                    Summary.MarketCap = System.Convert.ToDouble(mcf * 1000000);
                }
                else if (last_char == "b")
                {
                    Summary.MarketCap = System.Convert.ToDouble(mcf * 1000000000);
                }
                else if (last_char == "t")
                {
                    Summary.MarketCap = System.Convert.ToDouble(mcf * 1000000000000);
                }
            }
            catch
            {
                Summary.MarketCap = 0;
            }

            

            //Get beta
            try
            {
                Summary.Beta = System.Convert.ToSingle(GetDataByDataTestName(web, "BETA_5Y-value"));
            }
            catch
            {
                Summary.Beta = null;
            }

            //Get PE Ratio
            try
            {
                Summary.PriceEarningsRatio = System.Convert.ToSingle(GetDataByDataTestName(web, "PE_RATIO-value"));
            }
            catch
            {
                Summary.PriceEarningsRatio = null;
            }

            //Get EPS ratio
            try
            {
                Summary.EarningsPerShare = System.Convert.ToSingle(GetDataByDataTestName(web, "EPS_RATIO-value"));
            }
            catch
            {
                Summary.EarningsPerShare = 0;
            }


            //Get earnings date
            try
            {
                Summary.EarningsDate = DateTime.Parse(GetDataByDataTestName(web, "EARNINGS_DATE-value"));
            }
            catch
            {
                Summary.EarningsDate = null;
            }

            

            //Get Forward Dividend Info
            try
            {
                loc1 = web.IndexOf("DIVIDEND_AND_YIELD-value");
                loc1 = web.IndexOf(">", loc1 + 1);
                loc2 = web.IndexOf("<", loc1 + 1);
                string dividendinfo = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                if (dividendinfo == "N/A (N/A)")
                {
                    Summary.ForwardDividend = null;
                    Summary.ForwardDividendYield = null;
                }
                else
                {
                    splitter.Clear();
                    splitter.Add(" ");
                    string[] partsfd = dividendinfo.Split(splitter.ToArray(), StringSplitOptions.None);
                    Summary.ForwardDividend = System.Convert.ToSingle(partsfd[0]);
                    Summary.ForwardDividendYield = System.Convert.ToSingle(partsfd[1].Replace("%", "").Replace("(", "").Replace(")", "")) / 100;
                }
            }
            catch
            {
                Summary.ForwardDividend = 0;
                Summary.ForwardDividendYield = 0;
            }

            

            //Get ex-dividend date
            try
            {
                Summary.ExDividendDate = DateTime.Parse(GetDataByDataTestName(web, "EX_DIVIDEND_DATE-value"));
            }
            catch
            {
                Summary.ExDividendDate = null;
            }

            //Get one year targets
            try
            {
                Summary.YearTargetEstimate = System.Convert.ToSingle(GetDataByDataTestName(web, "ONE_YEAR_TARGET_PRICE-value"));
            }
            catch
            {
                Summary.YearTargetEstimate = 0;
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