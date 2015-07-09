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

public partial class AdminModuleRoleInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadModule();
            loadRole();
            loadRowStatus();
            if (Request.QueryString["moduleRoleID"] != null)
            {
                int moduleRoleID = Int32.Parse(Request.QueryString["moduleRoleID"]);
                if (moduleRoleID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showModuleRoleData();
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

        ModuleRole moduleRole = new ModuleRole();

        moduleRole.ModuleID = Int32.Parse(ddlModule.SelectedValue);
        moduleRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
        moduleRole.AddedDate = DateTime.Now;
        moduleRole.AddedBy = loginID;
        moduleRole.ModifyDate = DateTime.Now;
        moduleRole.ModifyBy = loginID;
        moduleRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ModuleRoleManager.InsertModuleRole(moduleRole);
        Response.Redirect("AdminModuleRoleDisplay.aspx");
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

        ModuleRole moduleRole = new ModuleRole();
        moduleRole = ModuleRoleManager.GetModuleRoleByID(Int32.Parse(Request.QueryString["moduleRoleID"]));
        ModuleRole tempModuleRole = new ModuleRole();
        tempModuleRole.ModuleRoleID = moduleRole.ModuleRoleID;

        tempModuleRole.ModuleID = Int32.Parse(ddlModule.SelectedValue);
        tempModuleRole.RoleID = Int32.Parse(ddlRole.SelectedValue);
        tempModuleRole.AddedDate = DateTime.Now;
        tempModuleRole.AddedBy = loginID;
        tempModuleRole.ModifyDate = DateTime.Now;
        tempModuleRole.ModifyBy = loginID;
        tempModuleRole.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ModuleRoleManager.UpdateModuleRole(tempModuleRole);
        Response.Redirect("AdminModuleRoleDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlModule.SelectedIndex = 0;
        ddlRole.SelectedIndex = 0;
        txtAddedBy.Text = "";
        txtModifyDate.Text = "";
        txtModifyBy.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showModuleRoleData()
    {
        ModuleRole moduleRole = new ModuleRole();
        moduleRole = ModuleRoleManager.GetModuleRoleByID(Int32.Parse(Request.QueryString["moduleRoleID"]));

        ddlModule.SelectedValue = moduleRole.ModuleID.ToString();
        ddlRole.SelectedValue = moduleRole.RoleID.ToString();
        txtAddedBy.Text = moduleRole.AddedBy;
        txtModifyDate.Text = moduleRole.ModifyDate.ToString("dd MMM yyyy hh:mm tt");
        txtModifyBy.Text = moduleRole.ModifyBy;
        ddlRowStatus.SelectedValue = moduleRole.RowStatusID.ToString();
    }
    private void loadModule()
    {
        ListItem li = new ListItem("Select Module...", "0");
        ddlModule.Items.Add(li);

        List<Module> modules = new List<Module>();
        modules = ModuleManager.GetAllModules();
        foreach (Module module in modules)
        {
            ListItem item = new ListItem(module.ModuleName.ToString(), module.ModuleID.ToString());
            ddlModule.Items.Add(item);
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
