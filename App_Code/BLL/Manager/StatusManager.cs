using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class StatusManager
{
	public StatusManager()
	{
	}

    public static DataSet  GetAllStatuss()
    {
       DataSet statuss = new DataSet();
        SqlStatusProvider sqlStatusProvider = new SqlStatusProvider();
        statuss = sqlStatusProvider.GetAllStatuss();
        return statuss;
    }

	public static void statussPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadStatusPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlStatusProvider sqlStatusProvider = new SqlStatusProvider();
		DataSet ds =  sqlStatusProvider.GetStatusPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 statussPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllStatus()
    {
       DataSet statuss = new DataSet();
        SqlStatusProvider sqlStatusProvider = new SqlStatusProvider();
        statuss = sqlStatusProvider.GetDropDownLisAllStatus();
        return statuss;
    }


    public static Status GetStatusByStastusID(int StastusID)
    {
        Status status = new Status();
        SqlStatusProvider sqlStatusProvider = new SqlStatusProvider();
        status = sqlStatusProvider.GetStatusByStastusID(StastusID);
        return status;
    }


    public static int InsertStatus(Status status)
    {
        SqlStatusProvider sqlStatusProvider = new SqlStatusProvider();
        return sqlStatusProvider.InsertStatus(status);
    }


    public static bool UpdateStatus(Status status)
    {
        SqlStatusProvider sqlStatusProvider = new SqlStatusProvider();
        return sqlStatusProvider.UpdateStatus(status);
    }

    public static bool DeleteStatus(int statusID)
    {
        SqlStatusProvider sqlStatusProvider = new SqlStatusProvider();
        return sqlStatusProvider.DeleteStatus(statusID);
    }
}

