using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class LeadSourceManager
{
	public LeadSourceManager()
	{
	}

    public static DataSet  GetAllLeadSources()
    {
       DataSet leadSources = new DataSet();
        SqlLeadSourceProvider sqlLeadSourceProvider = new SqlLeadSourceProvider();
        leadSources = sqlLeadSourceProvider.GetAllLeadSources();
        return leadSources;
    }

	public static void leadSourcesPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadLeadSourcePage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlLeadSourceProvider sqlLeadSourceProvider = new SqlLeadSourceProvider();
		DataSet ds =  sqlLeadSourceProvider.GetLeadSourcePageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 leadSourcesPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllLeadSource()
    {
       DataSet leadSources = new DataSet();
        SqlLeadSourceProvider sqlLeadSourceProvider = new SqlLeadSourceProvider();
        leadSources = sqlLeadSourceProvider.GetDropDownLisAllLeadSource();
        return leadSources;
    }


    public static LeadSource GetLeadSourceByLeadSourceID(int LeadSourceID)
    {
        LeadSource leadSource = new LeadSource();
        SqlLeadSourceProvider sqlLeadSourceProvider = new SqlLeadSourceProvider();
        leadSource = sqlLeadSourceProvider.GetLeadSourceByLeadSourceID(LeadSourceID);
        return leadSource;
    }


    public static int InsertLeadSource(LeadSource leadSource)
    {
        SqlLeadSourceProvider sqlLeadSourceProvider = new SqlLeadSourceProvider();
        return sqlLeadSourceProvider.InsertLeadSource(leadSource);
    }


    public static bool UpdateLeadSource(LeadSource leadSource)
    {
        SqlLeadSourceProvider sqlLeadSourceProvider = new SqlLeadSourceProvider();
        return sqlLeadSourceProvider.UpdateLeadSource(leadSource);
    }

    public static bool DeleteLeadSource(int leadSourceID)
    {
        SqlLeadSourceProvider sqlLeadSourceProvider = new SqlLeadSourceProvider();
        return sqlLeadSourceProvider.DeleteLeadSource(leadSourceID);
    }
}

