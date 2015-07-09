using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class MarketingVanueManager
{
	public MarketingVanueManager()
	{
	}

    public static DataSet  GetAllMarketingVanues()
    {
       DataSet marketingVanues = new DataSet();
        SqlMarketingVanueProvider sqlMarketingVanueProvider = new SqlMarketingVanueProvider();
        marketingVanues = sqlMarketingVanueProvider.GetAllMarketingVanues();
        return marketingVanues;
    }

	public static void marketingVanuesPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadMarketingVanuePage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlMarketingVanueProvider sqlMarketingVanueProvider = new SqlMarketingVanueProvider();
		DataSet ds =  sqlMarketingVanueProvider.GetMarketingVanuePageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 marketingVanuesPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllMarketingVanue()
    {
       DataSet marketingVanues = new DataSet();
        SqlMarketingVanueProvider sqlMarketingVanueProvider = new SqlMarketingVanueProvider();
        marketingVanues = sqlMarketingVanueProvider.GetDropDownLisAllMarketingVanue();
        return marketingVanues;
    }


    public static MarketingVanue GetMarketingVanueByMarketingVanueID(int MarketingVanueID)
    {
        MarketingVanue marketingVanue = new MarketingVanue();
        SqlMarketingVanueProvider sqlMarketingVanueProvider = new SqlMarketingVanueProvider();
        marketingVanue = sqlMarketingVanueProvider.GetMarketingVanueByMarketingVanueID(MarketingVanueID);
        return marketingVanue;
    }


    public static int InsertMarketingVanue(MarketingVanue marketingVanue)
    {
        SqlMarketingVanueProvider sqlMarketingVanueProvider = new SqlMarketingVanueProvider();
        return sqlMarketingVanueProvider.InsertMarketingVanue(marketingVanue);
    }


    public static bool UpdateMarketingVanue(MarketingVanue marketingVanue)
    {
        SqlMarketingVanueProvider sqlMarketingVanueProvider = new SqlMarketingVanueProvider();
        return sqlMarketingVanueProvider.UpdateMarketingVanue(marketingVanue);
    }

    public static bool DeleteMarketingVanue(int marketingVanueID)
    {
        SqlMarketingVanueProvider sqlMarketingVanueProvider = new SqlMarketingVanueProvider();
        return sqlMarketingVanueProvider.DeleteMarketingVanue(marketingVanueID);
    }
}

