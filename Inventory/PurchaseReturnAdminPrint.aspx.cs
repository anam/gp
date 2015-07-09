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
        int ReturnID = int.Parse(Request.QueryString["ReturnID"]);
        Inv_PurchaseReturen purchaseReturn = Inv_PurchaseReturenManager.GetInv_PurchaseReturenByID(ReturnID);

        //purchase info
        lblPurchaseDate.Text = purchaseReturn.PurchseReturenDate.ToString("dd-MMM-yyyy");
        lblPurchaseReturnID.Text = purchaseReturn.Inv_PurchaseReturenID.ToString();
        lblRefPurchaseNo.Text = purchaseReturn.PurchaseIDs;
        lblSupplierName.Text = purchaseReturn.ExtraField1;
        lblParticulars.Text = purchaseReturn.ExtraField2;

        //Item Info
        List<Inv_Item> items = new List<Inv_Item>();
        items = Inv_ItemManager.GetAllInv_ItemsByPurchaseReturnID(ReturnID);

        string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Product Code</td>
                            <td>Product Name</td>
                            <td>Quantity</td>
                            <td>Unit</td>
                            <td>Price</td>
                            <td>Amount</td>
                        </tr>";
        int lastRawMaterialID = 0;
        decimal Total = 0;
        decimal Subtotal = 0;
        decimal TotalAmount = 0;
        decimal SubtotalAmount = 0;
        int serialNo=1;
        foreach (Inv_Item item in items)
        {
            if (lastRawMaterialID != 0 && lastRawMaterialID != item.RawMaterialID)
            {
                htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Sub Total</td>
                            <td>" + Subtotal.ToString("0,0.00") + @"</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
                        </tr>";

                Subtotal = 0;
                SubtotalAmount = 0;
            }

            if (lastRawMaterialID != item.RawMaterialID)
            {
                lastRawMaterialID = item.RawMaterialID;
                htmlTable += @"<tr>
                            <td colspan='7' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;font-weight:bold;'>
                                Item: "+ item.RawMaterialName +@"
                            </td>
                        </tr>";
            }

            htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>"+(serialNo++).ToString() +@"</td>
                            <td>"+item.ItemCode+@"</td>
                            <td>"+item.ItemName+@"</td>
                            <td style='text-align:right;'>"+item.PurchasedQuantity.ToString("0,0.00")+@"</td>
                            <td >" + item.QuantityUnitName + @"</td>
                            <td style='text-align:right;'>" + item.PricePerUnit.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + item.PurchasedQuantityPrice.ToString("0,0.00") + @"</td>
                        </tr>";

            Subtotal += item.PurchasedQuantity;
            SubtotalAmount += item.PurchasedQuantityPrice;

            Total += item.PurchasedQuantity;
            TotalAmount += item.PurchasedQuantityPrice;
        }

        htmlTable += @"<tr class='subtotalRow'>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Sub Total</td>
                        <td>" + Subtotal.ToString("0,0.00") + @"</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
                    </tr>";

        htmlTable += @"<tr id='lastRow'>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Grand Total</td>
                        <td>" + Total.ToString("0,0.00") + @"</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>" + TotalAmount.ToString("0,0.00") + @"</td>
                    </tr></table>";

        lblItemList.Text = htmlTable;
        
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
	                        , Inv_PurchaseReturn.WorkSatationID
                            ,Inv_ItemTransaction.ExtraField1 as Inv_IssueDetailsID
                        FROM Inv_ItemTransaction 
                        INNER JOIN Inv_PurchaseReturn ON Inv_ItemTransaction.ReferenceID = Inv_PurchaseReturn.Inv_PurchaseReturenID
                        where Inv_ItemTransaction.ItemTrasactionTypeID=1
                        and Inv_PurchaseReturn.Inv_PurchaseReturenID = " + Request.QueryString["ReturnID"];

        DataSet ds = CommonManager.SQLExec(sql);
        sql = "";
        string finalSQL = @"
                            Delete Inv_PurchaseReturn where  Inv_PurchaseReturn.Inv_PurchaseReturenID = " + Request.QueryString["ReturnID"] + @";
Delete ACC_JournalDetail where JournalMasterID in (Select ACC_JournalMasterID  FROM [ACC_JournalMaster]
  where Note = 'Purchase Return "+decimal.Parse(Request.QueryString["ReturnID"]).ToString("0.00")+ @"');
Delete ACC_JournalMaster  where Note = 'Purchase Return " + decimal.Parse(Request.QueryString["ReturnID"]).ToString("0.00") + @"'
";
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            finalSQL += "Update Inv_Item set ExtraFieldQuantity3 -= " + dr["Quantity"].ToString()
                    + ",ExtraFieldQuantity1+= " + dr["Quantity"].ToString() + @" where Inv_ItemID=" + dr["ItemID"].ToString() + @";
                    Delete Inv_ItemTransaction where Inv_ItemTransactionID= " + dr["Inv_ItemTransactionID"].ToString() + @"           
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