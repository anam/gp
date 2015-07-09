using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class MetroLocationManager
{
	public MetroLocationManager()
	{
	}

    public static DataSet  GetAllMetroLocations()
    {
       DataSet metroLocations = new DataSet();
        SqlMetroLocationProvider sqlMetroLocationProvider = new SqlMetroLocationProvider();
        metroLocations = sqlMetroLocationProvider.GetAllMetroLocations();
        return metroLocations;
    }

	public static void metroLocationsPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadMetroLocationPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlMetroLocationProvider sqlMetroLocationProvider = new SqlMetroLocationProvider();
		DataSet ds =  sqlMetroLocationProvider.GetMetroLocationPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 metroLocationsPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllMetroLocation()
    {
       DataSet metroLocations = new DataSet();
        SqlMetroLocationProvider sqlMetroLocationProvider = new SqlMetroLocationProvider();
        metroLocations = sqlMetroLocationProvider.GetDropDownLisAllMetroLocation();
        return metroLocations;
    }


    public static MetroLocation GetMetroLocationByMetroLocationID(int MetroLocationID)
    {
        MetroLocation metroLocation = new MetroLocation();
        SqlMetroLocationProvider sqlMetroLocationProvider = new SqlMetroLocationProvider();
        metroLocation = sqlMetroLocationProvider.GetMetroLocationByMetroLocationID(MetroLocationID);
        return metroLocation;
    }


    public static int InsertMetroLocation(MetroLocation metroLocation)
    {
        SqlMetroLocationProvider sqlMetroLocationProvider = new SqlMetroLocationProvider();
        return sqlMetroLocationProvider.InsertMetroLocation(metroLocation);
    }


    public static bool UpdateMetroLocation(MetroLocation metroLocation)
    {
        SqlMetroLocationProvider sqlMetroLocationProvider = new SqlMetroLocationProvider();
        return sqlMetroLocationProvider.UpdateMetroLocation(metroLocation);
    }

    public static bool DeleteMetroLocation(int metroLocationID)
    {
        SqlMetroLocationProvider sqlMetroLocationProvider = new SqlMetroLocationProvider();
        return sqlMetroLocationProvider.DeleteMetroLocation(metroLocationID);
    }
}

