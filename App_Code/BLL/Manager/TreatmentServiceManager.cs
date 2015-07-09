using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class TreatmentServiceManager
{
	public TreatmentServiceManager()
	{
	}

    public static DataSet  GetAllTreatmentServices()
    {
       DataSet treatmentServices = new DataSet();
        SqlTreatmentServiceProvider sqlTreatmentServiceProvider = new SqlTreatmentServiceProvider();
        treatmentServices = sqlTreatmentServiceProvider.GetAllTreatmentServices();
        return treatmentServices;
    }

	public static void treatmentServicesPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadTreatmentServicePage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlTreatmentServiceProvider sqlTreatmentServiceProvider = new SqlTreatmentServiceProvider();
		DataSet ds =  sqlTreatmentServiceProvider.GetTreatmentServicePageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 treatmentServicesPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllTreatmentService()
    {
       DataSet treatmentServices = new DataSet();
        SqlTreatmentServiceProvider sqlTreatmentServiceProvider = new SqlTreatmentServiceProvider();
        treatmentServices = sqlTreatmentServiceProvider.GetDropDownLisAllTreatmentService();
        return treatmentServices;
    }


    public static TreatmentService GetTreatmentServiceByTreatmentServiceID(int TreatmentServiceID)
    {
        TreatmentService treatmentService = new TreatmentService();
        SqlTreatmentServiceProvider sqlTreatmentServiceProvider = new SqlTreatmentServiceProvider();
        treatmentService = sqlTreatmentServiceProvider.GetTreatmentServiceByTreatmentServiceID(TreatmentServiceID);
        return treatmentService;
    }


    public static int InsertTreatmentService(TreatmentService treatmentService)
    {
        SqlTreatmentServiceProvider sqlTreatmentServiceProvider = new SqlTreatmentServiceProvider();
        return sqlTreatmentServiceProvider.InsertTreatmentService(treatmentService);
    }


    public static bool UpdateTreatmentService(TreatmentService treatmentService)
    {
        SqlTreatmentServiceProvider sqlTreatmentServiceProvider = new SqlTreatmentServiceProvider();
        return sqlTreatmentServiceProvider.UpdateTreatmentService(treatmentService);
    }

    public static bool DeleteTreatmentService(int treatmentServiceID)
    {
        SqlTreatmentServiceProvider sqlTreatmentServiceProvider = new SqlTreatmentServiceProvider();
        return sqlTreatmentServiceProvider.DeleteTreatmentService(treatmentServiceID);
    }
}

