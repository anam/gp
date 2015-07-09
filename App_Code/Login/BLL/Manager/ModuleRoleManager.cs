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

public class ModuleRoleManager
{
	public ModuleRoleManager()
	{
	}

    public static List<ModuleRole> GetAllModuleRoles()
    {
        List<ModuleRole> moduleRoles = new List<ModuleRole>();
        SqlModuleRoleProvider sqlModuleRoleProvider = new SqlModuleRoleProvider();
        moduleRoles = sqlModuleRoleProvider.GetAllModuleRoles();
        return moduleRoles;
    }


    public static ModuleRole GetModuleRoleByID(int id)
    {
        ModuleRole moduleRole = new ModuleRole();
        SqlModuleRoleProvider sqlModuleRoleProvider = new SqlModuleRoleProvider();
        moduleRole = sqlModuleRoleProvider.GetModuleRoleByID(id);
        return moduleRole;
    }


    public static int InsertModuleRole(ModuleRole moduleRole)
    {
        SqlModuleRoleProvider sqlModuleRoleProvider = new SqlModuleRoleProvider();
        return sqlModuleRoleProvider.InsertModuleRole(moduleRole);
    }


    public static bool UpdateModuleRole(ModuleRole moduleRole)
    {
        SqlModuleRoleProvider sqlModuleRoleProvider = new SqlModuleRoleProvider();
        return sqlModuleRoleProvider.UpdateModuleRole(moduleRole);
    }

    public static bool DeleteModuleRole(int moduleRoleID)
    {
        SqlModuleRoleProvider sqlModuleRoleProvider = new SqlModuleRoleProvider();
        return sqlModuleRoleProvider.DeleteModuleRole(moduleRoleID);
    }
}
