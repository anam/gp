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
        }
    }

    private void loadData()
    {
        int Pos_TransactionMasterID = int.Parse(Request.QueryString["Pos_TransactionMasterID"]);

        string sql = @"select   Pos_TransactionMaster.TransactionDate,
                                Pos_TransactionMaster.TransactionID,
                                Pos_TransactionMaster.ToOrFromID as CustomerID,
                                SR.ChartOfAccountLabel4Text as WorkStationName,
                                SR.ExtraField1 as ShowRoomAddress,
                                Pos_TransactionMaster.ExtraField1 as CashAmount, 
                                Pos_TransactionMaster.ExtraField2 as CardAmount, 
                                Pos_TransactionMaster.ExtraField3 as CustomerName, 
                                Pos_TransactionMaster.ExtraField4 as ContactNo,
                                Pos_TransactionMaster.ExtraField5 as CardNo,
                                SP.ChartOfAccountLabel4Text as SalesPerson
                            From
                            Pos_TransactionMaster
                            left outer join ACC_ChartOfAccountLabel4 as SR on SR.ACC_ChartOfAccountLabel4ID = Pos_TransactionMaster.WorkSatationID
                            left outer join ACC_ChartOfAccountLabel4 as SP on SP.ACC_ChartOfAccountLabel4ID = cast(Pos_TransactionMaster.Record as int)
                            where Pos_TransactionMaster.Pos_TransactionMasterID=" + Pos_TransactionMasterID.ToString() + @";

                            select Pos_Product.SalePrice,
                            Pos_Transaction.Quantity,
                            Pos_Product.ProductName,
                            Pos_Product.BarCode,
                            Pos_Transaction.ExtraField1 as Discount,
                            Pos_Transaction.ExtraField2 as Vat
                            from Pos_Product
                            inner join Pos_Transaction on Pos_Transaction.Pos_ProductID = Pos_Product.Pos_ProductID
                            where Pos_Transaction.Pos_ProductTransactionMasterID=" + Pos_TransactionMasterID.ToString() + @"
                            order by Pos_Product.ProductName;";

        DataSet ds = CommonManager.SQLExec(sql);

        string htmlTable = "";

        htmlTable += @"
                        <table>
                        <tr>
                        <td>SP</td>
                        <td>:</td>
                        <td>" + ds.Tables[0].Rows[0]["SalesPerson"].ToString() + @"</td>
                        </tr>
                        <tr>
                        <td>Date</td>
                        <td>:</td>
                        <td>" + DateTime.Parse(ds.Tables[0].Rows[0]["TransactionDate"].ToString()).ToString("dd-MMM-yyyy hh:mm tt") + @"</td>
                        </tr>
                        <tr>
                        <td>Return Invoice#</td>
                        <td>:</td>
                        <td>" + Request.QueryString["rtnInv"] + @"</td>
                        </tr>
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
                        <td>" + ds.Tables[0].Rows[0]["WorkStationName"].ToString() + ", " + ds.Tables[0].Rows[0]["ShowRoomAddress"].ToString() + @"</td>
                        </tr>
                        </table>
                        ";
        lblMaster.Text = htmlTable;

        htmlTable = @"<hr/>Sales<hr/><table id='dataTable' cellspacing='0' border='0' cellpadding='0'><tr class='bordered'><td>SI</td><td style='text-align:center;'>Item</td><td>Qty</td><td>U. Price</td><td>Total Price</td></tr>";
        int count = 1;
        decimal totalPrie = 0;
        decimal totalQty = 0;
        decimal totalDiscount = 0;
        decimal totalVat = 0;
        foreach (DataRow dr in ds.Tables[1].Rows)
        {
            totalPrie += decimal.Parse(dr["SalePrice"].ToString()) * decimal.Parse(dr["Quantity"].ToString());
            totalQty += decimal.Parse(dr["Quantity"].ToString());
            totalDiscount += decimal.Parse(dr["Discount"].ToString());
            totalVat += decimal.Parse(dr["Vat"].ToString());
            htmlTable += @"<tr class='bordered'><td>" + (count++).ToString() + "</td><td>" + dr["BarCode"].ToString() + "<br/>" + dr["ProductName"].ToString() + "</td><td>"+decimal.Parse(dr["Quantity"].ToString()).ToString("0,0")+"</td><td>" + decimal.Parse(dr["SalePrice"].ToString()).ToString("0,0.00") + "</td><td>" + (decimal.Parse(dr["SalePrice"].ToString()) * decimal.Parse(dr["Quantity"].ToString())).ToString("0,0.00") + "</td></tr>";
        }

        htmlTable += @"<tr><td colspan='5'>....................................................................</td></tr>";
        htmlTable += @"<tr><td></td><td>Sub Total:</td><td>" + totalQty.ToString("0,0") + "</td><td></td><td>" + totalPrie.ToString("0,0.00") + "</td></tr>";
        htmlTable += @"<tr><td></td><td>Discount:</td><td></td><td></td><td>" + totalDiscount.ToString("0,0.00") + "</td></tr>";
        htmlTable += @"<tr><td></td><td>VAT:</td><td></td><td></td><td>" + totalVat.ToString("0,0.00") + "</td></tr>";
        htmlTable += @"<tr><td colspan='5'>....................................................................</td></tr>";

        htmlTable += @"<tr><td></td><td>Due Amount:</td><td></td><td></td><td>" + (totalPrie - totalDiscount + totalVat).ToString("0,0.00") + "</td></tr>";
        htmlTable += @"<tr><td></td><td>Paid:</td><td></td><td></td><td>" + (totalPrie - totalDiscount + totalVat).ToString("0,0.00") + "</td></tr>";
        htmlTable += @"<tr><td></td><td>Change:</td><td></td><td></td><td>0.00</td></tr>";
        htmlTable += @"<tr><td colspan='5'>....................................................................</td></tr>";
        htmlTable += @"<tr><td colspan='5' style='text-align:Center;font-size: 14px;color:Red; font-weight: bold;'>Mode Of Payment</td></tr>";
        if (decimal.Parse(ds.Tables[0].Rows[0]["CashAmount"].ToString()) != 0)
        {
            htmlTable += @"<tr><td></td><td>Cash:</td><td></td><td></td><td>" + decimal.Parse(ds.Tables[0].Rows[0]["CashAmount"].ToString()).ToString("0,0.00") + "</td></tr>";
        }

        if (decimal.Parse(ds.Tables[0].Rows[0]["CardAmount"].ToString()) != 0)
        {
            htmlTable += @"<tr><td></td><td>Credit:</td><td></td><td></td><td>" + decimal.Parse(ds.Tables[0].Rows[0]["CardAmount"].ToString()).ToString("0,0.00") + "</td></tr>";
        }
        htmlTable += @"<tr><td colspan='5'>....................................................................</td></tr>";

        htmlTable += "</table>";

        lblDetails.Text = htmlTable;
    }
   
}