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

public partial class AdminMenuInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadModule();
            //loadPage();
            //loadParentMenu();
            loadRowStatus();

            ddlRowStatus.SelectedValue = "1";

            btnAdd.Visible = true;
            btnClear.Visible = false;
            btnUpdate.Visible = false;


            if (Request.QueryString["ModuleID"] != null)
            {
                ddlModule.SelectedValue = Request.QueryString["ModuleID"];
                ddlModule_SelectedIndexChanged(this, new EventArgs());
                showMenuDataByModuleID(ddlModule.SelectedValue);
            }

            if (Request.QueryString["PageID"] != null)
            {
                ddlPage.SelectedValue = Request.QueryString["PageID"];
            }

            if (Request.QueryString["MenuID"] != null)
            {
                loadSingleMenu(Request.QueryString["MenuID"]);
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

        Menu menu = new Menu();

        menu.MenuTitle = txtMenuTitle.Text;
        menu.ModuleID = Int32.Parse(ddlModule.SelectedValue);
        menu.PageID = Int32.Parse(ddlPage.SelectedValue);
        menu.ParentMenuID = Int32.Parse(ddlParentMenu.SelectedValue);
        menu.MenuOrder = decimal.Parse(txtMenuOrder.Text);
        menu.AddedBy = loginID;
        menu.AddedDate = DateTime.Now;
        menu.UpdatedBy = loginID;
        menu.UpdatedDate = DateTime.Now;
        menu.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = MenuManager.InsertMenu(menu);

        showMenuDataByModuleID(ddlModule.SelectedValue);
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
        Menu menu = new Menu();
        menu = MenuManager.GetMenuByID(Int32.Parse(hfMenuID.Value));
        Menu tempMenu = new Menu();
        tempMenu.MenuID = menu.MenuID;

        tempMenu.MenuTitle = txtMenuTitle.Text;
        tempMenu.ModuleID = Int32.Parse(ddlModule.SelectedValue);
        tempMenu.PageID = Int32.Parse(ddlPage.SelectedValue);
        tempMenu.ParentMenuID = Int32.Parse(ddlParentMenu.SelectedValue);
        tempMenu.MenuOrder = decimal.Parse(txtMenuOrder.Text);
        tempMenu.AddedBy = loginID;
        tempMenu.AddedDate = DateTime.Now;
        tempMenu.UpdatedBy = loginID;
        tempMenu.UpdatedDate = DateTime.Now;
        tempMenu.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = MenuManager.UpdateMenu(tempMenu);

        Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0]+"?ModuleID="+ddlModule.SelectedValue);

        //showMenuDataByModuleID(ddlModule.SelectedValue);
        //btnClear_Click(this, new EventArgs());
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtMenuTitle.Text = "";
        txtMenuOrder.Text = "1";
        //ddlModule.SelectedIndex = 0;
        //ddlPage.SelectedIndex = 0;
        //ddlParentMenu.SelectedIndex = 0;
        //ddlRowStatus.SelectedIndex = 0;

        btnAdd.Visible = true;
        btnUpdate.Visible = false;
        btnClear.Visible = false;
    }
    private void showMenuData()
    {
        Menu menu = new Menu();
        menu = MenuManager.GetMenuByID(Int32.Parse(Request.QueryString["menuID"]));

        txtMenuTitle.Text = menu.MenuTitle;
        ddlModule.SelectedValue = menu.ModuleID.ToString();
        loadPage();
        //ddlModule_SelectedIndexChanged(this, new EventArgs());
        ddlPage.SelectedValue = menu.PageID.ToString();
        ddlParentMenu.SelectedValue = menu.ParentMenuID.ToString();
        ddlRowStatus.SelectedValue = menu.RowStatusID.ToString();
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


    private void loadPage(int moduleID)
    {
        ddlPage.Items.Clear();

        ListItem li = new ListItem("Select Page...", "0");
        ddlPage.Items.Add(li);

        List<Page> pages = new List<Page>();
        pages = PageManager.GetAllPagesByModuleID(moduleID);
        foreach (Page page in pages)
        {
            ListItem item = new ListItem(page.PageTitle.ToString(), page.PageID.ToString());
            ddlPage.Items.Add(item);
        }

    }

    private void loadParentMenu(int moduleID)
    {
        

        ListItem li = new ListItem("No Parent Menu..", "0");
        ddlParentMenu.Items.Add(li);

        List<Menu> menus = new List<Menu>();
        menus = MenuManager.GetAllMenusByModuleID(moduleID);
        foreach (Menu menu in menus)
        {
            if (menu.ParentMenuID == 0)
            {
                ListItem item = new ListItem(menu.MenuTitle.ToString(), menu.MenuID.ToString());
                ddlParentMenu.Items.Add(item);
            }
        }
    }

    private void loadPage()
    {
        ListItem li = new ListItem("Select Page...", "0");
        ddlPage.Items.Add(li);

        List<Page> pages = new List<Page>();
        pages = PageManager.GetAllPages();
        foreach (Page page in pages)
        {
            ListItem item = new ListItem(page.PageTitle.ToString(), page.PageID.ToString());
            ddlPage.Items.Add(item);
        }
    }
    private void loadParentMenu()
    {
        ListItem li = new ListItem("No Parent Menu", "0");
        ddlParentMenu.Items.Add(li);

        List<Menu> parentMenus = new List<Menu>();
        parentMenus = MenuManager.GetAllMenus();
        foreach (Menu parentMenu in parentMenus)
        {
            ListItem item = new ListItem(parentMenu.MenuTitle.ToString(), parentMenu.MenuID.ToString());
            ddlParentMenu.Items.Add(item);
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
        if (!btnUpdate.Visible)
        {
            loadPage();
            loadParentMenu();
        }

        showMenuDataByModuleID(ddlModule.SelectedValue);
    }

    private void showMenuDataByModuleID(string ModuleID)
    {
        gvMenu.DataSource = MenuManager.GetAllMenusByModuleID(int.Parse(ModuleID));
        gvMenu.DataBind();
    }

    protected void lbSelect_Click(object sender, EventArgs e)
    {
       loadSingleMenu(((LinkButton)sender).CommandArgument);
    }

    private void loadSingleMenu(string MenuID)
    {
        Menu menu = new Menu();
        menu = MenuManager.GetMenuByID(Int32.Parse(MenuID));

        txtMenuTitle.Text = menu.MenuTitle;
        txtMenuOrder.Text = menu.MenuOrder.ToString();
        ddlModule.SelectedValue = menu.ModuleID.ToString();
        loadPage();
        //ddlModule_SelectedIndexChanged(this, new EventArgs());
        ddlPage.SelectedValue = menu.PageID.ToString();
        ddlParentMenu.SelectedValue = menu.ParentMenuID.ToString();
        ddlRowStatus.SelectedValue = menu.RowStatusID.ToString();
        hfMenuID.Value = menu.MenuID.ToString();

        btnAdd.Visible = false;
        btnUpdate.Visible = true;
        btnClear.Visible = true;
    }

    protected void lbAddSubMenu_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ModuleID"] != null)
        {
            ddlModule.SelectedValue = Request.QueryString["ModuleID"];
            ddlModule_SelectedIndexChanged(this, new EventArgs());
        }

        if (Request.QueryString["PageID"] != null)
        {
            ddlPage.SelectedValue = Request.QueryString["PageID"];
        }

        ddlParentMenu.SelectedValue = ((LinkButton)sender).CommandArgument;

        btnAdd.Visible = true;
        btnUpdate.Visible = false;
        btnClear.Visible = false;
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = MenuManager.DeleteMenu(Convert.ToInt32(linkButton.CommandArgument));

        showMenuDataByModuleID(ddlModule.SelectedValue);
    }

    private void showMenuGrid()
    {
        gvMenu.DataSource = MenuManager.GetAllMenus();
        gvMenu.DataBind();
    }
}
