using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

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

        public float? AskQuantity
        {
            get
            {
                try
                {
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.askSize.raw").Value<float>();
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
                    return QuoteSummaryStoreObj.SelectToken("summaryDetail.trailingEPS.raw").Value<float>();
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

        #endregion

        #region "toolkit"

        #endregion
    }
}
