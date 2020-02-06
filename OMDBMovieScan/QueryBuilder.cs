using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;
namespace OMDBMovieScan
{
    class QueryBuilder
    {
        public string APIBaseURL { get; set; }
        public string APIKey { get; set; }
        ILogger log = null;
        public QueryBuilder(ILogger iLog)
        {
            try
            {
                this.APIBaseURL = string.IsNullOrEmpty(ConfigurationManager.AppSettings["_baseURL"]) ? "" : ConfigurationManager.AppSettings["_baseURL"].ToString();
                this.APIKey = string.IsNullOrEmpty(ConfigurationManager.AppSettings["_apiKey"]) ? "" : ConfigurationManager.AppSettings["_apiKey"].ToString();
                this.log = iLog;
            }
            catch(Exception ex)
            {
                log.LogError(ex.InnerException.Message ?? ex.Message);
            }
        }

        public string GetQMBTitle(string title, string year, string plot)
        {
            string strQString = String.Empty;
            try
            {
                if (string.IsNullOrEmpty(this.APIBaseURL))
                    throw new Exception("Please validate your baseUrl");
                strQString += this.APIBaseURL + "?";
                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentNullException("Value cannot be null: title");

                strQString += "t=" + title + "&";

                if (!string.IsNullOrEmpty(year))
                    strQString += "y=" + year + "&";


                if (!string.IsNullOrEmpty(plot))
                    strQString += "plot=" + plot + "&";


                if (String.IsNullOrEmpty(this.APIKey))
                    throw new ArgumentNullException("A valid API Key is required to proceed");

                strQString += "apikey=" + this.APIKey;

                return strQString;
            }
            catch (Exception ex)
            {
                log.LogError(ex.InnerException.Message ?? ex.Message);
                return string.Empty;
            }
        }

        public string GetQMBSearch(string search,string year,string plot)
        {
            string strQString = String.Empty;
            try
            {
                if (string.IsNullOrEmpty(this.APIBaseURL))
                    throw new Exception("Please validate your baseUrl");
                strQString += this.APIBaseURL + "?";
                if (string.IsNullOrWhiteSpace(search))
                    throw new ArgumentNullException("Value cannot be null: search");

                strQString += "s=" + search + "&";

                if (!string.IsNullOrEmpty(year))
                    strQString += "y=" + year + "&";


                if (!string.IsNullOrEmpty(plot))
                    strQString += "plot=" + plot + "&";


                if (String.IsNullOrEmpty(this.APIKey))
                    throw new ArgumentNullException("A valid API Key is required to proceed");

                strQString += "apikey=" + this.APIKey;

                return strQString;
            }
            catch (Exception ex)
            {
                log.LogError(ex.InnerException.Message ?? ex.Message);
                return string.Empty;
            }
        }
    }
}
