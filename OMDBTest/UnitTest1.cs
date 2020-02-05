using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace OMDBTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Logger.Logger objLogger = new Logger.Logger();
            ResponseAppender.FileAppender appender = new ResponseAppender.FileAppender();
            OMDBMovieScan.MovieFetch<OMDBBase.Movie> objScan = new OMDBMovieScan.MovieFetch<OMDBBase.Movie>(typeof(OMDBBase.SearchList), objLogger, appender);
            IEnumerable<OMDBBase.Movie> lstMovies = objScan.GetMovieListbyKeyWord("days", "2019", "");
            var boolVal = lstMovies.Where(a => a.Title.Contains("days")).Any();
            Assert.AreEqual(true,boolVal);
        }
    }
}
