using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Yahoo.Finance
{
    public class QuoteSummaryStore
    {
        private JObject QuoteObj;

        public QuoteSummaryStore(JObject obj)
        {
            QuoteObj = obj;
        }

        public string Symbol
        {
            get
            {
                return QuoteObj.Property("symbol").Value.ToString();
            }
        }


        #region "Constructors"

        #endregion

    }
}
