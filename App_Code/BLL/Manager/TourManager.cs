using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class TourManager
{
	public TourManager()
	{
	}

    public static DataSet  GetAllTours()
    {
       DataSet tours = new DataSet();
        SqlTourProvider sqlTourProvider = new SqlTourProvider();
        tours = sqlTourProvider.GetAllTours();
        return tours;
    }

	public static void toursPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
		{
		double dblPageCount = (double)((decimal)recordCount / decimal.Parse(PageSize.ToString()));
		int pageCount = (int)Math.Ceiling(dblPageCount);
		List<ListItem> pages = new List<ListItem>();
		if (pageCount > 0)
		{
 		pages.Add(new ListItem("First", "1", currentPage > 1));
		for (int i = 1; i <= pageCount; i++)
		{
		 pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
		}
		pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
		}
		rptPager.DataSource = pages;
		rptPager.DataBind();
		}
 	public static void LoadTourPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlTourProvider sqlTourProvider = new SqlTourProvider();
		DataSet ds =  sqlTourProvider.GetTourPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 toursPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllTour()
    {
       DataSet tours = new DataSet();
        SqlTourProvider sqlTourProvider = new SqlTourProvider();
        tours = sqlTourProvider.GetDropDownLisAllTour();
        return tours;
    }

    public static DataSet   GetAllToursWithRelation()
    {
       DataSet tours = new DataSet();
        SqlTourProvider sqlTourProvider = new SqlTourProvider();
        tours = sqlTourProvider.GetAllTours();
        return tours;
    }


    public static Tour GetTourByCustomerID(int CustomerID)
    {
        Tour tour = new Tour();
        SqlTourProvider sqlTourProvider = new SqlTourProvider();
        tour = sqlTourProvider.GetTourByCustomerID(CustomerID);
        return tour;
    }


    public static Tour GetTourStatusByTourStatusID(int TourStatusID)
    {
        Tour tour = new Tour();
        SqlTourProvider sqlTourProvider = new SqlTourProvider();
        tour = sqlTourProvider.GetTourByTourStatusID(TourStatusID);
        return tour;
    }


    public static Tour GetTourByTourID(int TourID)
    {
        Tour tour = new Tour();
        SqlTourProvider sqlTourProvider = new SqlTourProvider();
        tour = sqlTourProvider.GetTourByTourID(TourID);
        return tour;
    }


    public static int InsertTour(Tour tour)
    {
        SqlTourProvider sqlTourProvider = new SqlTourProvider();
        return sqlTourProvider.InsertTour(tour);
    }


    public static bool UpdateTour(Tour tour)
    {
        SqlTourProvider sqlTourProvider = new SqlTourProvider();
        return sqlTourProvider.UpdateTour(tour);
    }

    public static bool DeleteTour(int tourID)
    {
        SqlTourProvider sqlTourProvider = new SqlTourProvider();
        return sqlTourProvider.DeleteTour(tourID);
    }
}

