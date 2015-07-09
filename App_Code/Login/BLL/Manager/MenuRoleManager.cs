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

public class MenuRoleManager
{
	public MenuRoleManager()
	{
	}

    public static List<MenuRole> GetAllMenuRoles()
    {
        List<MenuRole> menuRoles = new List<MenuRole>();
        SqlMenuRoleProvider sqlMenuRoleProvider = new SqlMenuRoleProvider();
        menuRoles = sqlMenuRoleProvider.GetAllMenuRoles();
        return menuRoles;
    }


    public static MenuRole GetMenuRoleByID(int id)
    {
        MenuRole menuRole = new MenuRole();
        SqlMenuRoleProvider sqlMenuRoleProvider = new SqlMenuRoleProvider();
        menuRole = sqlMenuRoleProvider.GetMenuRoleByID(id);
        return menuRole;
    }


    public static int InsertMenuRole(MenuRole menuRole)
    {
        SqlMenuRoleProvider sqlMenuRoleProvider = new SqlMenuRoleProvider();
        return sqlMenuRoleProvider.InsertMenuRole(menuRole);
    }


    public static bool UpdateMenuRole(MenuRole menuRole)
    {
        SqlMenuRoleProvider sqlMenuRoleProvider = new SqlMenuRoleProvider();
        return sqlMenuRoleProvider.UpdateMenuRole(menuRole);
    }

    public static bool DeleteMenuRole(int menuRoleID)
    {
        SqlMenuRoleProvider sqlMenuRoleProvider = new SqlMenuRoleProvider();
        return sqlMenuRoleProvider.DeleteMenuRole(menuRoleID);
    }
}
