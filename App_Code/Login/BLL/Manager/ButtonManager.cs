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

public class ButtonManager
{
	public ButtonManager()
	{
	}

    public static List<Button> GetAllButtons()
    {
        List<Button> buttons = new List<Button>();
        SqlButtonProvider sqlButtonProvider = new SqlButtonProvider();
        buttons = sqlButtonProvider.GetAllButtons();
        return buttons;
    }

    public static List<Button> GetAllButtonsByPageID(int PageID)
    {
        List<Button> buttons = new List<Button>();
        SqlButtonProvider sqlButtonProvider = new SqlButtonProvider();
        buttons = sqlButtonProvider.GetAllButtonsByPageID(PageID);
        return buttons;
    }

    public static List<Button> GetAllButtonsByPageURLnUserID(string pageURL,int loginID)
    {
        pageURL = pageURL.Split('/')[pageURL.Split('/').Length - 1].Contains('?') ? pageURL.Split('/')[pageURL.Split('/').Length - 1].Split('?')[0] : pageURL.Split('/')[pageURL.Split('/').Length - 1];

        List<Button> buttons = new List<Button>();
        SqlButtonProvider sqlButtonProvider = new SqlButtonProvider();
        buttons = sqlButtonProvider.GetAllButtonsByPageURLnUserID(pageURL,loginID);
        return buttons;
    }

    public static bool GetAllButtonsByPageURLnUserIDnButtonName(string buttonName,string pageURL, string loginID)
    {
        pageURL = pageURL.Split('/')[pageURL.Split('/').Length - 1].Contains('?') ? pageURL.Split('/')[pageURL.Split('/').Length - 1].Split('?')[0] : pageURL.Split('/')[pageURL.Split('/').Length - 1];

        List<Button> buttons = new List<Button>();
        SqlButtonProvider sqlButtonProvider = new SqlButtonProvider();
        buttons = sqlButtonProvider.GetAllButtonsByPageURLnUserIDnButtonName(pageURL,int.Parse(loginID), buttonName);

        if (buttons.Count != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Button GetButtonByID(int id)
    {
        Button button = new Button();
        SqlButtonProvider sqlButtonProvider = new SqlButtonProvider();
        button = sqlButtonProvider.GetButtonByID(id);
        return button;
    }


    public static int InsertButton(Button button)
    {
        SqlButtonProvider sqlButtonProvider = new SqlButtonProvider();
        return sqlButtonProvider.InsertButton(button);
    }


    public static bool UpdateButton(Button button)
    {
        SqlButtonProvider sqlButtonProvider = new SqlButtonProvider();
        return sqlButtonProvider.UpdateButton(button);
    }

    public static bool DeleteButton(int buttonID)
    {
        SqlButtonProvider sqlButtonProvider = new SqlButtonProvider();
        return sqlButtonProvider.DeleteButton(buttonID);
    }
}
