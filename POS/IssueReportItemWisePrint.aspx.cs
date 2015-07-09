using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Inventory_PurchasePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialLoad();
            loadData();
        }
    }

    
    private void loadData()
    {
        

        int TransactionTypeID = 9;
        

        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");
        lblStockDate.Text = DateTime.Parse(fromDate).ToString("dd/MM/yyyy") + " to " + DateTime.Parse(toDate).ToString("dd/MM/yyyy");

        //purchase info
        string sql = @"SELECT     ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text AS WorkSatation
, ACC_ChartOfAccountLabel4_1.ChartOfAccountLabel4Text AS Product, 
                      Pos_TransactionMaster.TransactionDate, Pos_Product.BarCode,Pos_Product.ProductID,
                       Pos_TransactionMaster.TransactionID, Pos_Size.SizeName
                       ,Pos_Product.SalePrice as UnitPrice,Pos_Transaction.Quantity
,(Pos_Product.SalePrice * Pos_Transaction.Quantity) as TotalAmount
FROM         Pos_Transaction INNER JOIN
                      Pos_Product ON Pos_Transaction.Pos_ProductID = Pos_Product.Pos_ProductID INNER JOIN
                      ACC_ChartOfAccountLabel4 AS ACC_ChartOfAccountLabel4_1 ON 
                      Pos_Product.ProductID = ACC_ChartOfAccountLabel4_1.ACC_ChartOfAccountLabel4ID INNER JOIN
                      ACC_ChartOfAccountLabel4 ON Pos_Transaction.WorkStationID = ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID INNER JOIN
                      Pos_TransactionMaster ON Pos_Transaction.Pos_ProductTransactionMasterID = Pos_TransactionMaster.Pos_TransactionMasterID INNER JOIN
                      Pos_Size ON Pos_Product.Pos_SizeID = Pos_Size.Pos_SizeID
where Pos_TransactionMaster.Pos_TransactionTypeID=9
and (Pos_TransactionMaster.TransactionDate between '" + fromDate + "' and '" + toDate + "')";

        if (Request.QueryString["WorkStationID"] != null)
        {
            sql += @"
and Pos_Transaction.WorkStationID=" + Request.QueryString["WorkStationID"] + @"
            ";
        }
        sql += @"
order by ACC_ChartOfAccountLabel4_1.ChartOfAccountLabel4Text
,Pos_TransactionMaster.TransactionDate,
ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text,Pos_Product.BarCode;";

        
        lblVoucherType.Text = Pos_TransactionTypeManager.GetPos_TransactionTypeByID(TransactionTypeID).TransactionTypeName;
        

        DataSet ds= CommonManager.SQLExec(sql);


        string ProductID = "0";
            decimal Total = 0;
            decimal Subtotal = 0;
            decimal TotalAmount = 0;
            decimal SubtotalAmount = 0;
            int serialNo = 1;

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        ";


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (ProductID != "0" && ProductID != dr["ProductID"].ToString())
                {
                    htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + Subtotal.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
                        </tr>";

                    Subtotal = 0;
                    SubtotalAmount = 0;
                }

                if (ProductID != dr["ProductID"].ToString())
                {
                    ProductID = dr["ProductID"].ToString();
                    htmlTable += @"<tr>
                            <td colspan='10' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;'>
                                <b>Item:</b> " + dr["Product"].ToString() + @"
                            </td>
                        </tr>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Date</td>
                            <td>Branch Name</td>
                            <td>Issue ID</td>
                            <td>Barcode</td>
                            <td>Product</td>
                            <td>Size</td>
                            <td>Qty</td>
                            <td>Price</td>
                            <td>Amount</td>
                        </tr>";
                }

                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + DateTime.Parse(dr["TransactionDate"].ToString()).ToString("dd MMM yyyy") + @"</td>
                            <td>" + dr["WorkSatation"].ToString() + @"</td>
                            <td>" + dr["TransactionID"].ToString() + @"</td>
                            <td>" + dr["BarCode"].ToString() + @"</td>
                            <td>" + dr["Product"].ToString() + @"</td>
                            <td>" + dr["SizeName"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Quantity"].ToString()).ToString("0,0.00") + @"</td>
                            <td >" + decimal.Parse(dr["UnitPrice"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["TotalAmount"].ToString()).ToString("0,0.00") + @"</td>
                        </tr>";

                Subtotal += decimal.Parse(dr["Quantity"].ToString());
                SubtotalAmount += decimal.Parse(dr["TotalAmount"].ToString());

                Total += decimal.Parse(dr["Quantity"].ToString());
                TotalAmount += decimal.Parse(dr["TotalAmount"].ToString());
            }

            htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + Subtotal.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
                        </tr>";

            htmlTable += @"<tr id='lastRow'>
                        <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        <td>Grand Total</td>
                        <td>" + Total.ToString("0,0.00") + @"</td>
                        <td>&nbsp;</td>
                        <td>" + TotalAmount.ToString("0,0.00") + @"</td>
                    </tr></table>";


        lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}