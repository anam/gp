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
        
        List<Pos_TransactionType> allTransactionType = Pos_TransactionTypeManager.GetAllPos_TransactionTypes();

        foreach (Pos_TransactionType type in allTransactionType)
        {
            if(type.ExtraField2 =="SR")
            rbtnTransactionType.Items.Add(new ListItem(type.TransactionTypeName,type.Pos_TransactionTypeID.ToString()));
        }

        rbtnTransactionType.SelectedValue = "13";
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

        ddlACC_ChartOfAccountLabel4.Items.Add(new ListItem("All Supplier", "0"));
        ddlWorkStationForTransactionReport.Items.Add(new ListItem("All Workstation", "0"));

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
                ddlWorkStationForTransactionReport.Items.Add(item);
            }
        }
        try
        {
            ddlWorkStationForTransactionReport.SelectedValue = getLogin().ExtraField5;
        }
        catch (Exception ex)
        { }
    }

    private void makeAllTheLinkInvisible()
    {
        hlnkCentralStockReport.Visible = false;
        hlnkWorkStationStockReport.Visible = false;
        hlnkLinkForTransacationByID.Visible = false;
        hlinkCentralProductStockReport.Visible = false;
        hlnkShowroomStockReport.Visible = false;
        hlnkLinkForDelivaryChalan.Visible = false;
        hlnkShowroomSalesReport.Visible = false;
        hlnkSalesPersonWiseSalesReport.Visible = false;
        hlnkDateWiseSalesReport.Visible = false;
        hlnkLinkForDeleteTransaction.Visible = false;
    }

    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }

    protected void btnGenerateLinkForTransacationByID_Click(object sender, EventArgs e)
    {
        if ((rbtnTransactionType.SelectedValue == "13" ||rbtnTransactionType.SelectedValue == "14") && ddlWorkStationForTransactionReport.SelectedValue =="0")
        {
            showAlartMessage("Please select the Branch for any Sales voucher");
            return;
        }

        makeAllTheLinkInvisible();
        hlnkLinkForTransacationByID.Visible = true;

        string sql = "Select Pos_TransactionMasterID from Pos_TransactionMaster where TransactionID=" + txtID.Text + " and Pos_TransactionTypeID="+rbtnTransactionType.SelectedValue;
        if (rbtnTransactionType.SelectedValue == "13"
            ||
            rbtnTransactionType.SelectedValue == "14"
            )
        {
            sql += " and WorkSatationID="+ddlWorkStationForTransactionReport.SelectedValue;
        }
        hlnkLinkForTransacationByID.NavigateUrl = (rbtnTransactionType.SelectedValue == "13" ? "SalesPrint.aspx" : "TransactionPrint.aspx") + "?Pos_TransactionMasterID=" + CommonManager.SQLExec(sql).Tables[0].Rows[0][0].ToString();
    }

    protected void btnDelivaryChalan_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkLinkForDelivaryChalan.Visible = true;

        string sql = "Select Pos_TransactionMasterID from Pos_TransactionMaster where TransactionID=" + txtID.Text + " and Pos_TransactionTypeID=" + rbtnTransactionType.SelectedValue;

        hlnkLinkForDelivaryChalan.NavigateUrl = "DelivaryChalanPrint.aspx?Pos_TransactionMasterID=" + CommonManager.SQLExec(sql).Tables[0].Rows[0][0].ToString();
    }


    protected void btnCentralProductStockReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlinkCentralProductStockReport.Visible = true;
        hlinkCentralProductStockReport.NavigateUrl = "StockReportInCentralByDatePrint.aspx?FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd")+" 11:59 PM";
    }

    protected void btnCentralStockReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkCentralStockReport.Visible = true;
        hlnkCentralStockReport.NavigateUrl = "StockReportInCentralBy" + (rbtnlViewStyle.SelectedValue == "1" ? "ItemWise" : "ItemGroup") + "Print.aspx"; 

    }

    protected void btnShowroomStockReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkShowroomStockReport.Visible = true;
        hlnkShowroomStockReport.NavigateUrl = "StockReportInShowroomBy" + (rbtnlViewStyle.SelectedValue == "1" ? "ItemWise" : "ItemGroup") + "Print.aspx?WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&WorkStationName="+ddlWorkStationForTransactionReport.SelectedItem.Text;

    }

    protected void btnShowroomSalesReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkShowroomSalesReport.Visible = true;
        hlnkShowroomSalesReport.NavigateUrl = "SalesReportDaywisePrint.aspx?WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";

    }

    protected void btnWorkStationStockReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkWorkStationStockReport.Visible = true;
        hlnkWorkStationStockReport.NavigateUrl = "StockReportInWorkStationByDate" + (rbtnlViewStyle.SelectedValue=="1"?"ItemWise":"") + "Print.aspx?WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd")+" 11:59 PM";
    }

    protected void btnLedgerBR_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkLedgerBr.Visible = true;
        hlnkLedgerBr.NavigateUrl = "";
    }
    protected void btnLedgerSup_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkLedgerSup.Visible = true;
        hlnkLedgerSup.NavigateUrl = "";
    }
    protected void btnLedger_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkLedger.Visible = true;
        hlnkLedger.NavigateUrl = (rbtnlViewStyle.SelectedIndex == 0 ? "TransactionReportPrint.aspx" : "TransactionReportItemWisePrint.aspx") + "?SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&TransactionTypeID="+rbtnTransactionType.SelectedValue+"&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd")+" 11:59 PM";
    }
    protected void btnSalesPersonWiseSalesReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkSalesPersonWiseSalesReport.Visible = true;
        hlnkSalesPersonWiseSalesReport.NavigateUrl = (rbtnlViewStyle.SelectedIndex == 0 ? "Transaction_SalesManWise_ReportItemWisePrint.aspx" : "Transaction_SalesManWise_ReportItemWisePrint.aspx") + "?SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&TransactionTypeID=" + rbtnTransactionType.SelectedValue + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    }
    protected void btnDateWiseSalesReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkDateWiseSalesReport.Visible = true;
        hlnkDateWiseSalesReport.NavigateUrl = (rbtnlViewStyle.SelectedIndex == 0 ? "Transaction_Sales_ReportItemWisePrint.aspx" : "Transaction_Sales_ReportItemWisePrint.aspx") + "?SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&TransactionTypeID=" + rbtnTransactionType.SelectedValue + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    }

    protected void btnDeleteTransaction_Click(object sender, EventArgs e)
    {
        if ((rbtnTransactionType.SelectedValue == "13" || rbtnTransactionType.SelectedValue == "14") && ddlWorkStationForTransactionReport.SelectedValue == "0")
        {
            showAlartMessage("Please select the Branch for any Sales voucher");
            return;
        }

        makeAllTheLinkInvisible();
        hlnkLinkForDeleteTransaction.Visible = true;

        string sql = "Select Pos_TransactionMasterID from Pos_TransactionMaster where TransactionID=" + txtID.Text + " and Pos_TransactionTypeID=" + rbtnTransactionType.SelectedValue;
        if (rbtnTransactionType.SelectedValue == "13"
            ||
            rbtnTransactionType.SelectedValue == "14"
            )
        {
            sql += " and WorkSatationID=" + ddlWorkStationForTransactionReport.SelectedValue;
        }
        hlnkLinkForDeleteTransaction.NavigateUrl = "TransactionPrint.aspx?Delete=1&Pos_TransactionMasterID=" + CommonManager.SQLExec(sql).Tables[0].Rows[0][0].ToString();
    }
}
