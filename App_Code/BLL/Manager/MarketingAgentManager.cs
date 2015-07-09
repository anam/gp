using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class MarketingAgentManager
{
	public MarketingAgentManager()
	{
	}

    public static DataSet  GetAllMarketingAgents()
    {
       DataSet marketingAgents = new DataSet();
        SqlMarketingAgentProvider sqlMarketingAgentProvider = new SqlMarketingAgentProvider();
        marketingAgents = sqlMarketingAgentProvider.GetAllMarketingAgents();
        return marketingAgents;
    }

	public static void marketingAgentsPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadMarketingAgentPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlMarketingAgentProvider sqlMarketingAgentProvider = new SqlMarketingAgentProvider();
		DataSet ds =  sqlMarketingAgentProvider.GetMarketingAgentPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 marketingAgentsPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllMarketingAgent()
    {
       DataSet marketingAgents = new DataSet();
        SqlMarketingAgentProvider sqlMarketingAgentProvider = new SqlMarketingAgentProvider();
        marketingAgents = sqlMarketingAgentProvider.GetDropDownLisAllMarketingAgent();
        return marketingAgents;
    }


    public static MarketingAgent GetMarketingAgentByMarketingAgentID(int MarketingAgentID)
    {
        MarketingAgent marketingAgent = new MarketingAgent();
        SqlMarketingAgentProvider sqlMarketingAgentProvider = new SqlMarketingAgentProvider();
        marketingAgent = sqlMarketingAgentProvider.GetMarketingAgentByMarketingAgentID(MarketingAgentID);
        return marketingAgent;
    }


    public static int InsertMarketingAgent(MarketingAgent marketingAgent)
    {
        SqlMarketingAgentProvider sqlMarketingAgentProvider = new SqlMarketingAgentProvider();
        return sqlMarketingAgentProvider.InsertMarketingAgent(marketingAgent);
    }


    public static bool UpdateMarketingAgent(MarketingAgent marketingAgent)
    {
        SqlMarketingAgentProvider sqlMarketingAgentProvider = new SqlMarketingAgentProvider();
        return sqlMarketingAgentProvider.UpdateMarketingAgent(marketingAgent);
    }

    public static bool DeleteMarketingAgent(int marketingAgentID)
    {
        SqlMarketingAgentProvider sqlMarketingAgentProvider = new SqlMarketingAgentProvider();
        return sqlMarketingAgentProvider.DeleteMarketingAgent(marketingAgentID);
    }
}

