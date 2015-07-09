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

public class MenuManager
{
	public MenuManager()
	{
	}

    public static List<Menu> GetAllMenus()
    {
        List<Menu> menus = new List<Menu>();
        SqlMenuProvider sqlMenuProvider = new SqlMenuProvider();
        menus = sqlMenuProvider.GetAllMenus();
        return menus;
    }

    public static DataSet GetMenuByLoginID(int LoginID)
    {
        SqlMenuProvider sqlMenuProvider = new SqlMenuProvider();
        return sqlMenuProvider.GetMenuByLoginID(LoginID);
    }

    public static DataSet GetMenuddl()
    {
        SqlMenuProvider sqlMenuProvider = new SqlMenuProvider();
        return sqlMenuProvider.GetMenuddl();
    }


    public static List<Menu> GetAllMenusByModuleID(int moduleID)
    {
        List<Menu> menus = new List<Menu>();
        SqlMenuProvider sqlMenuProvider = new SqlMenuProvider();
        menus = sqlMenuProvider.GetAllMenusByModuleID(moduleID);
        return menus;
    }

    public static Menu GetMenuByID(int id)
    {
        Menu menu = new Menu();
        SqlMenuProvider sqlMenuProvider = new SqlMenuProvider();
        menu = sqlMenuProvider.GetMenuByID(id);
        return menu;
    }


    public static int InsertMenu(Menu menu)
    {
        SqlMenuProvider sqlMenuProvider = new SqlMenuProvider();
        return sqlMenuProvider.InsertMenu(menu);
    }


    public static bool UpdateMenu(Menu menu)
    {
        SqlMenuProvider sqlMenuProvider = new SqlMenuProvider();
        return sqlMenuProvider.UpdateMenu(menu);
    }

    public static bool DeleteMenu(int menuID)
    {
        SqlMenuProvider sqlMenuProvider = new SqlMenuProvider();
        return sqlMenuProvider.DeleteMenu(menuID);
    }
}
