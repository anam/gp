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
        int IssueReturnID = int.Parse(Request.QueryString["IssueReturnID"]);

        //purchase info
        Inv_IssueMasterReturn issueMasterReturn = Inv_IssueMasterReturnManager.GetInv_IssueMasterReturnByID(IssueReturnID);
        if (issueMasterReturn == null)
        {
            showAlartMessage("Deleted");
            return;
        }
        lblPurchaseDate.Text = issueMasterReturn.IssueReturnDate.ToString("dd-MMM-yyyy");
        lblPurchaseID.Text = issueMasterReturn.Inv_IssueMasterReturnID.ToString();
        //lblInvoiceNo.Text = purchase.InvoiceNo;
        lblSupplierName.Text = issueMasterReturn.ExtraField1;
        lblParticulars.Text = issueMasterReturn.Particulars;

        if (issueMasterReturn.IsIssue)
        {
            #region Productive issue

            //Item Info
            List<Inv_IssueDetail> inv_IssueDetailsFromDB = Inv_IssueDetailManager.GetAllInv_IssueDetailsByIssueMasterReturnID(IssueReturnID.ToString());

            List<Inv_IssueDetail> inv_IssueDetailsArranged = new List<Inv_IssueDetail>();

            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
            {
                AddedIssedItem.IsProcessed = false;
            }

            foreach (Inv_IssueDetail rootIssedItem in inv_IssueDetailsFromDB)
            {
                if (rootIssedItem.AdditionalWithIssueDetailID == 0)
                {
                    rootIssedItem.ParentChildGap = "";
                    rootIssedItem.IsProcessed = true;
                    inv_IssueDetailsArranged.Add(rootIssedItem);
                    foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
                    {
                        if (AddedIssedItem.AdditionalWithIssueDetailID == rootIssedItem.Inv_IssueDetailID)
                        {
                            AddedIssedItem.IsProcessed = true;
                            AddedIssedItem.ParentChildGap = "----";
                            inv_IssueDetailsArranged.Add(AddedIssedItem);
                        }
                    }
                }
            }

            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
            {
                if (!AddedIssedItem.IsProcessed)
                {
                    AddedIssedItem.IsProcessed = true;
                    AddedIssedItem.ParentChildGap = "";
                    inv_IssueDetailsArranged.Add(AddedIssedItem);
                }
            }

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>SL no</td>
                            <td>Code</td>
                            <td>Description</td>
                            <td>Quantity</td>
                            <td>Unit</td>
                            <td>Unit price</td>
                            <td>Amount</td>
                            <td>Quality</td>
                            <td>Issue For</td>
                            <td>Qppx. Qty</td>
                        </tr>";
            int lastProductID = 0;
            
            decimal subTotalQuantity = 0;
            decimal subTotalAmount = 0;
            decimal subTotalAppxQty = 0;
            
            decimal totalQuantity = 0;
            decimal totalAmount = 0;
            decimal totalAppxQty = 0;
            
            int serialNo = 1;
            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsArranged)
            {
                if (lastProductID != 0 && lastProductID != AddedIssedItem.ProductID)
                {
                    htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + subTotalQuantity.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>" + subTotalAmount.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>" + subTotalAppxQty.ToString("0,0.00") + @"</td>
                        </tr>";

                    subTotalQuantity = 0;
                    subTotalAmount = 0;
                    subTotalAppxQty = 0;
                }

                if (lastProductID != AddedIssedItem.ProductID)
                {
                    lastProductID = AddedIssedItem.ProductID;
                    htmlTable += @"<tr>
                            <td colspan='10' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;font-weight:bold;'>
                                Issued For: " + AddedIssedItem.ProductName + @"
                            </td>
                        </tr>";
                }

                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + AddedIssedItem.ItemCode + @"</td>
                            <td>" + AddedIssedItem.ItemName + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.Quantity.ToString("0,0.00") + @"</td>
                            <td>" + AddedIssedItem.QuantityUnitName + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.PricePerUnit.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.TotalPrice.ToString("0,0.00") + @"</td>
                            <td>" + AddedIssedItem.QualityUnitValue + @" - " + AddedIssedItem.QualityUnitName + @"</td>                          
                            <td>" + AddedIssedItem.ProductName + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.ApproximateQuantity.ToString("0,0.00") + @"</td>
                        </tr>";

                subTotalQuantity += AddedIssedItem.Quantity;
                subTotalAmount += AddedIssedItem.TotalPrice;
                subTotalAppxQty += AddedIssedItem.ApproximateQuantity;

                totalQuantity += AddedIssedItem.Quantity;
                totalAmount += AddedIssedItem.TotalPrice;
                totalAppxQty += AddedIssedItem.ApproximateQuantity;
            }

            htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + subTotalQuantity.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>" + subTotalAmount.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>" + subTotalAppxQty.ToString("0,0.00") + @"</td>
                        </tr>";


            htmlTable += @"<tr  id='lastRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Grand Total</td>
                            <td>" + totalQuantity.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>" + totalAmount.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>" + totalAppxQty.ToString("0,0.00") + @"</td>
                        </tr></table>";

            lblItemList.Text = htmlTable;

            #endregion
        }
        else
        {
            #region Non-productive issue
            //Item Info
            List<Inv_IssueDetail> inv_IssueDetailsFromDB = Inv_IssueDetailManager.GetAllInv_IssueDetailsByIssueMasterReturnID(IssueReturnID.ToString());

            List<Inv_IssueDetail> inv_IssueDetailsArranged = new List<Inv_IssueDetail>();

            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
            {
                AddedIssedItem.IsProcessed = false;
            }

            foreach (Inv_IssueDetail rootIssedItem in inv_IssueDetailsFromDB)
            {
                if (rootIssedItem.AdditionalWithIssueDetailID == 0)
                {
                    rootIssedItem.ParentChildGap = "";
                    rootIssedItem.IsProcessed = true;
                    inv_IssueDetailsArranged.Add(rootIssedItem);
                    foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
                    {
                        if (AddedIssedItem.AdditionalWithIssueDetailID == rootIssedItem.Inv_IssueDetailID)
                        {
                            AddedIssedItem.IsProcessed = true;
                            AddedIssedItem.ParentChildGap = "----";
                            inv_IssueDetailsArranged.Add(AddedIssedItem);
                        }
                    }
                }
            }

            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsFromDB)
            {
                if (!AddedIssedItem.IsProcessed)
                {
                    AddedIssedItem.IsProcessed = true;
                    AddedIssedItem.ParentChildGap = "";
                    inv_IssueDetailsArranged.Add(AddedIssedItem);
                }
            }

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>SL no</td>
                            <td>Product ID</td>
                            <td>Description</td>
                            <td>Quantity</td>
                            <td>Rate</td>
                            <td>Amount</td>
                        </tr>";
            decimal totalQuantity = 0;
            decimal totalAmount = 0;

            int serialNo = 1;
            foreach (Inv_IssueDetail AddedIssedItem in inv_IssueDetailsArranged)
            {
                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + AddedIssedItem.ItemCode + @"</td>
                            <td>" + AddedIssedItem.ItemName + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.Quantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.PricePerUnit.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + AddedIssedItem.TotalPrice.ToString("0,0.00") + @"</td>
                        </tr>";

                totalQuantity += AddedIssedItem.Quantity;
                totalAmount += AddedIssedItem.TotalPrice;
            }

            htmlTable += @"<tr  id='lastRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Grand Total</td>
                            <td>" + totalQuantity.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>" + totalAmount.ToString("0,0.00") + @"</td>
                        </tr></table>";

            lblItemList.Text = htmlTable;
            #endregion
        }
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string sql = @"SELECT  Inv_ItemTransaction.Inv_ItemTransactionID
	                        , Inv_ItemTransaction.ItemID
	                        , Inv_ItemTransaction.Quantity
	                        , Inv_IssueMasterReturn.WorkSatationID
                            ,Inv_ItemTransaction.ExtraField1 as Inv_IssueDetailsID
                        FROM Inv_ItemTransaction 
                        INNER JOIN Inv_IssueMasterReturn ON Inv_ItemTransaction.ReferenceID = Inv_IssueMasterReturn.Inv_IssueMasterReturnID
                        where Inv_ItemTransaction.ItemTrasactionTypeID=3
                        and Inv_IssueMasterReturn.Inv_IssueMasterReturnID = " + Request.QueryString["IssueReturnID"];

        DataSet ds = CommonManager.SQLExec(sql);
        sql = "";
        string finalSQL = @"
                            Delete Inv_IssueMasterReturn where  Inv_IssueMasterReturn.Inv_IssueMasterReturnID = " + Request.QueryString["IssueReturnID"] + @";
                        ";
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            finalSQL += "Update Inv_Item set IssueReturedQuantity-= " + dr["Quantity"].ToString()
                    + ",ExtraFieldQuantity1-= " + dr["Quantity"].ToString() + @" where Inv_ItemID=" + dr["ItemID"].ToString() + @";
                    Delete Inv_ItemTransaction where Inv_ItemTransactionID= " + dr["Inv_ItemTransactionID"].ToString() + @"           
                    
update Inv_IssueDetail
	 set ExtraField1 = (CAST(ExtraField1 as DECIMAL(10, 2)) + " + dr["Quantity"].ToString()+@"),
		ExtraField2	= (CAST(ExtraField2 as DECIMAL(10, 2)) - " + dr["Quantity"].ToString() + @")
	where cast(Inv_IssueDetailID as nvarchar(256)) =" + dr["Inv_IssueDetailsID"].ToString() + @" 
";

        }

        try
        {
            CommonManager.SQLExec(finalSQL);
            showAlartMessage("Successfully done");
        }
        catch (Exception ex)
        {
            showAlartMessage("Something wrong!!! Please inform Anam(01818619647) Right now");
        }
    }
    private void showAlartMessage(string message)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
             "err_msg",
             "alert('" + message + "');",
             true);
    }
}