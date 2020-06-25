using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;   

namespace Yahoo.Finance
{
    public class EquityStatisticalData : EquityData
    {
        
        //Fiscal Year
        public DateTime? FiscalYearEnds {get; set;}
        public DateTime? MostRecentQuarter {get; set;}

        //Profitability
        public float? ProfitMargin {get; set;}
        public float? OperatingMargin {get; set;}

        //Management Affectiveness
        public float? ReturnOnAssets {get; set;}
        public float? ReturnOnEquity {get; set;}

        //Income statement
        public long? Revenue {get; set;}
        public float? RevenuePerShare {get; set;}
        public float? QuarterlyRevenueGrowth {get; set;}
        public long? GrossProfit {get; set;}
        public long? EBITDA {get; set;}
        public long? NetIncomeAvailableToCommon {get; set;}
        public float? DilutedEps {get; set;}
        public float? QuarterlyEarningsGrowth {get; set;}

        //Balance Sheet
        public long? TotalCash {get; set;}
        public float? TotalCashPerShare {get; set;}
        public long? TotalDebt {get; set;}
        public float? TotalDebtEquityRatio {get; set;}
        public float? CurrentRatio {get; set;}
        public float? BookValuePerShare {get; set;}

        //Cash Flow Statement
        public long? OperatingCashFlow {get; set;}
        public long? LeveredFreeCashFlow {get; set;}

        //Stock price information
        public float? Beta {get; set;}
        public float? YearChange {get; set;}
        public float? SP500YearChange {get; set;}
        public float? YearHigh {get; set;}
        public float? YearLow {get; set;}
        public float? MovingAverage50Day {get; set;}
        public float? MovingAverage200Day {get; set;}

        //Share statistics
        public long? AverageVolume3Month {get; set;}
        public long? AverageVolume10Day {get; set;}
        public long? SharesOutstanding {get; set;}
        public long? Float {get; set;}
        public float? PercentHeldByInsiders {get; set;}
        public float? PercentHeldByInstitutions {get; set;}
        public long? SharesShort {get; set;}
        public float? ShortRatio {get; set;}
        public float? ShortPercentOfFloat {get; set;}
        public float? ShortPercentOfSharesOutstanding {get; set;}

        //Dividends & Splits
        public float? ForwardAnnualDividend {get; set;}
        public float? ForwardAnnualDividendYield {get; set;}
        public float? TrailingAnnualDividend {get; set;}
        public float? TrailingAnnualDividendYield {get; set;}
        public float? FiveYearAverageDividendYield {get; set;}
        public float DividendPayoutRatio {get; set;}
        public DateTime? DividendDate {get; set;}
        public DateTime? ExDividendDate {get; set;}
        public string LastSplitFactor {get; set;}
        public DateTime? LastSplitDate {get; set;}


