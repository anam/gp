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

public class RoleManager
{
	public RoleManager()
	{
	}

    public static List<Role> GetAllRoles()
    {
        List<Role> roles = new List<Role>();
        SqlRoleProvider sqlRoleProvider = new SqlRoleProvider();
        roles = sqlRoleProvider.GetAllRoles();
        return roles;
    }

    public static DataSet GetModuleNMenuNPageNButtonByRoleID(int RoleID)
    {
        SqlRoleProvider sqlRoleProvider = new SqlRoleProvider();
        return sqlRoleProvider.GetModuleNMenuNPageNButtonByRoleID(RoleID);
        
    }


    public static Role GetRoleByID(int id)
    {
        Role role = new Role();
        SqlRoleProvider sqlRoleProvider = new SqlRoleProvider();
        role = sqlRoleProvider.GetRoleByID(id);
        return role;
    }


    public static int InsertRole(Role role)
    {
        SqlRoleProvider sqlRoleProvider = new SqlRoleProvider();
        return sqlRoleProvider.InsertRole(role);
    }


    public static bool UpdateRole(Role role)
    {
        SqlRoleProvider sqlRoleProvider = new SqlRoleProvider();
        return sqlRoleProvider.UpdateRole(role);
    }

    public static bool DeleteRole(int roleID)
    {
        SqlRoleProvider sqlRoleProvider = new SqlRoleProvider();
        return sqlRoleProvider.DeleteRole(roleID);
    }

    public static bool DeleteModuleMenuPageButtonRole(int roleID)
    {
        SqlRoleProvider sqlRoleProvider = new SqlRoleProvider();
        return sqlRoleProvider.DeleteModuleMenuPageButtonRole(roleID);
    }
}
