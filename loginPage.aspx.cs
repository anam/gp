using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request.QueryString["logout"] != null)
            {
                btnLogout_OnClick(this, new EventArgs());
            }
            else
            {
                //loginCheck();
            }
        }
    }

    private void loginCheck()
    {
        bool isloggedIN = false;
        Login login = new Login();
        if ((Session["Login"] != null))
        {
            try
            {
                login = LoginManager.GetLoginByLoginName(((Login)Session["Login"]).LoginName.ToString());
                if (login != null)
                {
                    Session["Login"] = login;
                    isloggedIN = true;
                }
                else
                {
                    lblMsg.Text = "<br/>You are not registered User.";
                    string errorMessage = "You are not registered User.";
                    String str = "<script> alert('" + errorMessage + "'); </Script>";
                    //Response.Write(str);
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = "<br/>You are not registered User.";
                string errorMessage = "You are not registered User.";
                String str = "<script> alert('" + errorMessage + "'); </Script>";
                //Response.Write(str);

            }
        }
        if ((Request.Browser.Cookies) && (Request.Cookies["LoginName"] != null))
        {

            HttpCookie aCookie = Request.Cookies["LoginName"];

            login = LoginManager.GetLoginByLoginName(Convert.ToString(Server.HtmlEncode(aCookie.Value)));
            if (login != null)
            {
                Session["Login"] = login;
                isloggedIN = true;
            }
        }

        if (isloggedIN) Response.Redirect(login.ExtraField2 == "" ? "Default.aspx" : login.ExtraField2);
    }

    protected void btnLogin_OnClick(object sender, EventArgs e)
    {
        try
        {
            Login login = LoginManager.GetLoginByLoginName(txtLoginName.Text);
            if (login != null)
            {
                if (login.Password == txtPassword.Text)
                {
                    Session["Login"] = login;
                    if (chkRememberme.Checked)
                    {
                        HttpCookie MyGreatCookie = new HttpCookie("LoginName");
                        MyGreatCookie.Value = login.LoginName.ToString();
                        MyGreatCookie.Expires = DateTime.Now.AddDays(100);
                        Response.Cookies.Add(MyGreatCookie);

                    }

                    if (Session["PreviousPage"] != null)
                    {
                        string tmp = Session["PreviousPage"].ToString();
                        Session.Remove("PreviousPage");
                        Response.Redirect(tmp);
                    }
                    else
                    {
                        Response.Redirect(login.ExtraField2 == "" ? "Default.aspx" : login.ExtraField2);
                    }
                }
                else
                {
                    lblMsg.Text = "<br/>Your Password is not match.";
                    string errorMessage = "Your Password is not match.";
                    String str = "<script> alert('" + errorMessage + "'); </Script>";
                    //Response.Write(str);
                }

            }
            else
            {
                lblMsg.Text = "<br/>You are not registered User.";
                string errorMessage = "You are not registered User.";
                String str = "<script> alert('" + errorMessage + "'); </Script>";
                //Response.Write(str);
            }

        }
        catch (Exception ex)
        {
            string errorMessage = "You are not registered User.";
            lblMsg.Text = errorMessage;
            String str = "<script> alert('" + errorMessage + "'); </Script>";
            //Response.Write(str);

        }
    }

    protected void btnLogout_OnClick(object sender, EventArgs e)
    {
        Session.Abandon();
        Session["Login"] = null;
        if (Request.Cookies["LoginName"] != null)
        {
            HttpCookie myCookie = new HttpCookie("LoginName");
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(myCookie);
        }
       // string str = Request.Url.ToString();
        Response.Redirect("LoginPage.aspx");
    }
}