        public async static Task<EquityStatisticalData> CreateAsync(string symbol)
        {
            EquityStatisticalData ToReturn = new EquityStatisticalData();
            
            //https://finance.yahoo.com/quote/GOOGL/key-statistics?p=GOOGL
            string url = "https://finance.yahoo.com/quote/" + symbol.Trim().ToLower() + "/key-statistics?p=" + symbol.Trim().ToLower();
            HttpClient hc = new HttpClient();
            HttpResponseMessage hrm = await hc.GetAsync(url);
            string web = await hrm.Content.ReadAsStringAsync();

            List<string> Splitter = new List<string>();
            Splitter.Add("No results for ");
            string[] parts = web.Split(Splitter.ToArray(), StringSplitOptions.None);
            if (parts.Length > 2)
            {
                throw new Exception("Symbol '" + symbol.ToUpper().Trim() + "' is invalid.");
            }

            //Fiscal year ends
            try
            {
                ToReturn.FiscalYearEnds = DateTime.Parse(ToReturn.GetValueByDisplayName(web, "Fiscal Year Ends"));
            }
            catch
            {
                ToReturn.FiscalYearEnds = null;
            }
            

            //Most recent quarter
            try
            {
                ToReturn.MostRecentQuarter = DateTime.Parse(ToReturn.GetValueByDisplayName(web, "Most Recent Quarter"));
            }
            catch
            {
                ToReturn.MostRecentQuarter = null;
            }
            

            //Profit Margin
            try
            {
                string ProfitMarginString = ToReturn.GetValueByDisplayName(web, "Profit Margin");
                ToReturn.ProfitMargin = ToReturn.PercentStringToPercentFloat(ProfitMarginString);
            }
            catch
            {
                ToReturn.ProfitMargin = null;
            }
            

            //Operating Margin
            try
            {
                ToReturn.OperatingMargin = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Operating Margin"));
            }
            catch
            {
                ToReturn.OperatingMargin = null;
            }
            

            //ROA
            try
            {
                ToReturn.ReturnOnAssets = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Return on Assets"));
            }
            catch
            {
                ToReturn.ReturnOnAssets = null;
            }
            

            //ROE
            try
            {
                ToReturn.ReturnOnEquity = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Return on Equity"));
            }
            catch
            {
                ToReturn.ReturnOnEquity = null;
            }
            

            //Revenue
            try
            {
                ToReturn.Revenue = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Revenue"));
            }
            catch
            {
                ToReturn.Revenue = null;
            }
            

            //Revenue per share
            try
            {
                ToReturn.RevenuePerShare = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Revenue Per Share"));
            }
            catch
            {
                ToReturn.RevenuePerShare = null;
            }
            

            //Quarterly Revenue Growth
            try
            {
                ToReturn.QuarterlyRevenueGrowth = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Quarterly Revenue Growth"));
            }
            catch
            {
                ToReturn.QuarterlyRevenueGrowth = null;
            }
            

            //Gross profit
            try
            {
                ToReturn.GrossProfit = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Gross Profit"));
            }
            catch
            {
                ToReturn.GrossProfit = null;
            }
            

            //EBITDA
            try
            {
                ToReturn.EBITDA = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "EBITDA"));
            }
            catch
            {
                ToReturn.EBITDA = null;
            }
            
            //Net income available to common shareholders
            try
            {
                ToReturn.NetIncomeAvailableToCommon = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Net Income Avi to Common"));
            }
            catch
            {
                ToReturn.NetIncomeAvailableToCommon = null;
            }
            

            //Diluted EPS
            try
            {
                ToReturn.DilutedEps = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Diluted EPS"));
            }
            catch
            {
                ToReturn.DilutedEps = null;
            }
            

            //Quarterly Earnings Growth
            try
            {
                ToReturn.QuarterlyEarningsGrowth = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Quarterly Earnings Growth"));
            }
            catch
            {
                ToReturn.QuarterlyEarningsGrowth = null;
            }
            

            

            //Total Cash
            try
            {
                ToReturn.TotalCash = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Total Cash"));
            }
            catch
            {
                ToReturn.TotalCash = null;
            }
            

            //Total Cash Per Share
            try
            {
                ToReturn.TotalCashPerShare = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Total Cash Per Share"));
            }
            catch
            {
                ToReturn.TotalCashPerShare = null;
            }
            

            //Total Debt
            try
            {
                ToReturn.TotalDebt = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Total Debt"));
            }
            catch
            {
                ToReturn.TotalDebt = null;
            }
            

            //Debt Equity Ratio
            try
            {
                ToReturn.TotalDebtEquityRatio = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Total Debt/Equity"));
            }
            catch
            {
                ToReturn.TotalDebtEquityRatio = null;
            }
            

            //Current Ratio
            try
            {
                ToReturn.CurrentRatio = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Current Ratio"));
            }
            catch
            {
                ToReturn.CurrentRatio = null;
            }
            

            //Book value per share
            try
            {
                ToReturn.BookValuePerShare = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Book Value Per Share"));
            }
            catch
            {
                ToReturn.BookValuePerShare = null;
            }
            

            

            //Operating Cash Flow
            try
            {
                ToReturn.OperatingCashFlow = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Operating Cash Flow"));
            }
            catch
            {
                ToReturn.OperatingCashFlow = null;
            }
            

            //Levered Free Cash Flow
            try
            {
                ToReturn.LeveredFreeCashFlow = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Levered Free Cash Flow"));
            }
            catch
            {
                ToReturn.LeveredFreeCashFlow = null;
            }
            

