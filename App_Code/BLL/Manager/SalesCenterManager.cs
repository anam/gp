using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class SalesCenterManager
{
	public SalesCenterManager()
	{
	}

    public static DataSet  GetAllSalesCenters()
    {
       DataSet salesCenters = new DataSet();
        SqlSalesCenterProvider sqlSalesCenterProvider = new SqlSalesCenterProvider();
        salesCenters = sqlSalesCenterProvider.GetAllSalesCenters();
        return salesCenters;
    }

	public static void salesCentersPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadSalesCenterPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlSalesCenterProvider sqlSalesCenterProvider = new SqlSalesCenterProvider();
		DataSet ds =  sqlSalesCenterProvider.GetSalesCenterPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 salesCentersPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllSalesCenter()
    {
       DataSet salesCenters = new DataSet();
        SqlSalesCenterProvider sqlSalesCenterProvider = new SqlSalesCenterProvider();
        salesCenters = sqlSalesCenterProvider.GetDropDownLisAllSalesCenter();
        return salesCenters;
    }


    public static SalesCenter GetSalesCenterBySalesCenterId(int SalesCenterId)
    {
        SalesCenter salesCenter = new SalesCenter();
        SqlSalesCenterProvider sqlSalesCenterProvider = new SqlSalesCenterProvider();
        salesCenter = sqlSalesCenterProvider.GetSalesCenterBySalesCenterId(SalesCenterId);
        return salesCenter;
    }


    public static int InsertSalesCenter(SalesCenter salesCenter)
    {
        SqlSalesCenterProvider sqlSalesCenterProvider = new SqlSalesCenterProvider();
        return sqlSalesCenterProvider.InsertSalesCenter(salesCenter);
    }


    public static bool UpdateSalesCenter(SalesCenter salesCenter)
    {
        SqlSalesCenterProvider sqlSalesCenterProvider = new SqlSalesCenterProvider();
        return sqlSalesCenterProvider.UpdateSalesCenter(salesCenter);
    }

    public static bool DeleteSalesCenter(int salesCenterID)
    {
        SqlSalesCenterProvider sqlSalesCenterProvider = new SqlSalesCenterProvider();
        return sqlSalesCenterProvider.DeleteSalesCenter(salesCenterID);
    }
}

