using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class TourStatusManager
{
	public TourStatusManager()
	{
	}

    public static DataSet  GetAllTourStatuss()
    {
       DataSet tourStatuss = new DataSet();
        SqlTourStatusProvider sqlTourStatusProvider = new SqlTourStatusProvider();
        tourStatuss = sqlTourStatusProvider.GetAllTourStatuss();
        return tourStatuss;
    }

	public static void tourStatussPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadTourStatusPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlTourStatusProvider sqlTourStatusProvider = new SqlTourStatusProvider();
		DataSet ds =  sqlTourStatusProvider.GetTourStatusPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 tourStatussPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllTourStatus()
    {
       DataSet tourStatuss = new DataSet();
        SqlTourStatusProvider sqlTourStatusProvider = new SqlTourStatusProvider();
        tourStatuss = sqlTourStatusProvider.GetDropDownLisAllTourStatus();
        return tourStatuss;
    }


    public static TourStatus GetTourStatusByTourStatusID(int TourStatusID)
    {
        TourStatus tourStatus = new TourStatus();
        SqlTourStatusProvider sqlTourStatusProvider = new SqlTourStatusProvider();
        tourStatus = sqlTourStatusProvider.GetTourStatusByTourStatusID(TourStatusID);
        return tourStatus;
    }


    public static int InsertTourStatus(TourStatus tourStatus)
    {
        SqlTourStatusProvider sqlTourStatusProvider = new SqlTourStatusProvider();
        return sqlTourStatusProvider.InsertTourStatus(tourStatus);
    }


    public static bool UpdateTourStatus(TourStatus tourStatus)
    {
        SqlTourStatusProvider sqlTourStatusProvider = new SqlTourStatusProvider();
        return sqlTourStatusProvider.UpdateTourStatus(tourStatus);
    }

    public static bool DeleteTourStatus(int tourStatusID)
    {
        SqlTourStatusProvider sqlTourStatusProvider = new SqlTourStatusProvider();
        return sqlTourStatusProvider.DeleteTourStatus(tourStatusID);
    }
}

