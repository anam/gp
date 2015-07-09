using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public class PageManager
{
	public PageManager()
	{
	}

    public static List<Page> GetAllPages()
    {
        List<Page> pages = new List<Page>();
        SqlPageProvider sqlPageProvider = new SqlPageProvider();
        pages = sqlPageProvider.GetAllPages();
        return pages;
    }

    public static List<Page> GetAllPagesByModuleID(int moduleID)
    {
        List<Page> pages = new List<Page>();
        SqlPageProvider sqlPageProvider = new SqlPageProvider();
        pages = sqlPageProvider.GetAllPagesByModuleID(moduleID);
        return pages;
    }

    public static DataSet GetAllPagenMenuByModuleID(int ModuleID)
    {
        SqlPageProvider sqlPageProvider = new SqlPageProvider();
        return sqlPageProvider.GetAllPagenMenuByModuleID(ModuleID);
    }

    public static Page GetPageByID(int id)
    {
        Page page = new Page();
        SqlPageProvider sqlPageProvider = new SqlPageProvider();
        page = sqlPageProvider.GetPageByID(id);
        return page;
    }


    public static int InsertPage(Page page)
    {
        SqlPageProvider sqlPageProvider = new SqlPageProvider();
        return sqlPageProvider.InsertPage(page);
    }


    public static bool UpdatePage(Page page)
    {
        SqlPageProvider sqlPageProvider = new SqlPageProvider();
        return sqlPageProvider.UpdatePage(page);
    }

    public static bool DeletePage(int pageID)
    {
        SqlPageProvider sqlPageProvider = new SqlPageProvider();
        return sqlPageProvider.DeletePage(pageID);
    }
}
