using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class AdminLoginRoleInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRole();
            loadLogin();
            loadRowStatus();
            if (Request.QueryString["loginRoleID"] != null)
            {
                int loginRoleID = Int32.Parse(Request.QueryString["loginRoleID"]);
                if (loginRoleID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showLoginRoleData();
                }
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string loginID = "1";
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }
            
            loginID = ((Login)Session["Login"]).LoginID.ToString();
        }
        catch (Exception ex)
        { }
        LoginRole loginRole = new LoginRole();

        loginRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
        loginRole.LoginID = Int32.Parse(ddlLogin.SelectedValue);
        loginRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        loginRole.AddedDate = DateTime.Now;
        loginRole.AddedBy = loginID;
        loginRole.ModifyDate = DateTime.Now;
        loginRole.ModifyBy = loginID;
        int resutl = LoginRoleManager.InsertLoginRole(loginRole);
        Response.Redirect("AdminLoginRoleDisplay.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string loginID = "1";
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }
            
            loginID = ((Login)Session["Login"]).LoginID.ToString();
        }
        catch (Exception ex)
        { }

        LoginRole loginRole = new LoginRole();
        loginRole = LoginRoleManager.GetLoginRoleByID(Int32.Parse(Request.QueryString["loginRoleID"]));
        LoginRole tempLoginRole = new LoginRole();
        tempLoginRole.LoginRoleID = loginRole.LoginRoleID;

        tempLoginRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
        tempLoginRole.LoginID = Int32.Parse(ddlLogin.SelectedValue);
        tempLoginRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        tempLoginRole.AddedDate = DateTime.Now;
        tempLoginRole.AddedBy = loginID;
        tempLoginRole.ModifyDate = DateTime.Now;
        tempLoginRole.ModifyBy = loginID;
        bool result = LoginRoleManager.UpdateLoginRole(tempLoginRole);
        Response.Redirect("AdminLoginRoleDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlRole.SelectedIndex = 0;
        ddlLogin.SelectedIndex = 0;
        ddlRowStatus.SelectedIndex = 0;
        txtAddedBy.Text = "";
        txtModifyDate.Text = "";
        txtModifyBy.Text = "";
    }
    private void showLoginRoleData()
    {
        LoginRole loginRole = new LoginRole();
        loginRole = LoginRoleManager.GetLoginRoleByID(Int32.Parse(Request.QueryString["loginRoleID"]));

        ddlRole.SelectedValue = loginRole.RoleID.ToString();
        ddlLogin.SelectedValue = loginRole.LoginID.ToString();
        ddlRowStatus.SelectedValue = loginRole.RowStatusID.ToString();
        txtAddedBy.Text = loginRole.AddedBy;
        txtModifyDate.Text = loginRole.ModifyDate.ToString("dd MMM yyyy hh:mm tt");
        txtModifyBy.Text = loginRole.ModifyBy;
    }
    private void loadRole()
    {
        ListItem li = new ListItem("Select Role...", "0");
        ddlRole.Items.Add(li);

        List<Role> roles = new List<Role>();
        roles = RoleManager.GetAllRoles();
        foreach (Role role in roles)
        {
            ListItem item = new ListItem(role.RoleName.ToString(), role.RoleID.ToString());
            ddlRole.Items.Add(item);
        }
    }
    private void loadLogin()
    {
        ListItem li = new ListItem("Select Login...", "0");
        ddlLogin.Items.Add(li);

        List<Login> logins = new List<Login>();
        logins = LoginManager.GetAllLogins();
        foreach (Login login in logins)
        {
            ListItem item = new ListItem(login.LoginName.ToString(), login.LoginID.ToString());
            ddlLogin.Items.Add(item);
        }
    }
    private void loadRowStatus()
    {
        ListItem li = new ListItem("Select RowStatus...", "0");
        ddlRowStatus.Items.Add(li);

        List<RowStatus> rowStatuss = new List<RowStatus>();
        rowStatuss = RowStatusManager.GetAllRowStatuss();
        foreach (RowStatus rowStatus in rowStatuss)
        {
            ListItem item = new ListItem(rowStatus.RowStatusName.ToString(), rowStatus.RowStatusID.ToString());
            ddlRowStatus.Items.Add(item);
        }
    }
}
