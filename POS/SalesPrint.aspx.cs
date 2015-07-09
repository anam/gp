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
            loadData();

            PrintSalesInvoice();
        }
    }

    private void PrintSalesInvoice()
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "javascript:printIt(document.getElementById('printSalesVoucher').innerHTML);",
             true);
    }

    private void loadData()
    {
        bool IsSalesReturnInvoice = false;
        int Pos_TransactionMasterID = int.Parse(Request.QueryString["Pos_TransactionMasterID"]);
        string returnInvoiceNo = "";
        string sql = @"select   Pos_TransactionMaster.TransactionDate,
                                Pos_TransactionMaster.TransactionID,
                                Pos_TransactionMaster.Particulars,
                                Pos_TransactionMaster.ToOrFromID as CustomerID,
                                SR.ChartOfAccountLabel4Text as WorkStationName,
                                SR.ExtraField2 as ShowRoomAddress,
                                Pos_TransactionMaster.ExtraField1 as CashAmount, 
                                Pos_TransactionMaster.ExtraField2 as CardAmount, 
                                Pos_TransactionMaster.ExtraField3 as CustomerName, 
                                Pos_TransactionMaster.ExtraField4 as ContactNo,
                                Pos_TransactionMaster.ExtraField5 as Due,
                                Pos_Customer.CardNo as CardNo,
                                SP.ChartOfAccountLabel4Text as SalesPerson,
                                Login_Login.FirstName +' '+Login_Login.LastName  as PreparedBy,
                                Pos_TransactionMaster.UpdatedBy
                            From
                            Pos_TransactionMaster
                            left outer join ACC_ChartOfAccountLabel4 as SR on SR.ACC_ChartOfAccountLabel4ID = Pos_TransactionMaster.WorkSatationID
                            left outer join ACC_ChartOfAccountLabel4 as SP on SP.ACC_ChartOfAccountLabel4ID = cast(Pos_TransactionMaster.Record as int)
                            inner join Login_Login on Login_Login.LoginID = Pos_TransactionMaster.AddedBy
                            left outer join Pos_Customer on Pos_Customer.Pos_CustomerID = Pos_TransactionMaster.ToOrFromID
                            where Pos_TransactionMaster.Pos_TransactionMasterID=" + Pos_TransactionMasterID.ToString() + @";

                            select Pos_Transaction.UpdatedBy as SalePrice,
                            Pos_Transaction.Quantity,
                            Pos_Product.ProductName,
                            Pos_Product.BarCode,
                            Pos_Transaction.ExtraField1 as Discount,
                            Pos_Transaction.ExtraField2 as Vat
                            from Pos_Product
                            inner join Pos_Transaction on Pos_Transaction.Pos_ProductID = Pos_Product.Pos_ProductID
                            where Pos_Transaction.Pos_ProductTransactionMasterID=" + Pos_TransactionMasterID.ToString() + @"
                            order by Pos_Product.ProductName;

";

        DataSet ds = CommonManager.SQLExec(sql);
        DataSet dsReturn = new DataSet();
        
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (int.Parse(ds.Tables[0].Rows[0]["UpdatedBy"].ToString()) < 0)
            {
                IsSalesReturnInvoice = true;
                sql = @"select   Pos_TransactionMaster.TransactionDate,
                                Pos_TransactionMaster.TransactionID,
                                Pos_TransactionMaster.Particulars,
                                Pos_TransactionMaster.ToOrFromID as CustomerID,
                                SR.ChartOfAccountLabel4Text as WorkStationName,
                                SR.ExtraField2 as ShowRoomAddress,
                                Pos_TransactionMaster.ExtraField1 as CashAmount, 
                                Pos_TransactionMaster.ExtraField2 as OldInvoiceNo, 
                                Pos_TransactionMaster.ExtraField3 as CustomerName, 
                                Pos_TransactionMaster.ExtraField4 as ContactNo,
                                Pos_TransactionMaster.ExtraField5 as Due,
                                Pos_Customer.CardNo as CardNo,
                                SP.ChartOfAccountLabel4Text as SalesPerson,
                                Login_Login.FirstName +' '+Login_Login.LastName  as PreparedBy,
                                Pos_TransactionMaster.UpdatedBy
                            From
                            Pos_TransactionMaster
                            left outer join ACC_ChartOfAccountLabel4 as SR on SR.ACC_ChartOfAccountLabel4ID = Pos_TransactionMaster.WorkSatationID
                            left outer join ACC_ChartOfAccountLabel4 as SP on SP.ACC_ChartOfAccountLabel4ID = cast(Pos_TransactionMaster.Record as int)
                            inner join Login_Login on Login_Login.LoginID = Pos_TransactionMaster.AddedBy
                            left outer join Pos_Customer on Pos_Customer.Pos_CustomerID = Pos_TransactionMaster.ToOrFromID
                            where Pos_TransactionMaster.Pos_TransactionMasterID=" + ((-1)*int.Parse(ds.Tables[0].Rows[0]["UpdatedBy"].ToString())).ToString() + @";

                            select  Pos_Transaction.UpdatedBy as SalePrice,
                            Pos_Transaction.Quantity,
                            Pos_Product.ProductName,
                            Pos_Product.BarCode,
                            Pos_Transaction.ExtraField1 as Discount,
                            Pos_Transaction.ExtraField2 as Vat
                            from Pos_Product
                            inner join Pos_Transaction on Pos_Transaction.Pos_ProductID = Pos_Product.Pos_ProductID
                            where Pos_Transaction.Pos_ProductTransactionMasterID=" + ((-1) * int.Parse(ds.Tables[0].Rows[0]["UpdatedBy"].ToString())).ToString() + @"
                            order by Pos_Product.ProductName;

";
                
                dsReturn = CommonManager.SQLExec(sql);
                returnInvoiceNo = @"
                        <tr>
                        <td>Return Invoice#</td>
                        <td>:</td>
                        <td>" + dsReturn.Tables[0].Rows[0]["OldInvoiceNo"].ToString() + @"</td>
                        </tr>";
                lblSalesReturn.Text = "Return";
            }
        }
        string htmlTable = "";
        lblBranchLocation.Text = ds.Tables[0].Rows[0]["ShowRoomAddress"].ToString();
        htmlTable = @"
                        <table>
                        
                        <tr>
                        <td>Date</td>
                        <td>:</td>
                        <td>" + DateTime.Parse(ds.Tables[0].Rows[0]["TransactionDate"].ToString()).ToString("dd-MMM-yyyy hh:mm tt") + @"</td>
                        </tr>
                        " + returnInvoiceNo + @"
                        <tr>
                        <td>Invoice#</td>
                        <td>:</td>
                        <td>" + ds.Tables[0].Rows[0]["TransactionID"].ToString() + @"</td>
                        </tr>
                        <tr>
                        <td>Card #</td>
                        <td>:</td>
                        <td>" + ds.Tables[0].Rows[0]["CardNo"].ToString() + @"</td>
                        </tr>
                        <tr>
                        <td>Cus Name</td>
                        <td>:</td>
                        <td>" + ds.Tables[0].Rows[0]["CustomerName"].ToString() + @"</td>
                        </tr>
                        <tr>
                        <td>BR Name</td>
                        <td>:</td>
                        <td>" + ds.Tables[0].Rows[0]["WorkStationName"].ToString()  + @"</td>
                        </tr>
                        <tr>
                        <td>SP</td>
                        <td>:</td>
                        <td>" + ds.Tables[0].Rows[0]["SalesPerson"].ToString() + @"</td>
                        </tr>
                        <tr>
                        <td>prepared by</td>
                        <td>:</td>
                        <td>" + ds.Tables[0].Rows[0]["PreparedBy"].ToString() + @"</td>
                        </tr>
                        <tr>
                        <td>Particulars</td>
                        <td>:</td>
                        <td>" + ds.Tables[0].Rows[0]["Particulars"].ToString() + @"</td>
                        </tr>
                        </table>
                        ";
        lblMaster.Text = htmlTable;
        string htmlTableReturn = "";
        decimal totalReturnAmount = 0;
        if (IsSalesReturnInvoice)
        {

           htmlTableReturn = @"<table id='dataTable' cellspacing='0' border='0' cellpadding='0'><tr class='bordered'><td>SI</td><td style='text-align:center;'>Return Item(s)</td><td>Qty</td><td>U. Price</td><td>Total Price</td></tr>";
           int countReturn = 1;
           decimal totalPrieReturn = 0;
           decimal totalQtyReturn = 0;
           decimal totalDiscountReturn = 0;
           decimal totalVatReturn = 0;
            foreach (DataRow dr in dsReturn.Tables[1].Rows)
            {
                totalPrieReturn += decimal.Parse(dr["SalePrice"].ToString()) * decimal.Parse(dr["Quantity"].ToString());
                totalQtyReturn += decimal.Parse(dr["Quantity"].ToString());
                totalDiscountReturn += decimal.Parse(dr["Discount"].ToString());
                totalVatReturn += decimal.Parse(dr["Vat"].ToString());
                htmlTableReturn += @"<tr class='bordered'><td>" + (countReturn++).ToString() + "</td><td>" + dr["BarCode"].ToString() + "<br/>" + dr["ProductName"].ToString() + "</td><td>" + decimal.Parse(dr["Quantity"].ToString()).ToString("0,0") + "</td><td>" + decimal.Parse(decimal.Parse(dr["SalePrice"].ToString()).ToString("0,0")).ToString("0,0.00") + "</td><td>" + decimal.Parse((decimal.Parse(dr["SalePrice"].ToString()) * decimal.Parse(dr["Quantity"].ToString())).ToString("0,0")).ToString("0,0.00") + "</td></tr>";
            }

            htmlTableReturn += @"<tr><td colspan='5'>....................................................................</td></tr>";
            htmlTableReturn += @"<tr><td></td><td>Sub Total:</td><td>" + totalQtyReturn.ToString("0,0") + "</td><td></td><td>" + decimal.Parse(totalPrieReturn.ToString("0,0")).ToString("0,0.00") + "</td></tr>";
            htmlTableReturn += @"<tr><td></td><td>Discount:</td><td></td><td></td><td>" + totalDiscountReturn.ToString("0,0.00") + "</td></tr>";
            htmlTableReturn += @"<tr><td></td><td>VAT:</td><td></td><td></td><td>" + totalVatReturn.ToString("0,0.00") + "</td></tr>";
            htmlTableReturn += @"<tr><td colspan='5'>....................................................................</td></tr>";
            htmlTableReturn += @"<tr><td></td><td>Return Amount:</td><td></td><td></td><td>" + decimal.Parse((totalPrieReturn - totalDiscountReturn + totalVatReturn).ToString("0,0")).ToString("0,0.00") + "</td></tr>";
            totalReturnAmount = totalPrieReturn - totalDiscountReturn + totalVatReturn;
            htmlTableReturn += @"<tr><td colspan='5'>....................................................................</td></tr></table>";
        }

        htmlTable = htmlTableReturn+ @"<hr/><table id='dataTable' cellspacing='0' border='0' cellpadding='0'><tr class='bordered'><td>SI</td><td style='text-align:center;'>Sales Item(s)</td><td>Qty</td><td>U. Price</td><td>Total Price</td></tr>";
        int count = 1;
        decimal totalPrie = 0;
        decimal totalQty = 0;
        decimal totalDiscount = 0;
        decimal totalVat = 0;
        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            if (!ds.Tables[0].Rows[0]["CashAmount"].ToString().Contains("@"))
            {
                ds.Tables[0].Rows[0]["CashAmount"] = ds.Tables[0].Rows[0]["CashAmount"].ToString() ;
            }
            if (ds.Tables[0].Rows[0]["CardAmount"].ToString().Contains("@"))
            {
                ds.Tables[0].Rows[0]["CardAmount"] = ds.Tables[0].Rows[0]["CardAmount"].ToString().Split('@')[0];
            }

            totalPrie += decimal.Parse(decimal.Parse(dr["SalePrice"].ToString()).ToString("0")) * decimal.Parse(decimal.Parse(dr["Quantity"].ToString()).ToString("0"));
            totalQty += decimal.Parse(dr["Quantity"].ToString());
            totalDiscount += decimal.Parse(dr["Discount"].ToString());
            totalVat += decimal.Parse(dr["Vat"].ToString());
            htmlTable += @"<tr class='bordered'><td>" + (count++).ToString() + "</td><td>" + dr["BarCode"].ToString() + "<br/>" + dr["ProductName"].ToString() + "</td><td>" + decimal.Parse(dr["Quantity"].ToString()).ToString("0,0") + "</td><td>" + decimal.Parse(decimal.Parse(dr["SalePrice"].ToString()).ToString("0,0")).ToString("0,0.00") + "</td><td>" + decimal.Parse((decimal.Parse(dr["SalePrice"].ToString()) * decimal.Parse(dr["Quantity"].ToString())).ToString("0,0")).ToString("0,0.00") + "</td></tr>";
        }

        decimal dicountPercentage = (totalDiscount / decimal.Parse(totalPrie.ToString("0,0")) * 100);
        htmlTable += @"<tr><td colspan='5'>....................................................................</td></tr>";
        htmlTable += @"<tr><td></td><td>Sub Total:</td><td>" + totalQty.ToString("0,0") + "</td><td></td><td>" + decimal.Parse(totalPrie.ToString("0,0")).ToString("0,0.00") + "</td></tr>";
        htmlTable += @"<tr><td></td><td>Discount:</td><td>" + dicountPercentage.ToString("0") + @"%</td><td></td><td>" + totalDiscount.ToString("0,0.00") + "</td></tr>";
        htmlTable += @"<tr><td></td><td>VAT:</td><td></td><td></td><td>" + totalVat.ToString("0,0.00") + "</td></tr>";
        htmlTable += @"<tr><td colspan='5'>....................................................................</td></tr>";

        htmlTable += @"<tr><td></td><td>Due Amount:</td><td></td><td></td><td>" + decimal.Parse((totalPrie - totalDiscount + totalVat).ToString("0,0")).ToString("0,0.00") + "</td></tr>";
        if (IsSalesReturnInvoice)
            htmlTable += @"<tr><td></td><td>Return Amount:</td><td></td><td></td><td>" + decimal.Parse(totalReturnAmount.ToString("0,0")).ToString("0,0.00") + "</td></tr>";
        htmlTable += @"<tr><td></td><td>" + (IsSalesReturnInvoice ? "Currently " : "") + "Paid:</td><td></td><td></td><td>" + decimal.Parse((totalPrie - totalDiscount + totalVat - totalReturnAmount + decimal.Parse(ds.Tables[0].Rows[0]["Due"].ToString())).ToString("0,0")).ToString("0,0.00") + "</td></tr>";
        htmlTable += @"<tr><td></td><td>Change:</td><td></td><td></td><td>" + ds.Tables[0].Rows[0]["Due"].ToString() + @"</td></tr>";
        htmlTable += @"<tr><td colspan='5'>....................................................................</td></tr>";
        htmlTable += @"<tr><td colspan='5' style='text-align:Center;font-size: 14px;color:Red; font-weight: bold;'>Mode Of Payment</td></tr>";
        if (decimal.Parse(ds.Tables[0].Rows[0]["CashAmount"].ToString()) != 0)
        {
            htmlTable += @"<tr><td></td><td>Cash:</td><td></td><td></td><td>" +( decimal.Parse(ds.Tables[0].Rows[0]["CashAmount"].ToString()) - decimal.Parse(ds.Tables[0].Rows[0]["Due"].ToString())).ToString("0,0.00") + "</td></tr>";
        }

        if (decimal.Parse(ds.Tables[0].Rows[0]["CardAmount"].ToString()) != 0)
        {
            htmlTable += @"<tr><td></td><td>Credit:</td><td></td><td></td><td>" + decimal.Parse(ds.Tables[0].Rows[0]["CardAmount"].ToString()).ToString("0,0.00") + "</td></tr>";
        }
        htmlTable += @"<tr><td colspan='5'>....................................................................</td></tr>";

        if (IsSalesReturnInvoice)
            htmlTable += @"<tr><td colspan='5' style='text-align:center;'>No More Change / No Cash Back<br/><br/></td></tr>";
        else
        {
            htmlTable += @"<tr><td colspan='5' style='text-align:center;'>Changeable  within 7 days</td></tr>";
            htmlTable += @"<tr><td colspan='5' style='text-align:center;'>No change after use</td></tr>";
            htmlTable += @"<tr><td colspan='5' style='text-align:center;'>No Cash back</td></tr>";
        
            
        }
        htmlTable += @"<tr><td colspan='5' style='text-align:center;color:red;font-size:20px;'>*Thanks*</td></tr>";
        htmlTable += @"<tr><td colspan='5'>....................................................................</td></tr>";
        htmlTable += @"<tr><td colspan='5' style='text-align:center;'>Powered by MAVRICK-IT [www.mavrickit.com]</td></tr>";

        htmlTable += "</table>";

        lblDetails.Text = htmlTable;
    }
   
}