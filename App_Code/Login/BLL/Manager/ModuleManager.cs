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

public class ModuleManager
{
	public ModuleManager()
	{
	}

    public static List<Module> GetAllModules()
    {
        List<Module> modules = new List<Module>();
        SqlModuleProvider sqlModuleProvider = new SqlModuleProvider();
        modules = sqlModuleProvider.GetAllModules();
        return modules;
    }

    public static List<Module> GetAllModulesSearch(string searchString)
    {
        List<Module> modules = new List<Module>();
        SqlModuleProvider sqlModuleProvider = new SqlModuleProvider();
        modules = sqlModuleProvider.GetAllModulesSearch(searchString);
        return modules;
    }

    public static Module GetModuleByID(int id)
    {
        Module module = new Module();
        SqlModuleProvider sqlModuleProvider = new SqlModuleProvider();
        module = sqlModuleProvider.GetModuleByID(id);
        return module;
    }


    public static int InsertModule(Module module)
    {
        SqlModuleProvider sqlModuleProvider = new SqlModuleProvider();
        return sqlModuleProvider.InsertModule(module);
    }


    public static bool UpdateModule(Module module)
    {
        SqlModuleProvider sqlModuleProvider = new SqlModuleProvider();
        return sqlModuleProvider.UpdateModule(module);
    }

    public static bool DeleteModule(int moduleID)
    {
        SqlModuleProvider sqlModuleProvider = new SqlModuleProvider();
        return sqlModuleProvider.DeleteModule(moduleID);
    }
}
