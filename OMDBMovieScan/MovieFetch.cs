using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.IO;

namespace OMDBMovieScan
{
    public class MovieFetch<T> : IMovieFetch<T>
    {
        Type typeReturnList = null;
        Logger.ILogger Ilog;
        ResponseAppender.IAppender iappender;
        string path = "";

        /// <summary>
        /// Injecting the ReturnType for converting the data and sending appropriate information as a return object
        /// Logger type
        /// FileAPpender Type
        /// </summary>
        /// <param name="lstRetType"></param>
        /// <param name="log"></param>
        /// <param name="app"></param>
        public MovieFetch(Type lstRetType, Logger.ILogger log, ResponseAppender.IAppender app)
        {
            this.typeReturnList = lstRetType;
            this.Ilog = log;
            this.iappender = app;
            path = System.Configuration.ConfigurationManager.AppSettings["path"];
        }

        /// <summary>
        /// This will get an individual movie for the provided title
        /// </summary>
        /// <param name="title"></param>
        /// <param name="year"></param>
        /// <param name="plot"></param>
        /// <returns></returns>
        public T GetMovieByTitle(string title, string year, string plot)
        {
            T retObj;

            try
            {
                QueryBuilder qb = new QueryBuilder(this.Ilog);
                this.Ilog.LogInfo($"Entering the Querybuilder with title:{title},year: {year ?? "NULL"},plot:{plot ?? "NULL"}");
                string _url = qb.GetQMBTitle(title, year, plot);
                this.Ilog.LogInfo("Query generated");
                if (string.IsNullOrEmpty(_url))
                    throw new Exception("Issue identified while generating the url");
                else
                {
                    this.iappender.AddtoFile(this.GetType(), _url, path);
                    string resultJSON = SendRequest(_url).Result;
                    this.iappender.AddtoFile(this.GetType(), resultJSON, path);
                    this.Ilog.LogInfo(resultJSON ?? "Result fetched successfully");
                    JavaScriptSerializer jSerialize = new JavaScriptSerializer();
                    retObj = jSerialize.Deserialize<T>(resultJSON);

                }

                return retObj;
            }
            catch (Exception ex)
            {
                this.Ilog.LogError(ex.InnerException.Message ?? ex.Message);
                return default(T);
            }



        }

        /// <summary>
        /// This will fetch the required list of movies for the given search keyword, year and plot
        /// </summary>
        /// <param name="searchkeyword"></param>
        /// <param name="year"></param>
        /// <param name="plot"></param>
        /// <returns></returns>
        public IEnumerable<T> GetMovieListbyKeyWord(string searchkeyword, string year, string plot)
        {
            List<T> retObj;

            QueryBuilder qb = new QueryBuilder(this.Ilog);
            try
            {
                this.Ilog.LogInfo($"Entering the Querybuilder with search:{searchkeyword},year: {year ?? "NULL"},plot:{plot ?? "NULL"}");
                string _url = qb.GetQMBSearch(searchkeyword, year, plot);
                this.Ilog.LogInfo("Query generated");
                var anonymousdata = new { Search = "", Response = false };
                if (string.IsNullOrEmpty(_url))
                    throw new Exception("Issue identified while generating the url");
                else
                {
                    this.iappender.AddtoFile(this.GetType(), _url, path);
                    string resultJSON = SendRequest(_url).Result;
                    this.iappender.AddtoFile(this.GetType(), resultJSON, path);
                    this.Ilog.LogInfo(resultJSON ?? "Result fetched successfully");
                    JavaScriptSerializer jSerialize = new JavaScriptSerializer();
                    dynamic data = jSerialize.Deserialize(resultJSON, this.typeReturnList);
                    retObj = (List<T>)data.Search;

                }

                return retObj.AsEnumerable();
            }
            catch (Exception ex)
            {
                this.Ilog.LogError(ex.InnerException.Message ?? ex.Message);
                return default(IEnumerable<T>);
            }

        }

        private async Task<string> SendRequest(string url)
        {
            string objReturn = String.Empty;
            try
            {
                //using (HttpClient objclient = new HttpClient())
                //{
                //objclient.MaxResponseContentBufferSize = 2147483644;
                //objReturn = await objclient.GetStringAsync(url);

                WebRequest request = WebRequest.Create(url);

                WebResponse response = request.GetResponse();

                Stream dataStream = response.GetResponseStream();

                using (StreamReader reader = new StreamReader(dataStream))
                {
                    objReturn = reader.ReadToEnd();
                }



                //reader.Close();
                response.Close();

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objReturn;
        }
    }
}
