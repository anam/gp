using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class ClientManager
{
	public ClientManager()
	{
	}

    public static DataSet  GetAllClients()
    {
       DataSet clients = new DataSet();
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        clients = sqlClientProvider.GetAllClients();
        return clients;
    }


    public static void LoadClientPaymentHistoryPage(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
    public static void LoadCallLogPage(string search_val, System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize, Label lbltotalrecord, int LocationID, DateTime FromDate, DateTime ToDate, String UserID, string SignUp)
    {
        int recordCount = 0;
        int PageSize = int.Parse(ddlPageSize.SelectedValue);
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        DataSet ds = sqlClientProvider.GetCallLogPageWise(search_val, pageIndex, PageSize, out recordCount, LocationID, FromDate, ToDate, UserID, SignUp);
        gv.DataSource = ds;
        gv.DataBind();

        string htmlStatus = "";
        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            htmlStatus += dr["TourStatusName"].ToString() + " : (<b>";
            int count = 0;
            foreach (DataRow drCount in ds.Tables[2].Rows)
            {
                if (drCount["StastusID"].ToString() == dr["TourStatusID"].ToString())
                {
                    count = int.Parse(drCount["NoOfRecords"].ToString());
                }
            }

            htmlStatus += count.ToString() + "</b>) ";
 
        }

        lbltotalrecord.Text = htmlStatus + " Total Record Found : (<b>" + recordCount + "</b>)";
        clientsPaggination(rptPager, recordCount, pageIndex, PageSize);

    }
	public static void clientsPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
    public static void LoadClientPage(string search_val, System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize, Label lbltotalrecord, int LocationID, DateTime FromDate, DateTime ToDate)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlClientProvider sqlClientProvider = new SqlClientProvider();
        DataSet ds = sqlClientProvider.GetClientPageWise(search_val, pageIndex, PageSize, out recordCount, LocationID,FromDate,  ToDate);
		gv.DataSource = ds;
		gv.DataBind();
        lbltotalrecord.Text = "Total Record Found : " + recordCount;
		 clientsPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static void LoadClientPaymentHistoryPage(string search_val, System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
    {
        int recordCount = 0;
        int PageSize = int.Parse(ddlPageSize.SelectedValue);
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        DataSet ds = sqlClientProvider.LoadClientPaymentHistoryPage(search_val, pageIndex, PageSize, out recordCount);
        gv.DataSource = ds;
        gv.DataBind();
        LoadClientPaymentHistoryPage(rptPager, recordCount, pageIndex, PageSize);
    }


    public static DataSet  GetDropDownListAllClient()
    {
       DataSet clients = new DataSet();
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        clients = sqlClientProvider.GetDropDownLisAllClient();
        return clients;
    }

    public static DataSet   GetAllClientsWithRelation()
    {
       DataSet clients = new DataSet();
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        clients = sqlClientProvider.GetAllClients();
        return clients;
    }


    public static Client GetCityByCityID(int CityID)
    {
        Client client = new Client();
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        client = sqlClientProvider.GetClientByCityID(CityID);
        return client;
    }


    public static Client GetMetroLocationByMetroLocationID(int MetroLocationID)
    {
        Client client = new Client();
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        client = sqlClientProvider.GetClientByMetroLocationID(MetroLocationID);
        return client;
    }


    public static Client GetClientByClientID(int ClientID)
    {
        Client client = new Client();
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        client = sqlClientProvider.GetClientByClientID(ClientID);
        return client;
    }

    public static string GetClientByMaxApptDate(int ClientID)
    {
        
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        return sqlClientProvider.GetClientMaxApptDate(ClientID);
        
    }

    public static int InsertClient(Client client)
    {
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        return sqlClientProvider.InsertClient(client);
    }


    public static bool UpdateClient(Client client)
    {
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        return sqlClientProvider.UpdateClient(client);
    }

    public static bool DeleteClient(int clientID)
    {
        SqlClientProvider sqlClientProvider = new SqlClientProvider();
        return sqlClientProvider.DeleteClient(clientID);
    }
}

