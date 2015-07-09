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

public partial class AdminModuleInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadRowStatus();
            ddlRowStatus.SelectedValue = "1";
            if (Request.QueryString["moduleID"] != null)
            {
                int moduleID = Int32.Parse(Request.QueryString["moduleID"]);
                if (moduleID == 0)
                {
                    btnUpdate.Visible = false;
                    btnAdd.Visible = true;
                }
                else
                {
                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                    showModuleData();
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

        Module module = new Module();

        module.ModuleName = txtModuleName.Text;
        module.FolderName = txtFolderName.Text;
        module.DefaultURL = txtDefaultURL.Text;
        module.AddedBy = loginID;
        module.AddedDate = DateTime.Now;
        module.UpdatedBy = loginID;
        module.UpdatedDate = DateTime.Now;
        module.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ModuleManager.InsertModule(module);
        Response.Redirect("AdminModuleDisplay.aspx");
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

        Module module = new Module();
        module = ModuleManager.GetModuleByID(Int32.Parse(Request.QueryString["moduleID"]));
        Module tempModule = new Module();
        tempModule.ModuleID = module.ModuleID;

        tempModule.ModuleName = txtModuleName.Text;
        tempModule.FolderName = txtFolderName.Text;
        tempModule.DefaultURL = txtDefaultURL.Text;
        tempModule.AddedBy = loginID;
        tempModule.AddedDate = DateTime.Now;
        tempModule.UpdatedBy = loginID;
        tempModule.UpdatedDate = DateTime.Now;
        tempModule.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ModuleManager.UpdateModule(tempModule);
        Response.Redirect("AdminModuleDisplay.aspx");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtModuleName.Text = "";
        txtDefaultURL.Text = "";
        txtFolderName.Text = "";
        ddlRowStatus.SelectedIndex = 0;
    }
    private void showModuleData()
    {
        Module module = new Module();
        module = ModuleManager.GetModuleByID(Int32.Parse(Request.QueryString["moduleID"]));

        txtModuleName.Text = module.ModuleName;
        txtFolderName.Text = module.FolderName;
        txtDefaultURL.Text = module.DefaultURL;
        ddlRowStatus.SelectedValue = module.RowStatusID.ToString();
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
