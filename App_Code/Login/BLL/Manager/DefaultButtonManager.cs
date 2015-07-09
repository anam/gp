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

public class DefaultButtonManager
{
	public DefaultButtonManager()
	{
	}

    public static List<DefaultButton> GetAllDefaultButtons()
    {
        List<DefaultButton> defaultButtons = new List<DefaultButton>();
        SqlDefaultButtonProvider sqlDefaultButtonProvider = new SqlDefaultButtonProvider();
        defaultButtons = sqlDefaultButtonProvider.GetAllDefaultButtons();
        return defaultButtons;
    }


    public static DefaultButton GetDefaultButtonByID(int id)
    {
        DefaultButton defaultButton = new DefaultButton();
        SqlDefaultButtonProvider sqlDefaultButtonProvider = new SqlDefaultButtonProvider();
        defaultButton = sqlDefaultButtonProvider.GetDefaultButtonByID(id);
        return defaultButton;
    }


    public static int InsertDefaultButton(DefaultButton defaultButton)
    {
        SqlDefaultButtonProvider sqlDefaultButtonProvider = new SqlDefaultButtonProvider();
        return sqlDefaultButtonProvider.InsertDefaultButton(defaultButton);
    }


    public static bool UpdateDefaultButton(DefaultButton defaultButton)
    {
        SqlDefaultButtonProvider sqlDefaultButtonProvider = new SqlDefaultButtonProvider();
        return sqlDefaultButtonProvider.UpdateDefaultButton(defaultButton);
    }

    public static bool DeleteDefaultButton(int defaultButtonID)
    {
        SqlDefaultButtonProvider sqlDefaultButtonProvider = new SqlDefaultButtonProvider();
        return sqlDefaultButtonProvider.DeleteDefaultButton(defaultButtonID);
    }
}
