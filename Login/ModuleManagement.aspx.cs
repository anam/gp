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

public partial class AdminModuleDisplay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showModuleGrid();
            loadRowStatus();

            ddlRowStatus.SelectedValue = "1";
            btnAdd.Visible = true;
            btnUpdate.Visible = false;

            if (Request.QueryString["ModuleID"] != null)
            {
                loadSingleModule(Request.QueryString["ModuleID"]);
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
        module.MenuOrder = decimal.Parse(txtMenuOrder.Text);
        module.AddedBy = loginID;
        module.AddedDate = DateTime.Now;
        module.UpdatedBy = loginID;
        module.UpdatedDate = DateTime.Now;
        module.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ModuleManager.InsertModule(module);
        showModuleGrid();
        btnClear_Click(this, new EventArgs());
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
        module = ModuleManager.GetModuleByID(Int32.Parse(hfModuleID.Value));
        Module tempModule = new Module();
        tempModule.ModuleID = module.ModuleID;

        tempModule.ModuleName = txtModuleName.Text;
        tempModule.FolderName = txtFolderName.Text;
        tempModule.DefaultURL = txtDefaultURL.Text;
        tempModule.MenuOrder = decimal.Parse(txtMenuOrder.Text);
        tempModule.AddedBy = loginID;
        tempModule.AddedDate = DateTime.Now;
        tempModule.UpdatedBy = loginID;
        tempModule.UpdatedDate = DateTime.Now;
        tempModule.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ModuleManager.UpdateModule(tempModule);
        showModuleGrid();
        btnClear_Click(this, new EventArgs());
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtModuleName.Text = "";
        txtDefaultURL.Text = "";
        txtFolderName.Text = "";
        txtMenuOrder.Text = "1";
        ddlRowStatus.SelectedIndex = 0;
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
        btnClear.Visible = false;
    }
    protected void lbSelect_Click(object sender, EventArgs e)
    {
        loadSingleModule(((LinkButton)sender).CommandArgument);
        
    }

    private void loadSingleModule(string ModuleID)
    {
        Module module = new Module();
        module = ModuleManager.GetModuleByID(Int32.Parse(ModuleID));

        txtModuleName.Text = module.ModuleName;
        txtFolderName.Text = module.FolderName;
        txtDefaultURL.Text = module.DefaultURL;
        txtMenuOrder.Text = module.MenuOrder.ToString("0.00");
        ddlRowStatus.SelectedValue = module.RowStatusID.ToString();
        hfModuleID.Value = module.ModuleID.ToString();
        
        btnAdd.Visible = false;
        btnClear.Visible = true;
        btnUpdate.Visible = true;
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = ModuleManager.DeleteModule(Convert.ToInt32(linkButton.CommandArgument));
        showModuleGrid();
    }

    private void showModuleGrid()
    {
        gvModule.DataSource = ModuleManager.GetAllModules();
        gvModule.DataBind();
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