            //Beta
            try
            {
                ToReturn.Beta = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Beta (5Y Monthly)"));
            }
            catch
            {
                ToReturn.Beta = null;
            }
            

            //Year change
            try
            {
                ToReturn.YearChange = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "52-Week Change"));
            }
            catch
            {
                ToReturn.YearChange = null;
            }
            

            //S&P500 Year Change
            try
            {
                ToReturn.SP500YearChange = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "S&amp;P500 52-Week Change"));
            }
            catch
            {
                ToReturn.SP500YearChange = null;
            }
            

            //Year high
            try
            {
                ToReturn.YearHigh = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "52 Week High"));
            }
            catch
            {
                ToReturn.YearHigh = null;
            }
            

            //Year low
            try
            {
                ToReturn.YearLow = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "52 Week Low"));
            }
            catch
            {
                ToReturn.YearLow = null;
            }
            

            //50-Day moving average
            try
            {
                ToReturn.MovingAverage50Day = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "50-Day Moving Average"));
            }
            catch
            {
                ToReturn.MovingAverage50Day = null;
            }
            

            //200-Day moving average
            try
            {
                ToReturn.MovingAverage200Day = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "200-Day Moving Average"));
            }
            catch
            {
                ToReturn.MovingAverage200Day = null;
            }
            

            //Avg Volume (3 month)
            try
            {
                ToReturn.AverageVolume3Month = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Avg Vol (3 month)"));
            }
            catch
            {
                ToReturn.AverageVolume3Month = null;
            }
            

            //Avg Volume (10 day)
            try
            {
                ToReturn.AverageVolume10Day = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Avg Vol (10 day)"));
            }
            catch
            {
                ToReturn.AverageVolume10Day = null;
            }
            

            //Shares outstanding
            try
            {
                ToReturn.SharesOutstanding = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Shares Outstanding"));
            }
            catch
            {
                ToReturn.SharesOutstanding = null;
            }
            

            //Float
            try
            {
                ToReturn.Float = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayName(web, "Float"));
            }
            catch
            {
                ToReturn.Float = null;
            }
            

            //Percent held by insiders
            try
            {
                ToReturn.PercentHeldByInsiders = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "% Held by Insiders"));
            }
            catch
            {
                ToReturn.PercentHeldByInsiders = null;
            }
            

            //Percent held by institutions
            try
            {
                ToReturn.PercentHeldByInstitutions = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "% Held by Institutions"));
            }
            catch
            {
                ToReturn.PercentHeldByInstitutions = null;
            }
            
            

            //Shares short
            try
            {
                ToReturn.SharesShort = ToReturn.LongStringToLong(ToReturn.GetValueByDisplayNameLead(web, "Shares Short"));
            }
            catch
            {
                ToReturn.SharesShort = null;
            }
            

            //Short Ratio
            try
            {
                ToReturn.ShortRatio = Convert.ToSingle(ToReturn.GetValueByDisplayNameLead(web, "Short Ratio"));
            }
            catch
            {
                ToReturn.ShortRatio = null;
            }
            
            //Short % of Float
            try
            {
                ToReturn.ShortPercentOfFloat = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayNameLead(web, "Short % of Float"));
            }
            catch
            {
                ToReturn.ShortPercentOfSharesOutstanding = null;
            }
            

            //Short % of Shares Outstanding
            try
            {
                ToReturn.ShortPercentOfSharesOutstanding = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayNameLead(web, "Short % of Shares Outstanding"));
            }
            catch
            {
                ToReturn.ShortPercentOfSharesOutstanding = null;
            }
            

            //Forward Annual Dividend
            try
            {
                ToReturn.ForwardAnnualDividend = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Forward Annual Dividend Rate"));
            }
            catch
            {
                ToReturn.ForwardAnnualDividend = null;
            }

            //Forward Annual Dividend Yield
            try
            {
                ToReturn.ForwardAnnualDividendYield = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Forward Annual Dividend Yield"));
            }
            catch
            {
                ToReturn.ForwardAnnualDividendYield = null;
            }

            //Trailing Annual Dividend Rate
            try
            {
                ToReturn.TrailingAnnualDividend = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "Trailing Annual Dividend Rate"));
            }
            catch
            {
                ToReturn.TrailingAnnualDividend = null;
            }

            //Trailing Annual Dividend Yield
            try
            {
                ToReturn.TrailingAnnualDividendYield = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Trailing Annual Dividend Yield"));
            }
            catch
            {
                ToReturn.TrailingAnnualDividendYield = null;
            }

            //5 Year Average Dividend Yield
            try
            {
                ToReturn.FiveYearAverageDividendYield = Convert.ToSingle(ToReturn.GetValueByDisplayName(web, "5 Year Average Dividend Yield"));
            }
            catch
            {
                ToReturn.FiveYearAverageDividendYield = null;
            }

            //Payout ratio
            try
            {
                ToReturn.DividendPayoutRatio = ToReturn.PercentStringToPercentFloat(ToReturn.GetValueByDisplayName(web, "Payout Ratio"));
            }
            catch
            {
                ToReturn.DividendPayoutRatio = 0f;
            }

            //Dividend Date
            try
            {
                ToReturn.DividendDate = DateTime.Parse(ToReturn.GetValueByDisplayName(web, "Dividend Date"));
            }
            catch
            {
                ToReturn.DividendDate = null;
            }

            //Ex-Dividend Date
            try
            {
                ToReturn.ExDividendDate = DateTime.Parse(ToReturn.GetValueByDisplayName(web, "Ex-Dividend Date"));
            }
            catch
            {

            }

            //Last split factor
            try
            {
                ToReturn.LastSplitFactor = ToReturn.GetValueByDisplayName(web, "Last Split Factor");
            }
            catch
            {
                ToReturn.LastSplitFactor = null;
            }

            //Last split Date
            try
            {
                ToReturn.LastSplitDate = DateTime.Parse(ToReturn.GetValueByDisplayName(web, "Last Split Date"));
            }
            catch
            {
                ToReturn.LastSplitDate = null;
            }

            return ToReturn;
        }

        private string GetValueByDisplayName(string web, string display_name)
        {
            int loc1 = web.IndexOf(">" + display_name + "<");
            if (loc1 == -1)
            {
                throw new Exception("Unable to find '" + display_name + "' in supplied source.");
            }
            loc1 = web.IndexOf("<td class=", loc1 + 1);
            loc1 = web.IndexOf(">", loc1 + 1);
            int loc2 = web.IndexOf("<", loc1 + 1);
            string data = web.Substring(loc1 + 1, loc2 - loc1 - 1);
            return data;
        }

        private float PercentStringToPercentFloat(string percent_string)
        {
            float p = Convert.ToSingle(percent_string.Replace("%", "")) / 100f;
            return p;
        }

        private long LongStringToLong(string long_string)
        {
            float ValPortion = 0;
            try
            {
                ValPortion = Convert.ToSingle(long_string.Substring(0, long_string.Length-1));
            }
            catch
            {
                throw new Exception("Unable to convert value '" + long_string + "' to a long value (using LongStringToLong method).");
            }


            string last_char = long_string.Substring(long_string.Length-1,1).ToLower().Trim();
            long multiplier = 0;
            if (last_char == "h")
            {
                multiplier = 1000;
            }
            else if (last_char == "m")
            {
                multiplier = 1000000;
            }
            else if (last_char == "b")
            {
                multiplier = 1000000000;
            }
            else if (last_char == "t")
            {
                multiplier = 1000000000000;
            }
            
            long ToReturn = Convert.ToInt64(ValPortion * multiplier);
            return ToReturn;
        }

        private string GetValueByDisplayNameLead(string web, string display_name_lead)
        {
            int loc1 = web.IndexOf(">" + display_name_lead);
            if (loc1 == -1)
            {
                throw new Exception("Unable to find '" + display_name_lead + "' in supplied source.");
            }
            loc1 = web.IndexOf("<td class=", loc1 + 1);
            loc1 = web.IndexOf(">", loc1 + 1);
            int loc2 = web.IndexOf("<", loc1 + 1);
            string data = web.Substring(loc1 + 1, loc2 - loc1 - 1);
            return data;
        }
    
    }
}