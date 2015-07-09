using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class FormsManager
{
	public FormsManager()
	{
	}

    public static DataSet  GetAllFormss()
    {
       DataSet formss = new DataSet();
        SqlFormsProvider sqlFormsProvider = new SqlFormsProvider();
        formss = sqlFormsProvider.GetAllFormss();
        return formss;
    }

	public static void formssPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void LoadFormsPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlFormsProvider sqlFormsProvider = new SqlFormsProvider();
		DataSet ds =  sqlFormsProvider.GetFormsPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 formssPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllForms()
    {
       DataSet formss = new DataSet();
        SqlFormsProvider sqlFormsProvider = new SqlFormsProvider();
        formss = sqlFormsProvider.GetDropDownLisAllForms();
        return formss;
    }


    public static Forms GetFormsByFormsID(int FormsID)
    {
        Forms forms = new Forms();
        SqlFormsProvider sqlFormsProvider = new SqlFormsProvider();
        forms = sqlFormsProvider.GetFormsByFormsID(FormsID);
        return forms;
    }


    public static int InsertForms(Forms forms)
    {
        SqlFormsProvider sqlFormsProvider = new SqlFormsProvider();
        return sqlFormsProvider.InsertForms(forms);
    }


    public static bool UpdateForms(Forms forms)
    {
        SqlFormsProvider sqlFormsProvider = new SqlFormsProvider();
        return sqlFormsProvider.UpdateForms(forms);
    }

    public static bool DeleteForms(int formsID)
    {
        SqlFormsProvider sqlFormsProvider = new SqlFormsProvider();
        return sqlFormsProvider.DeleteForms(formsID);
    }
}

