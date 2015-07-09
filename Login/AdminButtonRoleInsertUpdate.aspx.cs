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

public partial class AdminButtonRoleInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRole();
            loadButton();
            loadRowStatus();
            if (Request.QueryString["buttonRoleID"] != null)
            {
                int buttonRoleID = Int32.Parse(Request.QueryString["buttonRoleID"]);
                if (buttonRoleID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showButtonRoleData();
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

        ButtonRole buttonRole = new ButtonRole();

        buttonRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
        buttonRole.ButtonID = Int32.Parse(ddlButton.SelectedValue);
        buttonRole.AddedDate = DateTime.Now;
        buttonRole.AddedBy = loginID;
        buttonRole.ModifyDate = DateTime.Now;
        buttonRole.ModifyBy = loginID;
        buttonRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ButtonRoleManager.InsertButtonRole(buttonRole);
        Response.Redirect("AdminButtonRoleDisplay.aspx");
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
        ButtonRole buttonRole = new ButtonRole();
        buttonRole = ButtonRoleManager.GetButtonRoleByID(Int32.Parse(Request.QueryString["buttonRoleID"]));
        ButtonRole tempButtonRole = new ButtonRole();
        tempButtonRole.ButtonRoleID = buttonRole.ButtonRoleID;

        tempButtonRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
        tempButtonRole.ButtonID = Int32.Parse(ddlButton.SelectedValue);
        tempButtonRole.AddedDate = DateTime.Now;
        tempButtonRole.AddedBy = loginID;
        tempButtonRole.ModifyDate = DateTime.Now;
        tempButtonRole.ModifyBy = loginID;
        tempButtonRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ButtonRoleManager.UpdateButtonRole(tempButtonRole);
        Response.Redirect("AdminButtonRoleDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlRole.SelectedIndex = 0;
        ddlButton.SelectedIndex = 0;
        txtAddedBy.Text = "";
        txtModifyDate.Text = "";
        txtModifyBy.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showButtonRoleData()
    {
        ButtonRole buttonRole = new ButtonRole();
        buttonRole = ButtonRoleManager.GetButtonRoleByID(Int32.Parse(Request.QueryString["buttonRoleID"]));

        ddlRole.SelectedValue = buttonRole.RoleID.ToString();
        ddlButton.SelectedValue = buttonRole.ButtonID.ToString();
        txtAddedBy.Text = buttonRole.AddedBy;
        txtModifyDate.Text = buttonRole.ModifyDate.ToString("dd MMM yyyy hh:mm tt");
        txtModifyBy.Text = buttonRole.ModifyBy;
        ddlRowStatus.SelectedValue = buttonRole.RowStatusID.ToString();
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
    private void loadButton()
    {
        ListItem li = new ListItem("Select Button...", "0");
        ddlButton.Items.Add(li);

        List<Button> buttons = new List<Button>();
        buttons = ButtonManager.GetAllButtons();
        foreach (Button button in buttons)
        {
            ListItem item = new ListItem(button.ButtonName.ToString(), button.ButtonID.ToString());
            ddlButton.Items.Add(item);
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
