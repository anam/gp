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

public partial class AdminPageInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadModule();
            loadRowStatus();
            
            ddlRowStatus.SelectedValue = "1";

            btnAdd.Visible = true;
            btnClear.Visible = false;
            btnUpdate.Visible = false;

            if (Request.QueryString["ModuleID"] != null)
            {
                ddlModule.SelectedValue = Request.QueryString["ModuleID"];
                showAllPageByModuleID(Request.QueryString["ModuleID"]);
            }

            if (Request.QueryString["PageID"] != null)
            {
                loadSinglePage(int.Parse(Request.QueryString["PageID"]));
            }
        }
    }

    private void showAllPageByModuleID(string moduleID)
    {
        gvPage.DataSource = PageManager.GetAllPagesByModuleID(int.Parse(moduleID));
        gvPage.DataBind();
    }

    protected void lbSelect_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;

        loadSinglePage(int.Parse(linkButton.CommandArgument));

    }

    private void loadSinglePage(int PageID)
    {
        Page page = new Page();
        page = PageManager.GetPageByID(PageID);

        txtPageTitle.Text = page.PageTitle;
        txtPageURL.Text = page.PageURL;
        ddlModule.SelectedValue = page.ModuleID.ToString();
        ddlRowStatus.SelectedValue = page.RowStatusID.ToString();
        hfPageID.Value = page.PageID.ToString();

        btnAdd.Visible = false;
        btnClear.Visible = true;
        btnUpdate.Visible = true;
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = PageManager.DeletePage(Convert.ToInt32(linkButton.CommandArgument));
        showAllPageByModuleID(ddlModule.SelectedValue);
        btnClear_Click(this, new EventArgs());
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

        Page page = new Page();

        page.PageTitle = txtPageTitle.Text;
        page.PageURL = txtPageURL.Text;
        page.ModuleID = Int32.Parse(ddlModule.SelectedValue);
        page.AddedBy = loginID;
        page.AddedDate = DateTime.Now;
        page.UpdatedBy = loginID;
        page.UpdatedDate = DateTime.Now;
        page.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = PageManager.InsertPage(page);

        showAllPageByModuleID(ddlModule.SelectedValue);
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

        Page page = new Page();
        page = PageManager.GetPageByID(Int32.Parse(hfPageID.Value));
        Page tempPage = new Page();
        tempPage.PageID = page.PageID;

        tempPage.PageTitle = txtPageTitle.Text;
        tempPage.PageURL = txtPageURL.Text;
        tempPage.ModuleID = Int32.Parse(ddlModule.SelectedValue);
        tempPage.AddedBy = loginID;
        tempPage.AddedDate = DateTime.Now;
        tempPage.UpdatedBy = loginID;
        tempPage.UpdatedDate = DateTime.Now;
        tempPage.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = PageManager.UpdatePage(tempPage);

        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0] + "?ModuleID=" + ddlModule.SelectedValue);

        //showAllPageByModuleID(ddlModule.SelectedValue);
        //btnClear_Click(this, new EventArgs());
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPageTitle.Text = "";
        txtPageURL.Text = "";
        //ddlModule.SelectedIndex = 0;
        //ddlRowStatus.SelectedIndex = 0;
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
        btnClear.Visible = false;
    }
    private void showPageData()
    {
        Page page = new Page();
        page = PageManager.GetPageByID(Int32.Parse(Request.QueryString["pageID"]));

        txtPageTitle.Text = page.PageTitle;
        txtPageURL.Text = page.PageURL;
        ddlModule.SelectedValue = page.ModuleID.ToString();
        ddlRowStatus.SelectedValue = page.RowStatusID.ToString();
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
    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        showAllPageByModuleID(ddlModule.SelectedValue);
    }
}
