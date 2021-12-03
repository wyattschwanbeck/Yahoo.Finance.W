using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Yahoo.Finance
{
    public class QuoteDataStore
    {
        private string _Symbol;
        private JObject QuoteObj;

        public QuoteDataStore(string symbol, JObject obj)
        {
            _Symbol = symbol;
            QuoteObj = obj;
        }

        public string Symbol
        {
            get
            {
                return _Symbol;
            }
        }

        #region "Extracted properties"

        public float? Open
        {
            get
            {
                try
                {
                    JObject obj = JObject.Parse(QuoteObj.Property("regularMarketOpen").Value.ToString());
                    float val = Convert.ToSingle(obj.Property("raw").Value.ToString());
                    return val;
                }
                catch
                {
                    return null;
                }
            }
        }


        #endregion


        #region "static constructors"

        public static QuoteDataStore[] ExtractQuoteDataStoresFromWebPage(string web_page_html)
        {
            int loc1 = web_page_html.IndexOf("root.App.main =");
            if (loc1 == -1)
            {
                throw new Exception("Unable to location data store in web page content.");
            }
            loc1 = web_page_html.IndexOf("{", loc1 + 1) - 1; //Get space BEFORE the opening bracket (hence -1)

            int loc2 = web_page_html.IndexOf("</script><script>", loc1);
            loc2 = web_page_html.LastIndexOf(";", loc2 - 1);
            loc2 = web_page_html.LastIndexOf(";", loc2 - 1); //Last semicolon before last closing bracket

            //Get the JSON body
            string FullJson = web_page_html.Substring(loc1 + 1, loc2 - loc1 - 1);
            JObject Master = JObject.Parse(FullJson);

            //Step down to the quoteData
            JObject jo_context = JObject.Parse(Master.Property("context").Value.ToString());
            JObject jo_dispatcher = JObject.Parse(jo_context.Property("dispatcher").Value.ToString());
            JObject jo_stores = JObject.Parse(jo_dispatcher.Property("stores").Value.ToString());
            JObject jo_StreamDataStore = JObject.Parse(jo_stores.Property("StreamDataStore").Value.ToString());
            JObject jo_quoteData = JObject.Parse(jo_StreamDataStore.Property("quoteData").Value.ToString());

            //Get each quote object
            IEnumerable<JProperty> QuoteProps = jo_quoteData.Properties();

            //Create each
            List<QuoteDataStore> ToReturn = new List<QuoteDataStore>();
            foreach (JProperty prop in QuoteProps)
            {
                JObject thisObj = JObject.Parse(prop.Value.ToString());
                ToReturn.Add(new QuoteDataStore(prop.Name, thisObj));
            }

            return ToReturn.ToArray();           
        }

        #endregion


    }
}