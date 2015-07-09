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

        int RawmaterialsTypeID = 0;
        try
        {
            RawmaterialsTypeID = Int32.Parse(Request.QueryString["RawmaterialsTypeID"]);
        }
        catch (Exception ex)
        {
            RawmaterialsTypeID = 0;
        }

        int purchaseID = 0;
        try
        {
            purchaseID = Int32.Parse(Request.QueryString["purchaseID"]);
        }
        catch (Exception ex)
        {
            purchaseID = 0;
        }


        //purchase info
        string sql = @"select PurchasedQuantity
,(Inv_Item.ExtraFieldQuantity1 * Inv_Item.PricePerUnit) as StoreTotalAmount
,Inv_Item.ExtraFieldQuantity2  as PurchaseAdjustMent
,Inv_Item.ExtraFieldQuantity3  as PurchaseReturn
,Inv_Item.ExtraFieldQuantity5  as IssuedQuantity
,Inv_Item.IssueReturedQuantity
,Inv_Item.ExtraFieldQuantity4  as WastageQuantity
,Inv_Item.UtilizedQuantity
,Inv_Item.ExtraFieldQuantity1  as AvailableInStore
,Inv_QuantityUnit.QuantityUnitName
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,ACC_ChartOfAccountLabel4.ExtraField1 
,Inv_Item.ItemCode,Inv_Item.RawMaterialID
from
Inv_Item inner join Inv_Purchase on Inv_Purchase.Inv_PurchaseID = Inv_Item.PurchaseID
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID= Inv_Item.RawMaterialID
inner join ACC_ChartOfAccountLabel4 as Supplier on Supplier.ACC_ChartOfAccountLabel4ID= Inv_Purchase.SuppierID
inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID=Inv_Item.QuantityUnitID
where Inv_Item.RowStatusID=1 
";
        
        if (RawmaterialsTypeID != 0)
        {
            sql += " and ACC_ChartOfAccountLabel4.ACC_HeadTypeID =" + RawmaterialsTypeID;
        }

        if (purchaseID != 0)
        {
            sql += " and Inv_Purchase.Inv_PurchaseID =" + purchaseID;
        }
        
            sql += " and Inv_Item.ExtraFieldQuantity1>0 " ;
