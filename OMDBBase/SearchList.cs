﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMDBBase
{
    public class SearchList
    {
        public List<Movie> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }
}
