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

public class PageRoleManager
{
	public PageRoleManager()
	{
	}

    public static List<PageRole> GetAllPageRoles()
    {
        List<PageRole> pageRoles = new List<PageRole>();
        SqlPageRoleProvider sqlPageRoleProvider = new SqlPageRoleProvider();
        pageRoles = sqlPageRoleProvider.GetAllPageRoles();
        return pageRoles;
    }


    public static PageRole GetPageRoleByID(int id)
    {
        PageRole pageRole = new PageRole();
        SqlPageRoleProvider sqlPageRoleProvider = new SqlPageRoleProvider();
        pageRole = sqlPageRoleProvider.GetPageRoleByID(id);
        return pageRole;
    }


    public static int InsertPageRole(PageRole pageRole)
    {
        SqlPageRoleProvider sqlPageRoleProvider = new SqlPageRoleProvider();
        return sqlPageRoleProvider.InsertPageRole(pageRole);
    }


    public static bool UpdatePageRole(PageRole pageRole)
    {
        SqlPageRoleProvider sqlPageRoleProvider = new SqlPageRoleProvider();
        return sqlPageRoleProvider.UpdatePageRole(pageRole);
    }

    public static bool DeletePageRole(int pageRoleID)
    {
        SqlPageRoleProvider sqlPageRoleProvider = new SqlPageRoleProvider();
        return sqlPageRoleProvider.DeletePageRole(pageRoleID);
    }
}
