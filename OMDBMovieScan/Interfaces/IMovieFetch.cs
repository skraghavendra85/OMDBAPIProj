using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMDBMovieScan
{
    interface IMovieFetch<T>
    {
        T GetMovieByTitle(string title,string year, string plot);
        IEnumerable<T> GetMovieListbyKeyWord(string searchkeyword, string year, string plot);
    }
}
