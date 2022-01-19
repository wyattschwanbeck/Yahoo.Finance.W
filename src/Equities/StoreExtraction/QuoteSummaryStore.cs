using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace Yahoo.Finance
{
    public class QuoteSummaryStore
    {
        private JObject QuoteSummaryStoreObj; //The JSON object titled "QuoteSummaryStore"

        public QuoteSummaryStore(JObject quote_summary_store_obj)
        {
            QuoteSummaryStoreObj = quote_summary_store_obj;
        }

        public string Symbol
        {
            get
            {
                return QuoteSummaryStoreObj.Property("symbol").Value.ToString();
            }
        }

        #region  "Profile"

        public string BusinessSummary
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryProfile.longBusinessSummary").Value<string>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public int? FullTimeEmployees
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryProfile.fullTimeEmployees").Value<int>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public string Sector
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryProfile.sector").Value<string>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public string Industry
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryProfile.industry").Value<string>();
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region "Summary"

        public float? Open
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.regularMarketOpen.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public int? AverageVolume90Day
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.averageDailyVolume3Month.raw").Value<int>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public string Exchange
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.exchange").Value<string>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? DayHigh
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.regularMarketDayHigh.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public string ShortName
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.shortName").Value<string>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public string LongName
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.longName").Value<string>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? Change
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.regularMarketChange.raw").Value<float>();
                }   
                catch
                {
                    return null;
                }
            }
        }

        public float? PreviousClose
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.regularMarketPreviousClose.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? Price
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.regularMarketPrice.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public string Currency
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.currency").Value<string>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public int? Volume
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.regularMarketVolume.raw").Value<int>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? MarketCap
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.marketCap.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? ChangePercent
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.regularMarketChangePercent.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? DayLow
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("price.regularMarketDayLow.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? BidPrice
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.bid.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public int? BidQuantity
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.bidSize.raw").Value<int>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? AskPrice
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.ask.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public int? AskQuantity
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.askSize.raw").Value<int>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public int? AverageVolume10Day
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.averageDailyVolume10Day.raw").Value<int>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? YearLow
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.fiftyTwoWeekLow.raw").Value<float>();
                }   
                catch
                {
                    return null;
                }
            }
        }

        public float? YearHigh
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.fiftyTwoWeekHigh.raw").Value<float>();
                }   
                catch
                {
                    return null;
                }
            }
        }

        public float? Beta
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.beta.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? PriceEarningsRatio
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.trailingPE.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? EarningsPerShare
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.trailingEps.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public DateTime? EarningsDate
        {
            get
            {
                try
                {
                    return DateTime.Parse(QuoteSummaryStoreObj.SelectToken("earnings.earningsChart.earningsDate[0].fmt").Value<string>());
                }   
                catch
                {
                    return null;
                }
            }
        }

        public float? ForwardDividend
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.dividendRate.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? ForwardDividendYield
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.dividendYield.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public DateTime? ExDividendDate
        {
            get
            {
                try
                {
                    return DateTime.Parse(QuoteSummaryStoreObj.SelectToken("summaryDetail.exDividendDate.fmt").Value<string>());
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? YearTargetEstimate
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.targetMeanPrice.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region "Statistics"

        public DateTime? LastFiscalYearEnd
        {
            get
            {
                try
                {
                    return DateTime.Parse(QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.lastFiscalYearEnd.fmt").Value<string>());
                }
                catch
                {
                    return null;
                }
            }
        }

        public DateTime? LastFiscalQuarterEnd
        {
            get
            {
                try
                {
                    return DateTime.Parse(QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.mostRecentQuarter.fmt").Value<string>());
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? ProfitMargin
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.profitMargins.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? OperatingMargin
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.operatingMargins.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? ReturnOnAssets
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.returnOnAssets.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? ReturnOnEquity
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.returnOnEquity.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? Revenue
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.totalRevenue.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? RevenuePerShare
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.revenuePerShare.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? QuarterlyRevenueGrowth
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.revenueGrowth.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? GrossProfit
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.grossProfits.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? EDBITDA
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.ebitda.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? NetIncomeAvailableToCommon
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.netIncomeToCommon.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? QuarterlyEarningsGrowth
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.earningsQuarterlyGrowth.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? Cash
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.totalCash.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? CashPerShare
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.totalCashPerShare.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? Debt
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.totalDebt.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? DebtToEquityRatio
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.debtToEquity.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? CurrentRatio
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.currentRatio.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? BookValuePerShare
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.bookValue.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? OperatingCashFlow
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.operatingCashflow.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? LeveredFreeCashFlow
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("financialData.freeCashflow.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? YearChangePercent
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.52WeekChange.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? SP500YearChangePercent
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.SandP52WeekChange.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? MovingAverage50Day
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.fiftyDayAverage.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? MovingAverage200Day
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.twoHundredDayAverage.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? SharesOutstanding
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.sharesOutstanding.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? Float
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.floatShares.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? PercentHeldByInsiders
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.heldPercentInsiders.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? PercentHeldByInstitutions
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.heldPercentInstitutions.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? SharesShort
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.sharesShort.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? ShortRatio
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.shortRatio.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? ShortPercentOfFloat
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.shortPercentOfFloat.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? ShortPercentOfSharesOutstanding
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.sharesPercentSharesOut.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? ForwardAnnualDividend
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.dividendRate.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? ForwardAnnualDividendYield
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.dividendYield.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? TrailingAnnualDividend
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.trailingAnnualDividendRate.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? TrailingAnnualDividendYield
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.trailingAnnualDividendYield.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? FiveYearAverageDividendYield
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.fiveYearAvgDividendYield.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public float? DividendPayoutRatio
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.payoutRatio.raw").Value<float>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public DateTime? DividendDate
        {
            get
            {
                try
                {
                    return DateTime.Parse(QuoteSummaryStoreObj.SelectToken("calendarEvents.dividendDate.fmt").Value<string>());
                }
                catch
                {
                    return null;
                }
            }
        }

        public string LastSplitFactor
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.lastSplitFactor").Value<string>();
                }
                catch
                {
                    return null;
                }
            }
        }

        public DateTime? LastSplitDate
        {
            get
            {
                try
                {
                    return DateTime.Parse(QuoteSummaryStoreObj.SelectToken("defaultKeyStatistics.lastSplitDate.fmt").Value<string>());
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion









        #region "Constructors"

        public static QuoteSummaryStore CreateFromRootJson(string json)
        {
            JObject root = JObject.Parse(json);
            JObject jo_context = JObject.Parse(root.Property("context").Value.ToString());
            JObject jo_dispatcher = JObject.Parse(jo_context.Property("dispatcher").Value.ToString());
            JObject jo_stores = JObject.Parse(jo_dispatcher.Property("stores").Value.ToString());
            JObject jo_QuoteSummaryStore = JObject.Parse(jo_stores.Property("QuoteSummaryStore").Value.ToString());
            return new QuoteSummaryStore(jo_QuoteSummaryStore);
        }

        public static QuoteSummaryStore CreateFromWebpage(string webpage_html)
        {
            string json = StoreExtractionToolkit.ExtractRootJsonFromWebpage(webpage_html);
            return QuoteSummaryStore.CreateFromRootJson(json);
        }

        public static async Task<QuoteSummaryStore> DownloadAsync(string symbol)
        {
            //Get from yahoo finance
            string url = "https://finance.yahoo.com/quote/" + symbol;
            HttpClient cl = new HttpClient();
            HttpResponseMessage hrm = await cl.GetAsync(url);
            string web = await hrm.Content.ReadAsStringAsync();
            return CreateFromWebpage(web);
        }

        #endregion

        #region "toolkit"

        #endregion
    }
}
