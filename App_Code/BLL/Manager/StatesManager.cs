using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class StatesManager
{
	public StatesManager()
	{
	}

    public static DataSet  GetAllStatess()
    {
       DataSet statess = new DataSet();
        SqlStatesProvider sqlStatesProvider = new SqlStatesProvider();
        statess = sqlStatesProvider.GetAllStatess();
        return statess;
    }

	public static void statessPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadStatesPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlStatesProvider sqlStatesProvider = new SqlStatesProvider();
		DataSet ds =  sqlStatesProvider.GetStatesPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 statessPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllStates()
    {
       DataSet statess = new DataSet();
        SqlStatesProvider sqlStatesProvider = new SqlStatesProvider();
        statess = sqlStatesProvider.GetDropDownLisAllStates();
        return statess;
    }


    public static States GetStatesByStateID(int StateID)
    {
        States states = new States();
        SqlStatesProvider sqlStatesProvider = new SqlStatesProvider();
        states = sqlStatesProvider.GetStatesByStateID(StateID);
        return states;
    }


    public static int InsertStates(States states)
    {
        SqlStatesProvider sqlStatesProvider = new SqlStatesProvider();
        return sqlStatesProvider.InsertStates(states);
    }


    public static bool UpdateStates(States states)
    {
        SqlStatesProvider sqlStatesProvider = new SqlStatesProvider();
        return sqlStatesProvider.UpdateStates(states);
    }

    public static bool DeleteStates(int statesID)
    {
        SqlStatesProvider sqlStatesProvider = new SqlStatesProvider();
        return sqlStatesProvider.DeleteStates(statesID);
    }
}

