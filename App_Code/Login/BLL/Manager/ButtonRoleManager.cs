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

public class ButtonRoleManager
{
	public ButtonRoleManager()
	{
	}

    public static List<ButtonRole> GetAllButtonRoles()
    {
        List<ButtonRole> buttonRoles = new List<ButtonRole>();
        SqlButtonRoleProvider sqlButtonRoleProvider = new SqlButtonRoleProvider();
        buttonRoles = sqlButtonRoleProvider.GetAllButtonRoles();
        return buttonRoles;
    }


    public static ButtonRole GetButtonRoleByID(int id)
    {
        ButtonRole buttonRole = new ButtonRole();
        SqlButtonRoleProvider sqlButtonRoleProvider = new SqlButtonRoleProvider();
        buttonRole = sqlButtonRoleProvider.GetButtonRoleByID(id);
        return buttonRole;
    }


    public static int InsertButtonRole(ButtonRole buttonRole)
    {
        SqlButtonRoleProvider sqlButtonRoleProvider = new SqlButtonRoleProvider();
        return sqlButtonRoleProvider.InsertButtonRole(buttonRole);
    }


    public static bool UpdateButtonRole(ButtonRole buttonRole)
    {
        SqlButtonRoleProvider sqlButtonRoleProvider = new SqlButtonRoleProvider();
        return sqlButtonRoleProvider.UpdateButtonRole(buttonRole);
    }

    public static bool DeleteButtonRole(int buttonRoleID)
    {
        SqlButtonRoleProvider sqlButtonRoleProvider = new SqlButtonRoleProvider();
        return sqlButtonRoleProvider.DeleteButtonRole(buttonRoleID);
    }
}
