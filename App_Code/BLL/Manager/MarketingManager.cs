using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class MarketingManager
{
	public MarketingManager()
	{
	}

    public static DataSet  GetAllMarketings()
    {
       DataSet marketings = new DataSet();
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        marketings = sqlMarketingProvider.GetAllMarketings();
        return marketings;
    }

	public static void marketingsPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadMarketingPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
		DataSet ds =  sqlMarketingProvider.GetMarketingPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 marketingsPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllMarketing()
    {
       DataSet marketings = new DataSet();
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        marketings = sqlMarketingProvider.GetDropDownLisAllMarketing();
        return marketings;
    }

    public static DataSet   GetAllMarketingsWithRelation()
    {
       DataSet marketings = new DataSet();
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        marketings = sqlMarketingProvider.GetAllMarketings();
        return marketings;
    }


    public static Marketing GetCustomerByCustomerID(int CustomerID)
    {
        Marketing marketing = new Marketing();
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        marketing = sqlMarketingProvider.GetMarketingByCustomerID(CustomerID);
        return marketing;
    }


    public static Marketing GetMarketingAgentByMarketingAgentID(int MarketingAgentID)
    {
        Marketing marketing = new Marketing();
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        marketing = sqlMarketingProvider.GetMarketingByMarketingAgentID(MarketingAgentID);
        return marketing;
    }


    public static Marketing GetMarketingCloserByMarketingCloserID(int MarketingCloserID)
    {
        Marketing marketing = new Marketing();
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        marketing = sqlMarketingProvider.GetMarketingByMarketingCloserID(MarketingCloserID);
        return marketing;
    }


    public static Marketing GetMarketingVanueByMarketingVanueID(int MarketingVanueID)
    {
        Marketing marketing = new Marketing();
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        marketing = sqlMarketingProvider.GetMarketingByMarketingVanueID(MarketingVanueID);
        return marketing;
    }


    public static Marketing GetLeadSourceByLeadSourceID(int LeadSourceID)
    {
        Marketing marketing = new Marketing();
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        marketing = sqlMarketingProvider.GetMarketingByLeadSourceID(LeadSourceID);
        return marketing;
    }


    public static Marketing GetGiftByGiftID(int GiftID)
    {
        Marketing marketing = new Marketing();
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        marketing = sqlMarketingProvider.GetMarketingByGiftID(GiftID);
        return marketing;
    }


    public static Marketing GetMarketingByMarketingID(int MarketingID)
    {
        Marketing marketing = new Marketing();
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        marketing = sqlMarketingProvider.GetMarketingByMarketingID(MarketingID);
        return marketing;
    }


    public static int InsertMarketing(Marketing marketing)
    {
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        return sqlMarketingProvider.InsertMarketing(marketing);
    }


    public static bool UpdateMarketing(Marketing marketing)
    {
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        return sqlMarketingProvider.UpdateMarketing(marketing);
    }

    public static bool DeleteMarketing(int marketingID)
    {
        SqlMarketingProvider sqlMarketingProvider = new SqlMarketingProvider();
        return sqlMarketingProvider.DeleteMarketing(marketingID);
    }
}

