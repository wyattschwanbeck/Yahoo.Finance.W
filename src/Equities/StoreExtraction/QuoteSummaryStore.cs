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

        public int? AverageVolume
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
