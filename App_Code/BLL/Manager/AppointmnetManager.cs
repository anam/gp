using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class AppointmnetManager
{
	public AppointmnetManager()
	{
	}

    public static DataSet  GetAllAppointmnets()
    {
       DataSet appointmnets = new DataSet();
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        appointmnets = sqlAppointmnetProvider.GetAllAppointmnets();
        return appointmnets;
    }

	public static void appointmnetsPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadAppointmnetPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
		DataSet ds =  sqlAppointmnetProvider.GetAppointmnetPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 appointmnetsPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllAppointmnet()
    {
       DataSet appointmnets = new DataSet();
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        appointmnets = sqlAppointmnetProvider.GetDropDownLisAllAppointmnet();
        return appointmnets;
    }

    public static DataSet GetAllAppointmnetByClientID(int ClientID)
    {
        DataSet appointmnets = new DataSet();
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        appointmnets = sqlAppointmnetProvider.GetAllAppointmnetByClientID(ClientID);
        return appointmnets;
    }

    public static DataSet GetAllAppointmnetByMetroLocationID(int MetroLocationID)
    {
        DataSet appointmnets = new DataSet();
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        appointmnets = sqlAppointmnetProvider.GetAllAppointmnetByMetroLocationID(MetroLocationID);
        return appointmnets;
    }
    public static DataSet SearchAppointMent(string  MetroLocationID,string dateFrom,string DateTo)
    {
        DataSet appointmnets = new DataSet();
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        appointmnets = sqlAppointmnetProvider.SearchAppointMent(MetroLocationID, dateFrom, DateTo);
        return appointmnets;
    }
    public static Appointmnet GetAppointmnetByClientID(int ClientID)
    {
        Appointmnet appointmnet = new Appointmnet();
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        appointmnet = sqlAppointmnetProvider.GetAppointmnetByClientID(ClientID);
        return appointmnet;
    }


    public static Appointmnet GetMetroLocationByMetroLocationID(int MetroLocationID)
    {
        Appointmnet appointmnet = new Appointmnet();
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        appointmnet = sqlAppointmnetProvider.GetAppointmnetByMetroLocationID(MetroLocationID);
        return appointmnet;
    }


    public static Appointmnet GetStastusByStastusID(int StastusID)
    {
        Appointmnet appointmnet = new Appointmnet();
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        appointmnet = sqlAppointmnetProvider.GetAppointmnetByStastusID(StastusID);
        return appointmnet;
    }


    public static Appointmnet GetAppointmnetByAppointmnetID(int AppointmnetID)
    {
        Appointmnet appointmnet = new Appointmnet();
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        appointmnet = sqlAppointmnetProvider.GetAppointmnetByAppointmnetID(AppointmnetID);
        return appointmnet;
    }


    public static int InsertAppointmnet(Appointmnet appointmnet)
    {
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        return sqlAppointmnetProvider.InsertAppointmnet(appointmnet);
    }


    public static bool UpdateAppointmnet(Appointmnet appointmnet)
    {
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        return sqlAppointmnetProvider.UpdateAppointmnet(appointmnet);
    }

    public static bool UpdateAppointmnetStatus(int id, int status)
    {
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        return sqlAppointmnetProvider.UpdateAppointmnetStatus(id,status);
    }
    
    public static bool DeleteAppointmnet(int appointmnetID)
    {
        SqlAppointmnetProvider sqlAppointmnetProvider = new SqlAppointmnetProvider();
        return sqlAppointmnetProvider.DeleteAppointmnet(appointmnetID);
    }
}

