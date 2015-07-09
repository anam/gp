using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

public partial class AdminLoginDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showLoginGrid();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("LoginCreateAfterLogin.aspx?loginID=0");
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        int id;
        id = Convert.ToInt32(linkButton.CommandArgument);
        Response.Redirect("LoginCreateAfterLogin.aspx?loginID=" + id);
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = LoginManager.DeleteLogin(Convert.ToInt32(linkButton.CommandArgument));
        showLoginGrid();
    }
    private Login getLogin()
    {
        Login login = new Login();
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

            login = (Login)Session["Login"];
        }
        catch (Exception ex)
        { }

        return login;
    }

    private void showLoginGrid()
    {
        List<Login> allTheUsers = LoginManager.GetAllLogins();
        List<Login> allTheUsersNewlyAdded = new List<Login>();
        Login login = getLogin();
        if (!("," + login.ExtraField1 + ",").Contains(",1,"))
        {
            foreach (Login item in allTheUsers)
            {
                bool needToadd = true;
                if (("," + item.ExtraField1 + ",").Contains(",1,") || ("," + item.ExtraField1 + ",").Contains(",2,"))
                {
                    needToadd = false;
                }
                else
                {
                    bool needToDelete = true;
                    foreach (string propertyID in login.ExtraField3.Split(','))
                    {
                        if (("," + item.ExtraField3 + ",").Contains("," + propertyID + ","))
                        {
                            needToDelete = false;
                            break;
                        }
                    }
                    if (needToDelete)
                    {
                        needToadd = false;
                    }
                }

                if (needToadd)
                {
                    allTheUsersNewlyAdded.Add(item);
                }
            }
        }
        else
        {
            allTheUsersNewlyAdded = allTheUsers;
        }
        gvLogin.DataSource = allTheUsersNewlyAdded;
        gvLogin.DataBind();
    }
}
