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

public partial class AdminACC_JournalDetailInsertUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            initailaData();
        }
    }

    private void initailaData()
    {

        
    }

  
    private Login getLogin()
    {
        Login login = new Login();
        try
        {
            if (Session["Login"] == null) { Session["PreviousPage"] = HttpContext.Current.Request.Url.AbsoluteUri; Response.Redirect("../LoginPage.aspx"); }

            login = (Login)Session["Login"];
        }
        catch (Exception ex)
        { }

        return login;
    }

   
   
    private void makeAllTheLinkInvisible()
    {
        hlPrintPurchaseForAdmin.Visible = false;
        hlPrintIssueForAdmin.Visible = false;
        lblDeleteMessage.Visible = false;
        hlItemHistory.Visible = false;
    }
    protected void btnDeletePurchase_Click(object sender, EventArgs e)
    {
        if (Inv_PurchaseManager.DeleteInv_Purchase(int.Parse(txtID.Text)))
        {
            makeAllTheLinkInvisible();
            lblDeleteMessage.Visible = true;
            lblDeleteMessage.Text = "Deleted Successfully..";
        }
    }
    protected void btnDeletePurchaseReturn_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintPurchaseReturnForAdmin.NavigateUrl = "PurchaseReturnAdminPrint.aspx?ReturnID=" + txtID.Text;
        hlPrintPurchaseReturnForAdmin.Visible = true;
    }
    protected void btnDeleteAdjustment_Click(object sender, EventArgs e)
    {

    }
    protected void btnDeleteIssue_Click(object sender, EventArgs e)
    {
        if (Inv_IssueMasterManager.DeleteInv_IssueMasterAll(int.Parse(txtID.Text), getLogin().LoginID))
        {
            makeAllTheLinkInvisible();
            lblDeleteMessage.Visible = true;
            lblDeleteMessage.Text = "Deleted Successfully..";
        }
        txtID.Text = "";
    }
    protected void btnItemHistory_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlItemHistory.NavigateUrl = "ItemHistory.aspx?ItemCode=" + txtID.Text;
        hlItemHistory.Visible = true;
    }
    protected void btnPrintIssueForAdmin_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintIssueForAdmin.NavigateUrl = "IssueForAdminPrint.aspx?IssueID=" + txtID.Text;
        hlPrintIssueForAdmin.Visible = true;
    }

    protected void btnPrintIssueReturnForAdmin_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintIssueReturnForAdmin.NavigateUrl = "IssueReturnAdminPrint.aspx?IssuereturnID=" + txtID.Text;
        hlPrintIssueReturnForAdmin.Visible = true;
    }


    protected void btnPrintUtilizationForAdmin_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintUtilizationForAdmin.NavigateUrl = "UtilizationModification.aspx?UtilizationID=" + txtID.Text;
        hlPrintUtilizationForAdmin.Visible = true;
    }


    protected void btnPrintPurchaseForAdmin_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintPurchaseForAdmin.NavigateUrl = "PurchasePrintAdmin.aspx?PurchaseID=" + txtID.Text;
        hlPrintPurchaseForAdmin.Visible = true;
    }
}
