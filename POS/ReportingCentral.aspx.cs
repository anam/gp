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
            applyRole();
        }
    }

    private void applyRole()
    {

       List<Button> allAllowedButton= ButtonManager.GetAllButtonsByPageURLnUserID(HttpContext.Current.Request.Url.AbsoluteUri, getLogin().LoginID);
       
        //load the transaction Type rbtn
        List<Pos_TransactionType> allTransactionType = Pos_TransactionTypeManager.GetAllPos_TransactionTypes();

       foreach (Pos_TransactionType type in allTransactionType)
       {
           foreach (Button btn in allAllowedButton)
           {
               if (btn.ButtonName == ("rbtn" + type.TransactionTypeName.Replace(" ", "")))
               {
                   rbtnTransactionType.Items.Add(new ListItem(type.TransactionTypeName, type.Pos_TransactionTypeID.ToString()));
                   break;
               }
           }
       }

       rbtnTransactionType.SelectedValue = "13";

           trSupplier.Visible = false;
           trWorkStation.Visible = false;
           ddlWorkStationForTransactionReport.Enabled = false;
           btnGenerateLinkForTransacationByID.Visible = false;
           btnDelivaryChalan.Visible = false;
           btnDeleteTransaction.Visible = false;
           btnCentralProductStockReport.Visible = false;
           btnWorkStationStockReport.Visible = false;
           btnLedgerSup.Visible = false;
           btnLedger.Visible = false;
           btnLedgerBR.Visible = false;
           btnCentralStockReport.Visible = false;
           btnShowroomStockReport.Visible = false;
           btnShowroomSalesReport.Visible = false;
           btnSalesPersonWiseSalesReport.Visible = false;
           btnDateWiseSalesReport.Visible = false;
           btnDayWiseSalesSummary.Visible = false;

         foreach (Button btn in allAllowedButton)
           {
               if (btn.ButtonName == "trSupplier")
               {
                   trSupplier.Visible = true;
               }

               if (btn.ButtonName == "trWorkStation")
               {
                   trWorkStation.Visible = true;
               }

               if (btn.ButtonName == "trWorkStation_Enabled")
               {
                   ddlWorkStationForTransactionReport.Enabled = true;
               }
               else
               {
                   ddlWorkStationForTransactionReport.SelectedValue = getLogin().ExtraField5;
               }

               if (btn.ButtonName == "btnGenerateLinkForTransacationByID")
               {
                   btnGenerateLinkForTransacationByID.Visible = true;
               }

               if (btn.ButtonName == "btnDelivaryChalan")
               {
                   btnDelivaryChalan.Visible = true;
               }

               if (btn.ButtonName == "btnDeleteTransaction")
               {
                   btnDeleteTransaction.Visible = true;
               }

               if (btn.ButtonName == "btnCentralProductStockReport")
               {
                   btnCentralProductStockReport.Visible = true;
               }

               if (btn.ButtonName == "btnWorkStationStockReport")
               {
                   btnWorkStationStockReport.Visible = true;
               }
               

               if (btn.ButtonName == "btnLedgerSup")
               {
                   //btnLedgerSup.Visible = true;
               }

               if (btn.ButtonName == "btnLedger")
               {
                   btnLedger.Visible = true;
               }

               if (btn.ButtonName == "btnLedgerBR")
               {
                   //btnLedgerBR.Visible = true;
               }

               if (btn.ButtonName == "btnCentralStockReport")
               {
                   btnCentralStockReport.Visible = true;
               }
               if (btn.ButtonName == "btnShowroomStockReport")
               {
                   btnShowroomStockReport.Visible = true;
               }
               if (btn.ButtonName == "btnShowroomSalesReport")
               {
                   btnShowroomSalesReport.Visible = true;
               }
               if (btn.ButtonName == "btnSalesPersonWiseSalesReport")
               {
                   btnSalesPersonWiseSalesReport.Visible = true;
               }
               if (btn.ButtonName == "btnDateWiseSalesReport")
               {
                   btnDateWiseSalesReport.Visible = true;
               }
               if (btn.ButtonName == "btnDayWiseSalesSummary")
               {
                   btnDayWiseSalesSummary.Visible = true;
               }
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

        ddlACC_ChartOfAccountLabel4.Items.Add(new ListItem("All Supplier", "0"));
        ddlWorkStationForTransactionReport.Items.Add(new ListItem("All Workstation", "0"));
        ddlSalesMan.Items.Add(new ListItem("All Salesman", "0"));
        ddlProduct.Items.Add(new ListItem("All Product-->", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            
            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 6)
            {
                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlACC_ChartOfAccountLabel4.Items.Add(item);
            }

            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 4)
            {
                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlSalesMan.Items.Add(item);
            }


            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 3)
            {
                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlProduct.Items.Add(item);
            }
            if (aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1 
                /*&&
                (
                   aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Head")
                   ||
                   aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.Contains("Show")
                )*/
                )
            {
                ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlWorkStationForTransactionReport.Items.Add(item);
            }
        }
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
        hlnkDateWiseSalesReport.Visible = false;
        hlnkSalesPersonWiseSalesReport.Visible = false;
        hlnkDayWiseSalesSummary.Visible = false;
        hlnkLinkForDeleteTransaction.Visible = false;
        hlnkTopSaleRport.Visible = false;
        hlnkTransactionIDsByDate.Visible = false;
        hlnkSingleProductSearchInHeadOffice.Visible = false;
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

        string pageName = "TransactionPrint";
        if (rbtnTransactionType.SelectedValue == "13")
        {
            pageName = "SalesPrint";
        }
        else
            if (rbtnTransactionType.SelectedValue == "9")
            {
                pageName = "DelivaryChalanPrint";
            }

        hlnkLinkForTransacationByID.NavigateUrl = pageName + ".aspx?Pos_TransactionMasterID=" + CommonManager.SQLExec(sql).Tables[0].Rows[0][0].ToString();
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
        hlinkCentralProductStockReport.NavigateUrl = "StockReportInCentralByDatePrint.aspx?FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
        //hlinkCentralProductStockReport.NavigateUrl = "StockReportInCentralByDate" + (rbtnlViewStyle.SelectedValue == "1" ? "ItemWise" : "") + "Print.aspx?FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
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
        string customerID = "0";
        try
        {
            if (txtCardNo.Text.Trim() != "")
            {
                DataSet ds = CommonManager.SQLExec("select Pos_CustomerID from Pos_Customer where CardNo='" + txtCardNo.Text + "'");

                customerID = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        catch (Exception ex)
        {
            customerID = "0";
        }

        hlnkShowroomSalesReport.NavigateUrl = "SalesReportDaywisePrint.aspx?CustomerID=" + customerID + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    }


    protected void btnWorkStationStockReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        if (ddlWorkStationForTransactionReport.SelectedValue == "0")
        {
            showAlartMessage("Please select a specific Showroom");
            return;
        }
        
        hlnkWorkStationStockReport.Visible = true;
        hlnkWorkStationStockReport.NavigateUrl = "StockReportInWorkStationByDate" + (rbtnlViewStyle.SelectedValue == "1" ? "ItemWise" : (rbtnlViewStyle.SelectedValue == "2" ? "ItemWise10Digit" : "")) + "Print.aspx?WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
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
        //if (rbtnTransactionType.SelectedValue == "9" && rbtnlViewStyle.SelectedIndex != 0)
        //{
        //    hlnkLedger.NavigateUrl = (rbtnlViewStyle.SelectedIndex == 0 ? "TransactionReportPrint.aspx" : "IssueReportItemWisePrint.aspx") + "?SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&TransactionTypeID=" + rbtnTransactionType.SelectedValue + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
        //}
        //else
        //hlnkLedger.NavigateUrl = (rbtnlViewStyle.SelectedIndex == 0 ? "TransactionReportPrint.aspx" : "TransactionReportItemWisePrint.aspx") + "?SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&TransactionTypeID="+rbtnTransactionType.SelectedValue+"&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd")+" 11:59 PM";
        hlnkLedger.NavigateUrl = (rbtnlViewStyle.SelectedIndex == 0 ? "TransactionReportPrint.aspx" : (rbtnlViewStyle.SelectedIndex == 1 ? "TransactionReportItemWisePrint.aspx" : "TransactionReport10DigitPrint.aspx")) + "?SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&TransactionTypeID=" + rbtnTransactionType.SelectedValue + "&VAT=" + ddlVATStatus.SelectedValue + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    }

    

    protected void btnSalesPersonWiseSalesReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkSalesPersonWiseSalesReport.Visible = true;
        hlnkSalesPersonWiseSalesReport.NavigateUrl = (rbtnlViewStyle.SelectedIndex == 0 ? "Transaction_SalesManWise_ReportItemWisePrint.aspx" : "Transaction_SalesManWise_ReportItemWisePrint.aspx") + "?SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&TransactionTypeID=" + rbtnTransactionType.SelectedValue + "&SalesManID=" + ddlSalesMan.SelectedValue + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    }
    protected void btnDateWiseSalesReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkDateWiseSalesReport.Visible = true;
        hlnkDateWiseSalesReport.NavigateUrl = (rbtnlViewStyle.SelectedIndex == 0 ? "Transaction_Sales_ReportItemWisePrint.aspx" : "Transaction_Sales_ReportItemWisePrint.aspx") + "?SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&TransactionTypeID=" + rbtnTransactionType.SelectedValue + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    }
    protected void btnDayWiseSalesSummary_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkDayWiseSalesSummary.Visible = true;
        hlnkDayWiseSalesSummary.NavigateUrl = "SalesReportGroupByDayPrint.aspx?FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";

    }
    protected void btnTopSaleRport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkTopSaleRport.Visible = true;
        hlnkTopSaleRport.NavigateUrl = "TopSaleReportPrint.aspx?ItemGrop=" + (rbtnlViewStyle.SelectedIndex == 0 ? "1" : "0") + "&Quantity=" + (rbtnQuantityOrNetSale.SelectedIndex == 0 ? "1" : "0") + "&Top=" + (rbtnHeighestOrLowest.SelectedIndex == 0 ? "1" : "0") + "&TopNo=" + txtDisplayCount.Text + "&MaxPrice=" + txtPriceMax.Text + "&MinPrice=" + txtPriceMin.Text + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    }
    protected void btnTransactionID_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkTransactionIDsByDate.Visible = true;
        hlnkTransactionIDsByDate.NavigateUrl = "TransactionIDsPrint.aspx?WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + @"&TransactionTypeID=" + rbtnTransactionType.SelectedValue + @"&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    }

    protected void btnShowroomSalesReturnReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkTransactionSalesInvoice.Visible = true;
        hlnkTransactionSalesInvoice.NavigateUrl = "SalesReturnReportPrint.aspx?IsReturn=1&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    }
    private void createBackup()
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
	
	set @DBName='GentleParkHO'
			
	SET @file_Name = @path + @DBName + '.rar'
	BACKUP DATABASE @DBName TO DISK = @file_Name 
";

        CommonManager.SQLExec(sql);

        Response.Redirect("../GentleParkHO.rar");
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

    protected void btnBackup_Click(object sender, EventArgs e)
    {
        createBackup();
    }

    protected void btnRunningSizeStockReport_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkRunningSizeStockReport.Visible = true;
        hlnkRunningSizeStockReport.NavigateUrl = (rbtnlViewStyle.SelectedIndex == 0 ? "TransactionReportPrint.aspx" : "TransactionReportItemWiseSizePrint.aspx") + "?SuppliyerID=" + ddlACC_ChartOfAccountLabel4.SelectedValue + "&TransactionTypeID=" + rbtnTransactionType.SelectedValue + "&WorkStationID=" + ddlWorkStationForTransactionReport.SelectedValue + "&ProductID=" + ddlProduct.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    }
    protected void btnSingleProductSearchInHeadOffice_Click(object sender, EventArgs e)
    {
        makeAllTheLinkInvisible();
        hlnkSingleProductSearchInHeadOffice.Visible = true;
        hlnkSingleProductSearchInHeadOffice.NavigateUrl = "Analysis_StockReportInCentralByDateItemWisePrint.aspx?ProductID=" + ddlProduct.SelectedValue + "&FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "&ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
    
    }
}
