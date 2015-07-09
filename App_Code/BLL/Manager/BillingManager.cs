using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class BillingManager
{
	public BillingManager()
	{
	}

    public static DataSet  GetAllBillings()
    {
       DataSet billings = new DataSet();
        SqlBillingProvider sqlBillingProvider = new SqlBillingProvider();
        billings = sqlBillingProvider.GetAllBillings();
        return billings;
    }

	public static void billingsPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadBillingPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlBillingProvider sqlBillingProvider = new SqlBillingProvider();
		DataSet ds =  sqlBillingProvider.GetBillingPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 billingsPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllBilling()
    {
       DataSet billings = new DataSet();
        SqlBillingProvider sqlBillingProvider = new SqlBillingProvider();
        billings = sqlBillingProvider.GetDropDownLisAllBilling();
        return billings;
    }


    public static DataSet GetBillingByClientID(int ClientID)
    {
        DataSet billings = new DataSet();
        SqlBillingProvider sqlBillingProvider = new SqlBillingProvider();
        billings = sqlBillingProvider.GetBillingByClientID(ClientID);
        return billings;
    }


    public static Billing GetBillingByBillingID(int BillingID)
    {
        Billing billing = new Billing();
        SqlBillingProvider sqlBillingProvider = new SqlBillingProvider();
        billing = sqlBillingProvider.GetBillingByBillingID(BillingID);
        return billing;
    }


    public static int InsertBilling(Billing billing)
    {
        SqlBillingProvider sqlBillingProvider = new SqlBillingProvider();
        return sqlBillingProvider.InsertBilling(billing);
    }


    public static bool UpdateBilling(Billing billing)
    {
        SqlBillingProvider sqlBillingProvider = new SqlBillingProvider();
        return sqlBillingProvider.UpdateBilling(billing);
    }
    public static bool UpdatePaymnetType(int id, int status, int TreatmentService)
    {
        SqlBillingProvider sqlBillingProvider = new SqlBillingProvider();
        return sqlBillingProvider.UpdatePaymnetType(id, status, TreatmentService);
    }
    
    public static bool DeleteBilling(int billingID)
    {
        SqlBillingProvider sqlBillingProvider = new SqlBillingProvider();
        return sqlBillingProvider.DeleteBilling(billingID);
    }
}

