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

public partial class AdminButtonInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadPage();
            loadDefaultButton();
            loadRowStatus();

            ddlRowStatus.SelectedValue = "1";

            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            btnClear.Visible = false;

            if (Request.QueryString["PageID"] != null)
            {
                ddlPage.SelectedValue = Request.QueryString["PageID"];
                ddlPage_SelectedIndexChanged(this, new EventArgs());
            }

            if (Request.QueryString["ButtonID"] != null)
            {
                loadSingleButton(Request.QueryString["ButtonID"]);
            }
        }
    }

    protected void lbSelect_Click(object sender, EventArgs e)
    {
        loadSingleButton(((LinkButton)sender).CommandArgument);
    }

    private void loadSingleButton(string ButtonID)
    {
        Button button = new Button();
        button = ButtonManager.GetButtonByID(Int32.Parse(ButtonID));

        txtButtonName.Text = button.ButtonName;
        txtButtonText.Text = button.ButtonText.ToString();
        ddlPage.SelectedValue = button.PageID.ToString();
        ddlRowStatus.SelectedValue = button.RowStatusID.ToString();

        btnAdd.Visible = false;
        btnUpdate.Visible = true;
        btnClear.Visible = true;
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = new LinkButton();
        linkButton = (LinkButton)sender;
        bool result = ButtonManager.DeleteButton(Convert.ToInt32(linkButton.CommandArgument));
        showButtonGrid();
    }

    private void showButtonGrid()
    {
        gvButton.DataSource = ButtonManager.GetAllButtonsByPageID(int.Parse(ddlPage.SelectedValue));
        gvButton.DataBind();
    }

    private void loadDefaultButton()
    {
        ListItem li = new ListItem("Select Default Button...", "0");
        ddlDefaultButton.Items.Add(li);

        List<DefaultButton> defaultButtons = new List<DefaultButton>();
        defaultButtons = DefaultButtonManager.GetAllDefaultButtons();
        foreach (DefaultButton defaultButton in defaultButtons)
        {
            ListItem item = new ListItem(defaultButton.DefaultButtonText.ToString(), defaultButton.DefaultButtonName.ToString());
            ddlDefaultButton.Items.Add(item);
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

        Button button = new Button();

        button.ButtonName = txtButtonName.Text;
        button.ButtonText = txtButtonText.Text;
        button.PageID = Int32.Parse(ddlPage.SelectedValue);
        button.AddedBy = loginID;
        button.AddedDate = DateTime.Now;
        button.UpdatedBy = loginID;
        button.UpdatedDate = DateTime.Now;
        button.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        int resutl = ButtonManager.InsertButton(button);

        showButtonGrid();
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
        Button button = new Button();
        button = ButtonManager.GetButtonByID(Int32.Parse(Request.QueryString["buttonID"]));
        Button tempButton = new Button();
        tempButton.ButtonID = button.ButtonID;

        tempButton.ButtonName = txtButtonName.Text;
        tempButton.ButtonText = txtButtonText.Text;
        tempButton.PageID = Int32.Parse(ddlPage.SelectedValue);
        tempButton.AddedBy = loginID;
        tempButton.AddedDate = DateTime.Now;
        tempButton.UpdatedBy = loginID;
        tempButton.UpdatedDate = DateTime.Now;
        tempButton.RowStatusID = Int32.Parse(ddlRowStatus.SelectedValue);
        bool result = ButtonManager.UpdateButton(tempButton);

        showButtonGrid();
        btnClear_Click(this, new EventArgs());
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtButtonName.Text = "";
        txtButtonText.Text = "";
        ddlPage.SelectedIndex = 0;
        ddlRowStatus.SelectedIndex = 0;

        btnAdd.Visible = true;
        btnUpdate.Visible = false;
        btnClear.Visible = false;
    }
    private void showButtonData()
    {
        Button button = new Button();
        button = ButtonManager.GetButtonByID(Int32.Parse(Request.QueryString["buttonID"]));

        txtButtonName.Text = button.ButtonName;
        txtButtonText.Text = button.ButtonText.ToString();
        ddlPage.SelectedValue = button.PageID.ToString();
        ddlRowStatus.SelectedValue = button.RowStatusID.ToString();
    }
    private void loadPage()
    {
        ListItem li = new ListItem("Select Page...", "0");
        ddlPage.Items.Add(li);

        List<Page> pages = new List<Page>();
        pages = PageManager.GetAllPages();
        foreach (Page page in pages)
        {
            ListItem item = new ListItem(page.ModuleID.ToString()+". "+page.PageTitle.ToString(), page.PageID.ToString());
            ddlPage.Items.Add(item);
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
    protected void ddlDefaultButton_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtButtonName.Text = ddlDefaultButton.SelectedValue;
        txtButtonText.Text = ddlDefaultButton.SelectedItem.Text;
    }
    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        showButtonGrid();
    }
}
