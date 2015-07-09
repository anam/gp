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

        loadACC_ChartOfAccountLabel4();
        txtFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        txtToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        //txtStockDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        
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

   
    private void loadACC_ChartOfAccountLabel4()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlACC_ChartOfAccountLabel4.Items.Add(new ListItem("-- Any Supplier --", "0"));
        ddlWorkStationID.Items.Add(new ListItem("-- Any Workstation --", "0"));
        ddlWorkStationForTransactionReport.Items.Add(new ListItem("-- Any Workstation --", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            
            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 6)
            {
                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlACC_ChartOfAccountLabel4.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
            {
                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlWorkStationID.Items.Add(item);
                ddlWorkStationForTransactionReport.Items.Add(item);
            }
        }
    }
    protected void btnSupplierwiseReport_Click(object sender, EventArgs e)
    {
        hlnkTransactionSummary.NavigateUrl = (rbtnlViewStyle.SelectedValue == "0" ? "PurchaseReportPrint" : "PurchaseReportItemWisePrint") + ".aspx?RawmaterialsTypeID="+rbtnlRawmaterialsType.SelectedValue+"&SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlnkTransactionSummary.Visible = true;
        hlnkSupplierwisePurchaseReturnReport.Visible = false;

    }


    protected void btnSupplierwisePurchaseReturnReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkSupplierwisePurchaseReturnReport.NavigateUrl = (rbtnlViewStyle.SelectedValue == "0" ? "PurchaseReturnReportPrint" : "PurchaseReturnReportItemWisePrint") + ".aspx?RawmaterialsTypeID=" + rbtnlRawmaterialsType.SelectedValue + "&SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlnkSupplierwisePurchaseReturnReport.Visible = true;

    }


    protected void btnAllSupplierItemReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkPurchaseReportSupplierWise.NavigateUrl = "PurchaseReportSupplierWisePrint.aspx?RawmaterialsTypeID=" + rbtnlRawmaterialsType.SelectedValue + "&SuppliyerID=0&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlnkPurchaseReportSupplierWise.Visible = true;

    }


    protected void btnSupplierwiseAdjustmentReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkSupplierwisePurchaseAdjustment.NavigateUrl = (rbtnlViewStyle.SelectedValue == "0" ? "AdjustmentReportPrint" : "AdjustmentReportItemWisePrint") + ".aspx?RawmaterialsTypeID=" + rbtnlRawmaterialsType.SelectedValue + "&SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlnkSupplierwisePurchaseAdjustment.Visible = true;

    }

    protected void btnWorkStationWiseIssueReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkWorkStationWiseIssueReport.NavigateUrl = (rbtnlViewStyle.SelectedValue == "0" ? "IssueReportPrint" : "IssueReportItemWisePrint") + ".aspx?RawmaterialsTypeID=" + rbtnlRawmaterialsType.SelectedValue + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlnkWorkStationWiseIssueReport.Visible = true;

    }

    protected void btnWorkStationWiseIssueReturnReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkWorkStationWiseIssueReturnReport.NavigateUrl = (rbtnlViewStyle.SelectedValue == "0" ? "IssueReturnReportPrint" : "IssueReturnReportItemWisePrint") + ".aspx?RawmaterialsTypeID=" + rbtnlRawmaterialsType.SelectedValue + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlnkWorkStationWiseIssueReturnReport.Visible = true;

    }


    protected void btnGenerateWorkStationWiseStockReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkWorkStationWiseStockReport.NavigateUrl = "StockWorkStationWiseReportPrint.aspx?RawmaterialsTypeID=" + rbtnlRawmaterialsType.SelectedValue + "&WorkStationID=" + ddlWorkStationID.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlnkWorkStationWiseStockReport.Visible = true;
    }

    protected void btnPrintPurchaseID_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintPurchaseID.NavigateUrl = "PurchasePrint.aspx?PurchaseID=" + txtID.Text;
        hlPrintPurchaseID.Visible = true;
    }

    protected void btnPrintPurchaseReturnID_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintPurchaseReturnID.NavigateUrl = "PurchaseReturnPrint.aspx?ReturnID=" + txtID.Text;
        hlPrintPurchaseReturnID.Visible = true;
    }

    protected void btnPrintAdjustment_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintAdjustment.NavigateUrl = "AdjustmentPrint.aspx?AdjustmentID=" + txtID.Text;
        hlPrintAdjustment.Visible = true;
    }

    protected void btnPrintIssue_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintIssue.NavigateUrl = "IssuePrint.aspx?IssueID=" + txtID.Text;
        hlPrintIssue.Visible = true;
    }


    protected void btnPrintIssueReturn_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintIssueReturn.NavigateUrl = "IssueReturnPrint.aspx?IssueReturnID=" + txtID.Text;
        hlPrintIssueReturn.Visible = true;
    }


    private void makeAllTheLinkInvisible()
    {
        hlnkWorkStationWiseStockReportDateWise.Visible = false;
        hlnkSupplierwisePurchaseReturnReport.Visible = false;
        hlnkSupplierwisePurchaseAdjustment.Visible = false;
        hlnkTransactionSummary.Visible = false;
        hlnkWorkStationWiseStockReport.Visible = false;
        hlnkWorkStationWiseIssueReport.Visible = false;
        hlPrintPurchaseID.Visible = false;
        hlPrintPurchaseReturnID.Visible = false;
        hlPrintAdjustment.Visible = false;
        hlPrintIssue.Visible = false;
        lblDeleteMessage.Visible = false;
        hlItemHistory.Visible = false;
        hlPrintIssueForAdmin.Visible = false;
        hlPrintIssueReturn.Visible = false;
        hlnkbackDatedStockReport.Visible = false;
        hlnkPurchaseReportSupplierWise.Visible = false;
        hlUtilizationPrintAdmin.Visible = false;
        hlUtilizationPrint.Visible = false;
        hlnkWorkStationWiseIssueReturnReport.Visible = false;
        hlWorkStationWiseUtilizationReport.Visible = false;
        hlnkWorkStationWiseUtilizationProductBasedReport.Visible = false;
    }
    
    protected void btnDeletePurchaseReturn_Click(object sender, EventArgs e)
    {

    }
    protected void btnDeleteAdjustment_Click(object sender, EventArgs e)
    {

    }
    protected void btnDeleteIssue_Click(object sender, EventArgs e)
    {

    }
    protected void btnItemHistory_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlItemHistory.NavigateUrl = "ItemHistoryPrint.aspx?ItemCode=" + txtID.Text;
        hlItemHistory.Visible = true;
    }
    protected void btnPrintIssueForAdmin_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlPrintIssueForAdmin.NavigateUrl = "IssueForAdminPrint.aspx?IssueID=" + txtID.Text;
        hlPrintIssueForAdmin.Visible = true;
    }
    protected void btnGenerateBackDatedStockReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkbackDatedStockReport.NavigateUrl = "StockReportInCentralByDatePrint.aspx?RawmaterialsTypeID=" + rbtnlRawmaterialsType.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlnkbackDatedStockReport.Visible = true;
    }
    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }

    protected void btnGenerateBackDatedWorkStationStockReport_Click(object sender, EventArgs e)
    {

        if (ddlWorkStationID.SelectedValue == "0")
        {
            showAlartMessage("Please select the workstation");
            return;
        }
        makeAllTheLinkInvisible();
        hlnkWorkStationWiseStockReportDateWise.NavigateUrl = "StockReportInWorkStationByDate" + (rbtnlViewStyle.SelectedValue == "0" ? "" : "ByItemWise") + "Print.aspx?WorkStationID=" + ddlWorkStationID.SelectedValue + "&RawmaterialsTypeID=" + rbtnlRawmaterialsType.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlnkWorkStationWiseStockReportDateWise.Visible = true;
    }
    protected void btnUtilizationPrint_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlUtilizationPrint.NavigateUrl = "UtilizationPrint.aspx?UtilizationID=" + txtID.Text;
        hlUtilizationPrint.Visible = true;
    }
    protected void btnUtilizationPrintAdmin_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlUtilizationPrintAdmin.NavigateUrl = "UtilizationAdminPrint.aspx?UtilizationID=" + txtID.Text;
        hlUtilizationPrintAdmin.Visible = true;
    }
    protected void btnWorkStationWiseUtilizationReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlWorkStationWiseUtilizationReport.NavigateUrl = (rbtnlViewStyle.SelectedValue == "0" ? "UtilizationReportPrint" : "UtilizationReportItemWisePrint") + ".aspx?RawmaterialsTypeID=" + rbtnlRawmaterialsType.SelectedValue + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlWorkStationWiseUtilizationReport.Visible = true;
    }
    protected void btnWorkStationWiseUtilizationProductBasedReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkWorkStationWiseUtilizationProductBasedReport.NavigateUrl = "UtilizationReportProductBasedPrint.aspx?WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd");
        hlnkWorkStationWiseUtilizationProductBasedReport.Visible = true;
    }

    protected void btnBackup_Click(object sender, EventArgs e)
    {
        try
        {
            DeleteFileFromFolder("GentleParkHO.rar");
        }
        catch (Exception ex)
        { }

        string sql = @"
DECLARE @DBName VARCHAR(50)
	DECLARE @path VARCHAR(256) 
	DECLARE @file_Name VARCHAR(256) -- filename for backup 
	
	SET @path = '" + Server.MapPath("..\\") + @"'
	
	set @DBName='GentleParkHO_Old'
			
	SET @file_Name = @path + @DBName + '.rar'
	BACKUP DATABASE @DBName TO DISK = @file_Name 
";

        CommonManager.SQLExec(sql);

        Response.Redirect("../GentleParkHO_Old.rar");
    }

    public void DeleteFileFromFolder(string StrFilename)
    {

        try
        {
            string strPhysicalFolder = Server.MapPath("..\\");
            string strFileFullPath = strPhysicalFolder + StrFilename;

            if (System.IO.File.Exists(strFileFullPath))
            {
                System.IO.File.Delete(strFileFullPath);
            }
        }
        catch (Exception ex)
        {
        }
    }
}
