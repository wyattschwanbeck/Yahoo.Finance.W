using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace Yahoo.Finance
{

    public class EquitySummaryData : EquityData
    {             
        public string Name { get; set; }
        public string StockSymbol { get; set; }
        public float Price { get; set; }
        public float DollarChange { get; set; }
        public float PercentChange { get; set; }
        public float PreviousClose { get; set; }
        public float Open { get; set; }
        public float BidPrice { get; set; }
        public int BidQuantity { get; set; }
        public float AskPrice { get; set; }
        public int AskQuantity { get; set; }
        public float DayRangeLow { get; set; }
        public float DayRangeHigh { get; set; }
        public float YearRangeLow { get; set; }
        public float YearRangeHigh { get; set; }
        public long Volume { get; set; }
        public long AverageVolume { get; set; }
        public double MarketCap { get; set; }
        public float? Beta { get; set; }
        public float? PriceEarningsRatio { get; set; }
        public float EarningsPerShare { get; set; }
        public DateTime? EarningsDate { get; set; }
        public float? ForwardDividend { get; set; }
        public float? ForwardDividendYield { get; set; }
        public DateTime? ExDividendDate { get; set; }
        public float YearTargetEstimate { get; set; }


        public static async Task<EquitySummaryData> CreateAsync(string symbol)
        {
            EquitySummaryData ToReturn = new EquitySummaryData();
            ToReturn.QueriedSymbol = symbol.ToUpper().Trim();

            //Get from yahoo finance
            string url = "https://finance.yahoo.com/quote/" + symbol;
            HttpClient cl = new HttpClient();
            HttpResponseMessage hrm = await cl.GetAsync(url);
            string web = await hrm.Content.ReadAsStringAsync();


            //Test for invalid
            if (web.ToLower().Contains(">Please check your spelling.".ToLower()))
            {
                throw new Exception("Stock '" + symbol + "' is not a valid stock symbol.");
            }


            int loc1;
            int loc2;
            List<string> splitter = new List<string>();

            //The name and price were left outside of a try bracket intentionally.
            //This should be the minimum amount of information that can be accessed.  If you can't get this, fail.

            //Get name and stock symbol
            string name_and_stock_symbol = ToReturn.GetDataByClassName(web, "D(ib) Fz(18px)");
            loc1 = name_and_stock_symbol.IndexOf("(");
            ToReturn.Name = name_and_stock_symbol.Substring(0, loc1 - 1).Trim();
            ToReturn.Name = ToReturn.Name.Replace("&amp;", "&");
            ToReturn.Name = ToReturn.Name.Replace("&#x27;", "'");
            loc2 = name_and_stock_symbol.IndexOf(")", loc1 + 1);
            ToReturn.StockSymbol = name_and_stock_symbol.Substring(loc1 + 1, loc2 - loc1 - 1);
            

            


            //Get price
            ToReturn.Price = System.Convert.ToSingle(ToReturn.GetDataByClassName(web, "Fw(b) Fz(36px) Mb(-4px) D(ib)"));

            
            //Get day dollar change
            try
            {
                loc1 = web.IndexOf("qsp-price-change");
                loc1 = web.IndexOf("<span class", loc1 + 1);
                loc1 = web.IndexOf(">", loc1 + 1);
                loc2 = web.IndexOf("<", loc1 + 1);
                string dayc = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                ToReturn.DollarChange = System.Convert.ToSingle(dayc.Replace("+", ""));
            }
            catch
            {
                ToReturn.DollarChange = 0;
            }

            //Get day percent change
            try
            {
                loc1 = web.LastIndexOf("data-field=\"regularMarketChangePercent\" data-trend=\"txt\"");
                loc1 = web.IndexOf("<span class", loc1 + 1);
                loc1 = web.IndexOf(">", loc1 + 1);
                loc2 = web.IndexOf("<", loc1 + 1);
                string dayc = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                dayc = dayc.Replace("+","");
                dayc = dayc.Replace("(", "");
                dayc = dayc.Replace(")", "");
                dayc = dayc.Replace("%", "");
                float val = System.Convert.ToSingle(dayc);
                ToReturn.PercentChange = val / 100f;
            }
            catch
            {
                ToReturn.PercentChange = 0f;
            }
            
            //Get previous close
            try
            {
                ToReturn.PreviousClose = System.Convert.ToSingle(ToReturn.GetDataByDataTestName(web, "PREV_CLOSE-value"));
            }
            catch
            {
                ToReturn.PreviousClose = 0;
            }
            
            
            //Get open
            try
            {
                ToReturn.Open = System.Convert.ToSingle(ToReturn.GetDataByDataTestName(web, "OPEN-value"));
            }
            catch
            {
                ToReturn.Open = 0;
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
                ToReturn.BidPrice = System.Convert.ToSingle(parts[0]);
                ToReturn.BidQuantity = System.Convert.ToInt32(parts[1]);
            }
            catch
            {
                ToReturn.BidPrice = 0;
                ToReturn.BidQuantity = 0;
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
                ToReturn.AskPrice = System.Convert.ToSingle(parts2[0]);
                ToReturn.AskQuantity = System.Convert.ToInt32(parts2[1]);
            }
            catch
            {
                ToReturn.AskPrice = 0;
                ToReturn.AskQuantity = 0;
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
                ToReturn.DayRangeLow = System.Convert.ToSingle(parts3[0].Trim());
                ToReturn.DayRangeHigh = System.Convert.ToSingle(parts3[1].Trim());
            }
            catch
            {
                ToReturn.DayRangeLow = 0;
                ToReturn.DayRangeHigh = 0;
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
                ToReturn.YearRangeLow = System.Convert.ToSingle(parts4[0].Trim());
                ToReturn.YearRangeHigh = System.Convert.ToSingle(parts4[1].Trim());
            }
            catch
            {
                ToReturn.YearRangeLow = 0;
                ToReturn.YearRangeHigh = 0;
            }
            

            

            //Get volume
            try
            {
                ToReturn.Volume = System.Convert.ToInt64(ToReturn.GetDataByDataFieldName(web, "regularMarketVolume").Replace(",",""));
            }
            catch
            {
                ToReturn.Volume = 0;
            }
            

            //Get average volume
            try
            {
                ToReturn.AverageVolume = System.Convert.ToInt64(ToReturn.GetDataByDataTestName(web, "AVERAGE_VOLUME_3MONTH-value").Replace(",", ""));

            }
            catch
            {
                ToReturn.AverageVolume = 0;
            }

            //Get market cap
            try
            {
                string market_cap = ToReturn.GetDataByDataTestName(web, "MARKET_CAP-value");
                string last_char = market_cap.Substring(market_cap.Length - 1, 1).ToLower();
                float mcf = System.Convert.ToSingle(market_cap.Substring(0, market_cap.Length - 1));
                if (last_char == "th")
                {
                    ToReturn.MarketCap = System.Convert.ToDouble(mcf * 1000);
                }
                else if (last_char == "m")
                {
                    ToReturn.MarketCap = System.Convert.ToDouble(mcf * 1000000);
                }
                else if (last_char == "b")
                {
                    ToReturn.MarketCap = System.Convert.ToDouble(mcf * 1000000000);
                }
                else if (last_char == "t")
                {
                    ToReturn.MarketCap = System.Convert.ToDouble(mcf * 1000000000000);
                }
            }
            catch
            {
                ToReturn.MarketCap = 0;
            }

            

            //Get beta
            try
            {
                ToReturn.Beta = System.Convert.ToSingle(ToReturn.GetDataByDataTestName(web, "BETA_5Y-value"));
            }
            catch
            {
                ToReturn.Beta = null;
            }

            //Get PE Ratio
            try
            {
                ToReturn.PriceEarningsRatio = System.Convert.ToSingle(ToReturn.GetDataByDataTestName(web, "PE_RATIO-value"));
            }
            catch
            {
                ToReturn.PriceEarningsRatio = null;
            }

            //Get EPS ratio
            try
            {
                ToReturn.EarningsPerShare = System.Convert.ToSingle(ToReturn.GetDataByDataTestName(web, "EPS_RATIO-value"));
            }
            catch
            {
                ToReturn.EarningsPerShare = 0;
            }


            //Get earnings date
            try
            {
                loc1 = web.IndexOf("EARNINGS_DATE-value");
                loc1 = web.IndexOf("<span", loc1 + 1);
                loc1 = web.IndexOf(">", loc1 + 1);
                loc2 = web.IndexOf("<", loc1 + 1);
                string datestr = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                ToReturn.EarningsDate = DateTime.Parse(datestr);
            }
            catch
            {
                ToReturn.EarningsDate = null;
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
                    ToReturn.ForwardDividend = null;
                    ToReturn.ForwardDividendYield = null;
                }
                else
                {
                    splitter.Clear();
                    splitter.Add(" ");
                    string[] partsfd = dividendinfo.Split(splitter.ToArray(), StringSplitOptions.None);
                    ToReturn.ForwardDividend = System.Convert.ToSingle(partsfd[0]);
                    ToReturn.ForwardDividendYield = System.Convert.ToSingle(partsfd[1].Replace("%", "").Replace("(", "").Replace(")", "")) / 100;
                }
            }
            catch
            {
                ToReturn.ForwardDividend = 0;
                ToReturn.ForwardDividendYield = 0;
            }

            

            //Get ex dividend
            try
            {
                loc1 = web.IndexOf("EX_DIVIDEND_DATE-value");
                loc1 = web.IndexOf("<span", loc1 + 1);
                loc1 = web.IndexOf(">", loc1 + 1);
                loc2 = web.IndexOf("<", loc1 + 1);
                string datestr = web.Substring(loc1 + 1, loc2 - loc1 - 1);
                ToReturn.ExDividendDate = DateTime.Parse(datestr);
            }
            catch
            {
                ToReturn.ExDividendDate= null;
            }

            //Get one year targets
            try
            {
                ToReturn.YearTargetEstimate = System.Convert.ToSingle(ToReturn.GetDataByDataTestName(web, "ONE_YEAR_TARGET_PRICE-value"));
            }
            catch
            {
                ToReturn.YearTargetEstimate = 0;
            }

            return ToReturn;
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

            loc1 = web_data.IndexOf(">", loc1 + 1);
            loc2 = web_data.IndexOf("<", loc1 + 1);

            string middle = web_data.Substring(loc1 + 1, loc2 - loc1 - 1);
            return middle;
        }

        private string GetDataByDataFieldName(string web_data, string data_field)
        {
            int loc1 = web_data.IndexOf("data-field=\"" + data_field + "\"");
            if (loc1 == -1)
            {
                throw new Exception("Unable to find data field '" + data_field + "' in web content");
            }
            loc1 = web_data.IndexOf(">", loc1 + 1);
            int loc2 = web_data.IndexOf("<", loc1 + 1);
            string ToReturn = web_data.Substring(loc1 + 1, loc2 - loc1 - 1);
            return ToReturn;
        }

    }


    

}
