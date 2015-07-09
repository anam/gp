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

public partial class AdminMenuRoleInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadMenu();
            loadRole();
            loadRowStatus();
            if (Request.QueryString["menuRoleID"] != null)
            {
                int menuRoleID = Int32.Parse(Request.QueryString["menuRoleID"]);
                if (menuRoleID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showMenuRoleData();
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

        MenuRole menuRole = new MenuRole();

        menuRole.MenuID = Int32.Parse(ddlMenu.SelectedValue);
        menuRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
        menuRole.AddedDate = DateTime.Now;
        menuRole.AddedBy = loginID;
        menuRole.ModifyDate = DateTime.Now;
        menuRole.ModifyBy = loginID;
        menuRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = MenuRoleManager.InsertMenuRole(menuRole);
        Response.Redirect("AdminMenuRoleDisplay.aspx");
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

        MenuRole menuRole = new MenuRole();
        menuRole = MenuRoleManager.GetMenuRoleByID(Int32.Parse(Request.QueryString["menuRoleID"]));
        MenuRole tempMenuRole = new MenuRole();
        tempMenuRole.MenuRoleID = menuRole.MenuRoleID;

        tempMenuRole.MenuID = Int32.Parse(ddlMenu.SelectedValue);
        tempMenuRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
        tempMenuRole.AddedDate = DateTime.Now;
        tempMenuRole.AddedBy = loginID;
        tempMenuRole.ModifyDate = DateTime.Now;
        tempMenuRole.ModifyBy = loginID;
        tempMenuRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = MenuRoleManager.UpdateMenuRole(tempMenuRole);
        Response.Redirect("AdminMenuRoleDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlMenu.SelectedIndex = 0;
        ddlRole.SelectedIndex = 0;
        txtAddedBy.Text = "";
        txtModifyDate.Text = "";
        txtModifyBy.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showMenuRoleData()
    {
        MenuRole menuRole = new MenuRole();
        menuRole = MenuRoleManager.GetMenuRoleByID(Int32.Parse(Request.QueryString["menuRoleID"]));

        ddlMenu.SelectedValue = menuRole.MenuID.ToString();
        ddlRole.SelectedValue = menuRole.RoleID.ToString();
        txtAddedBy.Text = menuRole.AddedBy;
        txtModifyDate.Text = menuRole.ModifyDate.ToString("dd MMM yyyy hh:mm tt");
        txtModifyBy.Text = menuRole.ModifyBy;
        ddlRowStatus.SelectedValue = menuRole.RowStatusID.ToString();
    }
    private void loadMenu()
    {
        ListItem li = new ListItem("Select Menu...", "0");
        ddlMenu.Items.Add(li);

        List<Menu> menus = new List<Menu>();
        menus = MenuManager.GetAllMenus();
        foreach (Menu menu in menus)
        {
            ListItem item = new ListItem(menu.MenuTitle.ToString(), menu.MenuID.ToString());
            ddlMenu.Items.Add(item);
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
