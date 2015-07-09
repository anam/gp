using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public class FormRightsManager
{
	public FormRightsManager()
	{
	}

    public static DataSet  GetAllFormRightss()
    {
       DataSet formRightss = new DataSet();
        SqlFormRightsProvider sqlFormRightsProvider = new SqlFormRightsProvider();
        formRightss = sqlFormRightsProvider.GetAllFormRightss();
        return formRightss;
    }

	public static void formRightssPaggination(System.Web.UI.WebControls.Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
 	public static void  LoadFormRightsPage(System.Web.UI.WebControls.GridView gv, System.Web.UI.WebControls.Repeater rptPager, int pageIndex, DropDownList ddlPageSize)
	{
		int recordCount=0;
		int PageSize =  int.Parse(ddlPageSize.SelectedValue);
		SqlFormRightsProvider sqlFormRightsProvider = new SqlFormRightsProvider();
		DataSet ds =  sqlFormRightsProvider.GetFormRightsPageWise(pageIndex, PageSize, out recordCount);
		gv.DataSource = ds;
		gv.DataBind();
		 formRightssPaggination(rptPager,recordCount, pageIndex, PageSize);
	}
    public static DataSet  GetDropDownListAllFormRights()
    {
       DataSet formRightss = new DataSet();
        SqlFormRightsProvider sqlFormRightsProvider = new SqlFormRightsProvider();
        formRightss = sqlFormRightsProvider.GetDropDownLisAllFormRights();
        return formRightss;
    }


    public static FormRights GetFormByFormID(int FormID)
    {
        FormRights formRights = new FormRights();
        SqlFormRightsProvider sqlFormRightsProvider = new SqlFormRightsProvider();
        formRights = sqlFormRightsProvider.GetFormRightsByFormID(FormID);
        return formRights;
    }


    public static DataSet GetFormByUserID(string UserID)
    {
        DataSet formRightss = new DataSet();
        SqlFormRightsProvider sqlFormRightsProvider = new SqlFormRightsProvider();
        formRightss = sqlFormRightsProvider.GetFormRightsByUserID(UserID);
        return formRightss;
    }


    public static FormRights GetFormRightsByFormRightsID(int FormRightsID)
    {
        FormRights formRights = new FormRights();
        SqlFormRightsProvider sqlFormRightsProvider = new SqlFormRightsProvider();
        formRights = sqlFormRightsProvider.GetFormRightsByFormRightsID(FormRightsID);
        return formRights;
    }

    public static FormRights GetFormRightsByUserIDFormID(string FormID, string userID)
    {
        FormRights formRights = new FormRights();
        SqlFormRightsProvider sqlFormRightsProvider = new SqlFormRightsProvider();
        formRights = sqlFormRightsProvider.GetFormRightsByUserIDFormID(FormID, userID);
        return formRights;
    }

    public static int InsertFormRights(FormRights formRights)
    {
        SqlFormRightsProvider sqlFormRightsProvider = new SqlFormRightsProvider();
        return sqlFormRightsProvider.InsertFormRights(formRights);
    }


    public static bool UpdateFormRights(FormRights formRights)
    {
        SqlFormRightsProvider sqlFormRightsProvider = new SqlFormRightsProvider();
        return sqlFormRightsProvider.UpdateFormRights(formRights);
    }

    public static bool DeleteFormRights(int formRightsID)
    {
        SqlFormRightsProvider sqlFormRightsProvider = new SqlFormRightsProvider();
        return sqlFormRightsProvider.DeleteFormRights(formRightsID);
    }
}

