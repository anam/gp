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

public class LoginManager
{
	public LoginManager()
	{
	}

    public static List<Login> GetAllLogins()
    {
        List<Login> logins = new List<Login>();
        SqlLoginProvider sqlLoginProvider = new SqlLoginProvider();
        logins = sqlLoginProvider.GetAllLogins();
        return logins;
    }

    public static DataSet GetAllLoginForMarketingUser()
    {
        SqlLoginProvider sqlLoginProvider = new SqlLoginProvider();
        return sqlLoginProvider.GetAllLoginForMarketingUser();
    }

    public static DataSet GetAllCloserWhoHasCustomer()
    {
        SqlLoginProvider sqlLoginProvider = new SqlLoginProvider();
        return sqlLoginProvider.GetAllCloserWhoHasCustomer();
    }


    public static List<Login> GetAllLoginsByLoginName(string LoginName)
    {
        List<Login> logins = new List<Login>();
        SqlLoginProvider sqlLoginProvider = new SqlLoginProvider();
        logins = sqlLoginProvider.GetAllLoginsByLoginName(LoginName);
        return logins;
    }

    public static Login GetLoginByID(int id)
    {
        Login login = new Login();
        SqlLoginProvider sqlLoginProvider = new SqlLoginProvider();
        login = sqlLoginProvider.GetLoginByID(id);
        return login;
    }

    public static Login GetLoginByLoginName(string loginName)
    {
        Login login = new Login();
        SqlLoginProvider sqlLoginProvider = new SqlLoginProvider();
        login = sqlLoginProvider.GetLoginByLoginName(loginName);
        return login;
    }


    public static int InsertLogin(Login login)
    {
        SqlLoginProvider sqlLoginProvider = new SqlLoginProvider();
        return sqlLoginProvider.InsertLogin(login);
    }


    public static bool UpdateLogin(Login login)
    {
        SqlLoginProvider sqlLoginProvider = new SqlLoginProvider();
        return sqlLoginProvider.UpdateLogin(login);
    }

    public static bool DeleteLogin(int loginID)
    {
        SqlLoginProvider sqlLoginProvider = new SqlLoginProvider();
        return sqlLoginProvider.DeleteLogin(loginID);
    }
}
