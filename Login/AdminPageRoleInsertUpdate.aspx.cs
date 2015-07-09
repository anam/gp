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

public partial class AdminPageRoleInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPage();
            loadRole();
            loadRowStatus();
            if (Request.QueryString["pageRoleID"] != null)
            {
                int pageRoleID = Int32.Parse(Request.QueryString["pageRoleID"]);
                if (pageRoleID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showPageRoleData();
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

        PageRole pageRole = new PageRole();

        pageRole.PageID = Int32.Parse(ddlPage.SelectedValue);
        pageRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
        pageRole.AddedDate = DateTime.Now;
        pageRole.AddedBy = loginID;
        pageRole.ModifyDate = DateTime.Now;
        pageRole.ModifyBy = loginID;
        pageRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = PageRoleManager.InsertPageRole(pageRole);
        Response.Redirect("AdminPageRoleDisplay.aspx");
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

        PageRole pageRole = new PageRole();
        pageRole = PageRoleManager.GetPageRoleByID(Int32.Parse(Request.QueryString["pageRoleID"]));
        PageRole tempPageRole = new PageRole();
        tempPageRole.PageRoleID = pageRole.PageRoleID;

        tempPageRole.PageID = Int32.Parse(ddlPage.SelectedValue);
        tempPageRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
        tempPageRole.AddedDate = DateTime.Now;
        tempPageRole.AddedBy = loginID;
        tempPageRole.ModifyDate = DateTime.Now;
        tempPageRole.ModifyBy = loginID;
        tempPageRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = PageRoleManager.UpdatePageRole(tempPageRole);
        Response.Redirect("AdminPageRoleDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlPage.SelectedIndex = 0;
        ddlRole.SelectedIndex = 0;
        txtAddedBy.Text = "";
        txtModifyDate.Text = "";
        txtModifyBy.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showPageRoleData()
    {
        PageRole pageRole = new PageRole();
        pageRole = PageRoleManager.GetPageRoleByID(Int32.Parse(Request.QueryString["pageRoleID"]));

        ddlPage.SelectedValue = pageRole.PageID.ToString();
        ddlRole.SelectedValue = pageRole.RoleID.ToString();
        txtAddedBy.Text = pageRole.AddedBy;
        txtModifyDate.Text = pageRole.ModifyDate.ToString("dd MMM yyyy hh:mm tt");
        txtModifyBy.Text = pageRole.ModifyBy;
        ddlRowStatus.SelectedValue = pageRole.RowStatusID.ToString();
    }
    private void loadPage()
    {
        ListItem li = new ListItem("Select Page...", "0");
        ddlPage.Items.Add(li);

        List<Page> pages = new List<Page>();
        pages = PageManager.GetAllPages();
        foreach (Page page in pages)
        {
            ListItem item = new ListItem(page.PageName.ToString(), page.PageID.ToString());
            ddlPage.Items.Add(item);
        }
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
