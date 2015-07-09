using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class ReferralSourceManager
{
	public ReferralSourceManager()
	{
	}

    public static DataSet  GetAllReferralSources()
    {
       DataSet referralSources = new DataSet();
        SqlReferralSourceProvider sqlReferralSourceProvider = new SqlReferralSourceProvider();
        referralSources = sqlReferralSourceProvider.GetAllReferralSources();
        return referralSources;
    }

	public static void referralSourcesPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadReferralSourcePage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlReferralSourceProvider sqlReferralSourceProvider = new SqlReferralSourceProvider();
		DataSet ds =  sqlReferralSourceProvider.GetReferralSourcePageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 referralSourcesPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllReferralSource()
    {
       DataSet referralSources = new DataSet();
        SqlReferralSourceProvider sqlReferralSourceProvider = new SqlReferralSourceProvider();
        referralSources = sqlReferralSourceProvider.GetDropDownLisAllReferralSource();
        return referralSources;
    }
    public static DataSet GetDropDownLisAllReferralSourceByMetroLocationID(int MetroLocationID)
    {
       DataSet referralSources = new DataSet();
        SqlReferralSourceProvider sqlReferralSourceProvider = new SqlReferralSourceProvider();
        referralSources = sqlReferralSourceProvider.GetDropDownLisAllReferralSourceByMetroLocationID(MetroLocationID);
        return referralSources;
    }
    
    public static DataSet   GetAllReferralSourcesWithRelation()
    {
       DataSet referralSources = new DataSet();
        SqlReferralSourceProvider sqlReferralSourceProvider = new SqlReferralSourceProvider();
        referralSources = sqlReferralSourceProvider.GetAllReferralSources();
        return referralSources;
    }


    public static ReferralSource GetHRMetroLocationByMetroLocationID(int MetroLocationID)
    {
        ReferralSource referralSource = new ReferralSource();
        SqlReferralSourceProvider sqlReferralSourceProvider = new SqlReferralSourceProvider();
        referralSource = sqlReferralSourceProvider.GetReferralSourceByMetroLocationID(MetroLocationID);
        return referralSource;
    }


    public static ReferralSource GetReferralSourceByReferralSourceID(int ReferralSourceID)
    {
        ReferralSource referralSource = new ReferralSource();
        SqlReferralSourceProvider sqlReferralSourceProvider = new SqlReferralSourceProvider();
        referralSource = sqlReferralSourceProvider.GetReferralSourceByReferralSourceID(ReferralSourceID);
        return referralSource;
    }


    public static int InsertReferralSource(ReferralSource referralSource)
    {
        SqlReferralSourceProvider sqlReferralSourceProvider = new SqlReferralSourceProvider();
        return sqlReferralSourceProvider.InsertReferralSource(referralSource);
    }


    public static bool UpdateReferralSource(ReferralSource referralSource)
    {
        SqlReferralSourceProvider sqlReferralSourceProvider = new SqlReferralSourceProvider();
        return sqlReferralSourceProvider.UpdateReferralSource(referralSource);
    }

    public static bool DeleteReferralSource(int referralSourceID)
    {
        SqlReferralSourceProvider sqlReferralSourceProvider = new SqlReferralSourceProvider();
        return sqlReferralSourceProvider.DeleteReferralSource(referralSourceID);
    }
}

