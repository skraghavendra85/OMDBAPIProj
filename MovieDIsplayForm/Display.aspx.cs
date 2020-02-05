using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MovieDIsplayForm
{
    public partial class Display : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(!IsPostBack)
            //{
                grdMovieData.Sorting += GrdMovieData_Sorting;
            //}
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            GridBind();
            Session["SortEntity"] = "";

                        
        }



        private void GridBind()
        {
            IEnumerable<OMDBBase.Movie> lstMovies = GetData();
            if(lstMovies!=null)
            {
                grdMovieData.DataSource = lstMovies.ToList();
                grdMovieData.DataBind();
            }
            else
            {
                grdMovieData.DataSource = null;
                grdMovieData.DataBind();
            }
            

            

        }

        private IEnumerable<OMDBBase.Movie> GetData()
        {
            Logger.Logger objLogger = new Logger.Logger();
            ResponseAppender.FileAppender appender = new ResponseAppender.FileAppender();
            OMDBMovieScan.MovieFetch<OMDBBase.Movie> objScan = new OMDBMovieScan.MovieFetch<OMDBBase.Movie>(typeof(OMDBBase.SearchList), objLogger, appender);
            IEnumerable<OMDBBase.Movie> lstMovies = objScan.GetMovieListbyKeyWord(txtSearchKey.Text, txtYear.Text, txtPlot.Text);
            objLogger = null;
            appender = null;
            return lstMovies;
        }

        private void GrdMovieData_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortData="";
            if (string.IsNullOrEmpty(Convert.ToString (Session["SortEntity"])))
            {
                Session["SortEntity"] = e.SortExpression + "_" + "Descending";
                sortData = Convert.ToString(Session["SortEntity"]);
            }
            else
            {
                sortData = Convert.ToString(Session["SortEntity"]);

                sortData = e.SortExpression + "_" + (sortData.Contains(e.SortExpression) ? (sortData.Contains("Ascending") ? "Descending" : "Ascending") : "Descending");
                Session["SortEntity"] = sortData;
            }
            IEnumerable<OMDBBase.Movie> lstMovies = GetData();

            if (lstMovies != null)
            {
                PropertyInfo[] propertys = lstMovies.First().GetType().GetProperties();
                var lstProperties = propertys.ToList().Where(p=> p.Name==e.SortExpression).First();
                
                if (sortData.Contains("Ascending"))
                {
                    lstMovies = lstMovies.OrderBy(key => lstProperties.GetValue(key, null));
                }
                else
                {
                    lstMovies = lstMovies.OrderByDescending(key => lstProperties.GetValue(key,null));
                }
                grdMovieData.DataSource = lstMovies.ToList();
                grdMovieData.DataBind();
            }
        }
    }
}