using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class DefaultMasterPage : System.Web.UI.MasterPage
{
    //public FormRights fright;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!this.Page.User.Identity.IsAuthenticated)
        //{
            
        //    Response.Redirect("~/login.aspx");
        //}

        //MembershipUser currentUser;
        //currentUser = Membership.GetUser();
        //if (currentUser == null)
        //    Response.Redirect("~/login.aspx");

        //if (currentUser.UserName != "admin")
        //{
        //    dvUserManagement.Visible = false;
        //}

        //var clientID = currentUser.ProviderUserKey.ToString();
        //string pageName = "AdminDisplayClient";
        //fright = FormRightsManager.GetFormRightsByUserIDFormID(pageName, clientID);
        
        //string name = HttpContext.Current.User.Identity.Name;
        //if (!string.IsNullOrEmpty(name))
        //{
        //    if (name.Equals("admin"))
        //    {
        //        //HyperLink1.NavigateUrl = "Admin.aspx";
        //    }
        //    else
        //    {
        //        //HyperLink1.NavigateUrl = "SalesAgent.aspx";
        //    }
        //}

        if (!IsPostBack)
        {
            loginCheck();
        }
    }

    private void loginCheck()
    {
        bool isloggedIN = false;

        if ((Session["Login"] != null))
        {
            try
            {
                Login login = LoginManager.GetLoginByLoginName(((Login)Session["Login"]).LoginName.ToString());
                if (login != null)
                {
                    Session["Login"] = login;
                    isloggedIN = true;
                }
                else
                {
                    isloggedIN = false;
                }

                lblLoginName.Text = login.FirstName + " " + login.MiddleName + " " + login.LastName;
                a_loginName.HRef =a_loginName.HRef.Split('=')[0]+"="+ login.LoginID.ToString();
            }
            catch (Exception ex)
            {
                isloggedIN = false;
            }
        }
        if ((Request.Browser.Cookies) && (Request.Cookies["LoginName"] != null))
        {

            HttpCookie aCookie = Request.Cookies["LoginName"];

            Login login = LoginManager.GetLoginByLoginName(Convert.ToString(Server.HtmlEncode(aCookie.Value)));
            if (login != null)
            {
                Session["Login"] = login;
                isloggedIN = true;
            }

            lblLoginName.Text = login.FirstName + " " + login.MiddleName + " " + login.LastName;
            a_loginName.HRef =a_loginName.HRef.Split('=')[0]+"="+ login.LoginID.ToString();
        }

        if (isloggedIN)
        {

        }
        else
        {
            Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect("../LoginPage.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Customer/DisplayCustomer.aspx?SearchKey=" + txtSearchKey.Text);
    }
}
