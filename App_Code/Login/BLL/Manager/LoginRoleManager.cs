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

public class LoginRoleManager
{
	public LoginRoleManager()
	{
	}

    public static List<LoginRole> GetAllLoginRoles()
    {
        List<LoginRole> loginRoles = new List<LoginRole>();
        SqlLoginRoleProvider sqlLoginRoleProvider = new SqlLoginRoleProvider();
        loginRoles = sqlLoginRoleProvider.GetAllLoginRoles();
        return loginRoles;
    }

    public static List<LoginRole> GetAllLoginRolesByLoginID(int LoginID)
    {
        List<LoginRole> loginRoles = new List<LoginRole>();
        SqlLoginRoleProvider sqlLoginRoleProvider = new SqlLoginRoleProvider();
        loginRoles = sqlLoginRoleProvider.GetAllLoginRolesByLoginID(LoginID);
        return loginRoles;
    }


    public static LoginRole GetLoginRoleByID(int id)
    {
        LoginRole loginRole = new LoginRole();
        SqlLoginRoleProvider sqlLoginRoleProvider = new SqlLoginRoleProvider();
        loginRole = sqlLoginRoleProvider.GetLoginRoleByID(id);
        return loginRole;
    }


    public static int InsertLoginRole(LoginRole loginRole)
    {
        SqlLoginRoleProvider sqlLoginRoleProvider = new SqlLoginRoleProvider();
        return sqlLoginRoleProvider.InsertLoginRole(loginRole);
    }


    public static bool UpdateLoginRole(LoginRole loginRole)
    {
        SqlLoginRoleProvider sqlLoginRoleProvider = new SqlLoginRoleProvider();
        return sqlLoginRoleProvider.UpdateLoginRole(loginRole);
    }

    public static bool UpdateLoginRoleByIDs(LoginRole loginRole,string RoleIDs)
    {
        SqlLoginRoleProvider sqlLoginRoleProvider = new SqlLoginRoleProvider();
        return sqlLoginRoleProvider.UpdateLoginRoleByIDs(loginRole,RoleIDs);
    }

    public static bool DeleteLoginRole(int loginRoleID)
    {
        SqlLoginRoleProvider sqlLoginRoleProvider = new SqlLoginRoleProvider();
        return sqlLoginRoleProvider.DeleteLoginRole(loginRoleID);
    }
}
