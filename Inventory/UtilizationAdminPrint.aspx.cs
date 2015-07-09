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
            try
            {
                loadData();
            }
            catch (Exception ex)
            {
                showAlartMessage("This transaciton has deleted");
            }
        }
    }

    private void loadData()
    {
        int adjustmentID = int.Parse(Request.QueryString["UtilizationID"]);
        Inv_Utilization purchaseAdjustment = Inv_UtilizationManager.GetInv_UtilizationByID(adjustmentID);

        //purchase info
        lblPurchaseDate.Text = purchaseAdjustment.UtilizationDate.ToString("dd-MMM-yyyy");
        lblPurchaseReturnID.Text = purchaseAdjustment.Inv_UtilizationID.ToString();
        //lblRefPurchaseNo.Text = purchaseAdjustment.PurchaseIDs;
        //lblSupplierName.Text = purchaseAdjustment.ExtraField1;
        lblParticulars.Text = purchaseAdjustment.ExtraField2;

        //Item Info
        List<Inv_Item> items = new List<Inv_Item>();
        items = Inv_ItemManager.GetAllInv_ItemsByUtilizationID(adjustmentID);

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
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        string sql = @"SELECT  Inv_ItemTransaction.Inv_ItemTransactionID
	                        , Inv_ItemTransaction.ItemID
	                        , Inv_ItemTransaction.Quantity
	                        , Inv_Utilization.WorkSatationID
                            , Inv_Utilization.ExtraField1 as JournalMasterID
	                        ,Inv_ItemTransaction.ExtraField3 as Inv_IssueDetailsID
                        FROM Inv_ItemTransaction 
                        INNER JOIN Inv_Utilization ON Inv_ItemTransaction.ReferenceID = Inv_Utilization.Inv_UtilizationID
                        where Inv_ItemTransaction.ItemTrasactionTypeID=4
                        and Inv_Utilization.Inv_UtilizationID = " + Request.QueryString["UtilizationID"];

        DataSet ds = CommonManager.SQLExec(sql);
        sql = "";
        string finalSQL= @"
                            Delete Inv_Utilization where  Inv_Utilization.Inv_UtilizationID = " + Request.QueryString["UtilizationID"]+ @";
                            Update ACC_JournalMaster set RowStatusID=3 where ACC_JournalMasterID = " + ds.Tables[0].Rows[0]["JournalMasterID"].ToString() + @";
                            Update ACC_JournalDetail set RowStatusID=3 where JournalMasterID = " + ds.Tables[0].Rows[0]["JournalMasterID"].ToString() + @";
                            Delete Inv_UtilizationDetails  where Inv_UtilizationID= " + Request.QueryString["UtilizationID"] + @";
                        ";
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            finalSQL+="Update Inv_Item set UtilizedQuantity-= "+dr["Quantity"].ToString()
                    + "  where Inv_ItemID=" + dr["ItemID"].ToString() + @";
                    Delete Inv_ItemTransaction where Inv_ItemTransactionID= " + dr["Inv_ItemTransactionID"].ToString() + @"           
                    ";

            if (dr["Inv_IssueDetailsID"].ToString() == "")//issue Details ID not specified
            {
                sql = @"select Inv_IssueDetail.Inv_IssueDetailID
                        ,Inv_IssueDetail.ExtraField1
                        ,Inv_IssueDetail.ExtraField3
                        ,Inv_IssueDetail.ExtraField4
                        ,Inv_IssueDetail.AdditionalWithIssueDetailID
                        ,Inv_IssueMaster.WorkSatationID 
                        from Inv_IssueDetail 
                        inner join Inv_IssueMaster on Inv_IssueDetail.ExtraField5 = Inv_IssueMaster.Inv_IssueMasterID
                        where Inv_IssueDetail.RowStatusID=1 and Inv_IssueMaster.RowStatusID=1
                        and Inv_IssueMaster.WorkSatationID = " + dr["WorkSatationID"].ToString() + @"
                        and Inv_IssueDetail.ItemID =" + dr["ItemID"].ToString() + @" ;";

                DataSet dsIssueDetails = CommonManager.SQLExec(sql);
                bool notFound = true;
                foreach (DataRow drIssueDetails in dsIssueDetails.Tables[0].Rows)
                {
                    /*
                     
                      //update Inv_IssueDetail
                sql += "Update Inv_IssueDetail set ExtraField1 =(cast ((cast(ExtraField1 as decimal(18,2))-" + txtUtilization.Text + ") as nvarchar)) " +
                        @", ExtraField3 =(cast ((cast(ExtraField3 as decimal(18,2))+" + txtUtilization.Text + ") as nvarchar)) "
                        + (txtWasted.Text == "0" ? "" : ", ExtraField4 =(cast ((cast(ExtraField4 as decimal(18,2))+" + txtWasted.Text + ") as nvarchar)) ")
                        + (lblProductName.Text == "N/A" ? "" : ", AdditionalWithIssueDetailID+=" + txtProductionQuantity.Text )                
                        +"  where Inv_IssueDetailID=" + hfInv_IssueDetailID.Value + ";";
                
                //update Inv_Item
                sql += "Update Inv_Item set UtilizedQuantity+= "+txtUtilization.Text
                        + (txtWasted.Text == "0" ? "" : ", ExtraFieldQuantity4 +=" + txtWasted.Text + " ")
                    + "  where Inv_ItemID=" + hfItemID.Value + ";";

                     */

                    if (decimal.Parse(drIssueDetails["ExtraField3"].ToString()) >= decimal.Parse(dr["Quantity"].ToString()))
                    {
                        finalSQL += "Update Inv_IssueDetail set ExtraField1 =(cast ((cast(ExtraField1 as decimal(18,2))+" + dr["Quantity"].ToString() + ") as nvarchar)) " +
                         @", ExtraField3 =(cast ((cast(ExtraField3 as decimal(18,2))-" + dr["Quantity"].ToString() + ") as nvarchar)) "
                         + "  where Inv_IssueDetailID=" + drIssueDetails["Inv_IssueDetailID"].ToString() + ";";
                        notFound = false;
                        break;
                    }
                }

                if (notFound)
                {
                    showAlartMessage("Something problem please contact Anam(01818619647)");
                    return;
                }
            }
            else
            {
                finalSQL += "Update Inv_IssueDetail set ExtraField1 =(cast ((cast(ExtraField1 as decimal(18,2))+" + dr["Quantity"].ToString() + ") as nvarchar)) " +
                     @", ExtraField3 =(cast ((cast(ExtraField3 as decimal(18,2))-" + dr["Quantity"].ToString() + ") as nvarchar)) "
                     + "  where Inv_IssueDetailID=" + dr["Inv_IssueDetailsID"].ToString() + ";";
                   
            }
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