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

public partial class AdminRoleInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRowStatus();
            if (Request.QueryString["roleID"] != null)
            {
                int roleID = Int32.Parse(Request.QueryString["roleID"]);
                if (roleID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showRoleData();
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
        Role role = new Role();

        role.RoleName = txtRoleName.Text;
        role.RoleDescription = txtRoleDescription.Text;
        role.AddedDate = DateTime.Now;
        role.AddedBy = loginID;
        role.ModifyDate = DateTime.Now;
        role.ModifyBy = loginID;
        role.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = RoleManager.InsertRole(role);
        Response.Redirect("AdminRoleDisplay.aspx");
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

        Role role = new Role();
        role = RoleManager.GetRoleByID(Int32.Parse(Request.QueryString["roleID"]));
        Role tempRole = new Role();
        tempRole.RoleID = role.RoleID;

        tempRole.RoleName = txtRoleName.Text;
        tempRole.RoleDescription = txtRoleDescription.Text;
        tempRole.AddedDate = DateTime.Now;
        tempRole.AddedBy = loginID;
        tempRole.ModifyDate = DateTime.Now;
        tempRole.ModifyBy = loginID;
        tempRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = RoleManager.UpdateRole(tempRole);
        Response.Redirect("AdminRoleDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtRoleName.Text = "";
        txtRoleDescription.Text = "";
        txtAddedBy.Text = "";
        txtModifyDate.Text = "";
        txtModifyBy.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showRoleData()
    {
        Role role = new Role();
        role = RoleManager.GetRoleByID(Int32.Parse(Request.QueryString["roleID"]));

        txtRoleName.Text = role.RoleName;
        txtRoleDescription.Text = role.RoleDescription;
        txtAddedBy.Text = role.AddedBy;
        txtModifyDate.Text = role.ModifyDate.ToString("dd MMM yyyy hh:mm tt");
        txtModifyBy.Text = role.ModifyBy;
        ddlRowStatus.SelectedValue = role.RowStatusID.ToString();
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
