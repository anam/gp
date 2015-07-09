using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class POS_TopSaleReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialdata();
            /*
        hlnkTopSaleRport.NavigateUrl = "TopSaleReportPrint.aspx?ItemGrop=" + (rbtnlViewStyle.SelectedIndex == 0 ? "1" : "0") + "
             * &Top=" + (rbtnHeighestOrLowest.SelectedIndex == 0 ? "1" : "0") + "&
             * TopNo=" + txtDisplayCount.Text + "
             * &MaxPrice=" + txtPriceMax.Text + "
             * &MinPrice=" + txtPriceMin.Text + "
             * &FromDate=" + DateTime.Parse(txtFromDate.Text).ToString("yyyy-MM-dd") + "
             * &ToDate=" + DateTime.Parse(txtToDate.Text).ToString("yyyy-MM-dd") + " 11:59 PM";
             */
            string ItemGrop = (Request.QueryString["ItemGrop"] == null ? "1" : Request.QueryString["ItemGrop"]);
            string Top = (Request.QueryString["Top"] == null ? "1" : Request.QueryString["Top"]);
            string TopNo = (Request.QueryString["TopNo"] == null ? "10" : Request.QueryString["TopNo"]);
            string MaxPrice = (Request.QueryString["MaxPrice"] == null ? "0" : Request.QueryString["MaxPrice"]);
            string MinPrice = (Request.QueryString["MinPrice"] == null ? "0" : Request.QueryString["MinPrice"]);
            string FromDate = (Request.QueryString["FromDate"] == null ? "0" : Request.QueryString["FromDate"]);
            string ToDate = (Request.QueryString["ToDate"] == null ? "0" : Request.QueryString["ToDate"]);
            string Quantity = (Request.QueryString["Quantity"] == null ? "0" : Request.QueryString["Quantity"]);

            if (ItemGrop == "1")
            {
                ItemGroupWise(Top, TopNo, MaxPrice, MinPrice, FromDate, ToDate, Quantity);
            }
            else
            {
                BarcodeWise(Top, TopNo, MaxPrice, MinPrice, FromDate, ToDate, Quantity);
            }
            //ItemGroupWise(" * Pos_Product.SalePrice");
            //BarcodeWise(" * Pos_Product.SalePrice");

        }
    }

    private void initialdata()
    {

        lblPrintDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
    }

   
    private void BarcodeWise( string Top, string TopNo, string MaxPrice, string MinPrice, string FromDate, string ToDate, string Quantity)
    {
        string price = "";
        if (Quantity == "1") price = " * Pos_Product.SalePrice";

        string sql = @"

        Select SUM(Quantity"+price+ @") as SaleQty,Pos_Product.BarCode,
        Pos_Product.ProductName
        into #tblSale
         from Pos_Transaction
        inner join Pos_Product on Pos_Product.Pos_ProductID = Pos_Transaction.Pos_ProductID
        inner join Pos_TransactionMaster 
	        on Pos_Transaction.Pos_ProductTransactionMasterID = Pos_TransactionMaster.Pos_TransactionMasterID
        where Pos_ProductTrasactionTypeID =13 and Pos_Transaction.RowStatusID=1
        and Pos_TransactionMaster.RowStatusID=1 and TransactionDate>='" + FromDate + @"' and  TransactionDate<='" + ToDate + @"'
        group by Pos_Product.BarCode,
        Pos_Product.ProductName

        Select SUM(Quantity" + price + @") as SaleReturnQty,Pos_Product.BarCode,
        Pos_Product.ProductName
        into #tblSaleReturn
         from Pos_Transaction
        inner join Pos_Product on Pos_Product.Pos_ProductID = Pos_Transaction.Pos_ProductID
        inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID = Pos_Product.ProductID
        inner join Pos_TransactionMaster 
	        on Pos_Transaction.Pos_ProductTransactionMasterID = Pos_TransactionMaster.Pos_TransactionMasterID
        where Pos_ProductTrasactionTypeID =14 and Pos_Transaction.RowStatusID=1
        and Pos_TransactionMaster.RowStatusID=1 and TransactionDate>='" + FromDate + @"' and  TransactionDate<='" + ToDate + @"'
        group by Pos_Product.BarCode,
        Pos_Product.ProductName


        select #tblSale.BarCode
        ,#tblSale.ProductName,#tblSale.SaleQty,#tblSaleReturn.SaleReturnQty
        into #total
         from #tblSale left outer join #tblSaleReturn on #tblSale.BarCode
        = #tblSaleReturn.ProductName
        Drop table #tblSale;
        Drop table #tblSaleReturn;
        select BarCode,ProductName ,SaleQty,
        (case  when SaleReturnQty <> 0 then SaleReturnQty else 0 end) as SalesReturn_Qty
        into #last
         from #total
        drop table #total

        Select top " +(TopNo)+@" BarCode,ProductName ,(SaleQty-SalesReturn_Qty) as netSale
        from #last
        where 1=1 " + (MaxPrice == "0" ? "" : " and (SaleQty-SalesReturn_Qty) <=" + MaxPrice) + (MinPrice == "0" ? "" : " and (SaleQty-SalesReturn_Qty) >=" + MinPrice) + @"
        order by (SaleQty-SalesReturn_Qty) " + (Top == "1" ? "Desc" : "Asc") + @"
        drop table #last
        ";

        DataSet ds = CommonManager.SQLExec(sql);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }

    private void ItemGroupWise( string Top, string TopNo, string MaxPrice, string MinPrice, string FromDate, string ToDate, string Quantity)
    {
        string price="";
        if (Quantity == "1") price = " * Pos_Product.SalePrice";
        
        string sql = @"

        Select SUM(Quantity" + price + @") as SaleQty,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,
        ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
        into #tblSale
         from Pos_Transaction
        inner join Pos_Product on Pos_Product.Pos_ProductID = Pos_Transaction.Pos_ProductID
        inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID = Pos_Product.ProductID
        inner join Pos_TransactionMaster 
	        on Pos_Transaction.Pos_ProductTransactionMasterID = Pos_TransactionMaster.Pos_TransactionMasterID
        where Pos_ProductTrasactionTypeID =13 and Pos_Transaction.RowStatusID=1
        and Pos_TransactionMaster.RowStatusID=1 and TransactionDate>='" + FromDate + @"' and  TransactionDate<='" + ToDate + @"'
        group by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
        ,ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID

        Select SUM(Quantity" + price + @") as SaleReturnQty,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,
        ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
        into #tblSaleReturn
         from Pos_Transaction
        inner join Pos_Product on Pos_Product.Pos_ProductID = Pos_Transaction.Pos_ProductID
        inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID = Pos_Product.ProductID
        inner join Pos_TransactionMaster 
	        on Pos_Transaction.Pos_ProductTransactionMasterID = Pos_TransactionMaster.Pos_TransactionMasterID
        where Pos_ProductTrasactionTypeID =14 and Pos_Transaction.RowStatusID=1
        and Pos_TransactionMaster.RowStatusID=1  and TransactionDate>='" + FromDate + @"' and  TransactionDate<='" + ToDate + @"'
        group by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
        ,ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID


        select #tblSale.ACC_ChartOfAccountLabel4ID
        ,#tblSale.ChartOfAccountLabel4Text,#tblSale.SaleQty,#tblSaleReturn.SaleReturnQty
        into #total
         from #tblSale left outer join #tblSaleReturn on #tblSale.ACC_ChartOfAccountLabel4ID
        = #tblSaleReturn.ACC_ChartOfAccountLabel4ID
        Drop table #tblSale;
        Drop table #tblSaleReturn;
        select ACC_ChartOfAccountLabel4ID,ChartOfAccountLabel4Text ,SaleQty,
        (case  when SaleReturnQty <> 0 then SaleReturnQty else 0 end) as SalesReturn_Qty
        into #last
         from #total
        drop table #total

        Select top " + (TopNo) + @"  ACC_ChartOfAccountLabel4ID,ChartOfAccountLabel4Text,(SaleQty-SalesReturn_Qty) as netSale
        from #last
        where 1=1 " + (MaxPrice == "0" ? "" : " and (SaleQty-SalesReturn_Qty) <=" + MaxPrice) + (MinPrice == "0" ? "" : " and (SaleQty-SalesReturn_Qty) >=" + MinPrice) + @"
      order by (SaleQty-SalesReturn_Qty) " + (Top == "1" ? "Desc" : "Asc") + @" 
      drop table #last
        ";

        DataSet ds = CommonManager.SQLExec(sql);
        GridView2.DataSource = ds.Tables[0];
        GridView2.DataBind();
    }
}