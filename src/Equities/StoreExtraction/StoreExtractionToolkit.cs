using System;

namespace Yahoo.Finance
{
    public class StoreExtractionToolkit
    {
        public static string ExtractRootJsonFromWebpage(string web_page_html)
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
            return FullJson;
        }
    }
}