sql +=@"
order by Inv_Item.RawMaterialID,ItemCode;";

        
        DataSet ds= CommonManager.SQLExec(sql);


        string RawMaterialID = "0";

        decimal PurchasedQuantity = 0;
        decimal PurchaseAdjustMent = 0;
        decimal PurchaseReturn = 0;
        decimal IssuedQuantity = 0;
        decimal IssueReturedQuantity = 0;
        decimal WastageQuantity = 0;
        decimal UtilizedQuantity = 0;
        decimal AvailableInStore = 0;
        decimal StoreTotalAmount = 0;

        decimal PurchasedQuantityTotal = 0;
        decimal PurchaseAdjustMentTotal =0;
        decimal PurchaseReturnTotal =0;
        decimal IssuedQuantityTotal =0;
        decimal IssueReturedQuantityTotal =0;
        decimal WastageQuantityTotal =0;
        decimal UtilizedQuantityTotal =0;
        decimal AvailableInStoreTotal =0;
        decimal StoreTotalAmountTotal =0;
            int serialNo = 1;

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        ";



            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (RawMaterialID != "0" && RawMaterialID != dr["RawMaterialID"].ToString())
                {
                    htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Total</td>
                            <td style='text-align:right;'>" + PurchasedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + PurchaseAdjustMent.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + PurchaseReturn.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + IssuedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + IssueReturedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + WastageQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + UtilizedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + AvailableInStore.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + StoreTotalAmount.ToString("0,0.00") + @"</td>
                        </tr>";

                    PurchasedQuantity = 0;
                    PurchaseAdjustMent = 0;
                    PurchaseReturn = 0;
                    IssuedQuantity = 0;
                    IssueReturedQuantity = 0;
                    WastageQuantity = 0;
                    UtilizedQuantity = 0;
                    AvailableInStore = 0;
                    StoreTotalAmount = 0;
                }

                if (RawMaterialID != dr["RawMaterialID"].ToString())
                {
                    RawMaterialID = dr["RawMaterialID"].ToString();
                    htmlTable += @"<tr>
                            <td colspan='13' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;'>
                                <b>Item:</b> " + dr["ChartOfAccountLabel4Text"].ToString() + @"
                            </td>
                        </tr>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Item Code</td>
                            <td>Item Name</td>
                            <td>Unit</td>
                            <td>Purchase<br/>Quantity</td>
                            <td>Purchase<br/>Adjustment<br/>Quantity</td>
                            <td>Purchase<br/>Return<br/>Quantity</td>
                            <td>Issued<br/>Quantity</td>
                            <td>Issue<br/>Return<br/>Quantity</td>
                            <td>Wastage<br/>Quantity</td>
                            <td>Utlized<br/>Quantity</td>
                            <td>Stock<br/>Quantity</td>
                            <td>Stock<br/>Amount</td>
                        </tr>";
                }

                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + dr["ItemCode"].ToString() + @"</td>
                            <td>" + dr["ChartOfAccountLabel4Text"].ToString() + @"</td>
                            <td >" + dr["QuantityUnitName"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["PurchasedQuantity"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["PurchaseAdjustMent"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["PurchaseReturn"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["IssuedQuantity"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["IssueReturedQuantity"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["WastageQuantity"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["UtilizedQuantity"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["AvailableInStore"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["StoreTotalAmount"].ToString()).ToString("0,0.00") + @"</td>
                        </tr>";

                PurchasedQuantity += decimal.Parse(dr["PurchasedQuantity"].ToString());
                PurchaseAdjustMent += decimal.Parse(dr["PurchaseAdjustMent"].ToString());
                PurchaseReturn += decimal.Parse(dr["PurchaseReturn"].ToString());
                IssuedQuantity += decimal.Parse(dr["IssuedQuantity"].ToString());
                IssueReturedQuantity += decimal.Parse(dr["IssueReturedQuantity"].ToString());
                WastageQuantity += decimal.Parse(dr["WastageQuantity"].ToString());
                UtilizedQuantity += decimal.Parse(dr["UtilizedQuantity"].ToString());
                AvailableInStore += decimal.Parse(dr["AvailableInStore"].ToString());
                StoreTotalAmount += decimal.Parse(dr["StoreTotalAmount"].ToString());

                PurchasedQuantityTotal  += decimal.Parse(dr["PurchasedQuantity"].ToString());
                PurchaseAdjustMentTotal  += decimal.Parse(dr["PurchaseAdjustMent"].ToString());
                PurchaseReturnTotal  += decimal.Parse(dr["PurchaseReturn"].ToString());
                IssuedQuantityTotal  += decimal.Parse(dr["IssuedQuantity"].ToString());
                IssueReturedQuantityTotal  += decimal.Parse(dr["IssueReturedQuantity"].ToString());
                WastageQuantityTotal  += decimal.Parse(dr["WastageQuantity"].ToString());
                UtilizedQuantityTotal  += decimal.Parse(dr["UtilizedQuantity"].ToString());
                AvailableInStoreTotal  += decimal.Parse(dr["AvailableInStore"].ToString());
                StoreTotalAmountTotal  += decimal.Parse(dr["StoreTotalAmount"].ToString());
            }

            htmlTable += @"<tr class='subtotalRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Total</td>
                            <td style='text-align:right;'>" + PurchasedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + PurchaseAdjustMent.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + PurchaseReturn.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + IssuedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + IssueReturedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + WastageQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + UtilizedQuantity.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + AvailableInStore.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + StoreTotalAmount.ToString("0,0.00") + @"</td>
                        </tr>";


            htmlTable += @"<tr id='lastRow'>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>Total</td>
                            <td style='text-align:right;'>" + PurchasedQuantityTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + PurchaseAdjustMentTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + PurchaseReturnTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + IssuedQuantityTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + IssueReturedQuantityTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + WastageQuantityTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + UtilizedQuantityTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + AvailableInStoreTotal.ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + StoreTotalAmountTotal.ToString("0,0.00") + @"</td>
                        </tr></table>";


       

        lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}