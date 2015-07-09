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

        int suppliyerID = 0;
        try
        {
            suppliyerID = Int32.Parse(Request.QueryString["SuppliyerID"]);
        }
        catch (Exception ex)
        {
            suppliyerID = 0;
        }

        int RawmaterialsTypeID = 0;
        try
        {
            RawmaterialsTypeID = Int32.Parse(Request.QueryString["RawmaterialsTypeID"]);
        }
        catch (Exception ex)
        {
            RawmaterialsTypeID = 0;
        }


        string fromDate = (Request.QueryString["FromDate"] != null ? Request.QueryString["FromDate"].ToString() : "");
        string toDate = (Request.QueryString["ToDate"] != null ? Request.QueryString["ToDate"].ToString() : "");

        //purchase info
        string sql = @"select SUM(PurchasedQuantity) as PurchasedQuantity
,SUM(Inv_Item.ExtraFieldQuantity1 * Inv_Item.PricePerUnit) as StoreTotalAmount
,SUM(Inv_Item.ExtraFieldQuantity2)  as PurchaseAdjustMent
,SUM(Inv_Item.ExtraFieldQuantity3)  as PurchaseReturn
,SUM(Inv_Item.ExtraFieldQuantity5)  as IssuedQuantity
,SUM(Inv_Item.IssueReturedQuantity)  as IssueReturedQuantity
,SUM(Inv_Item.ExtraFieldQuantity4)  as WastageQuantity
,SUM(Inv_Item.LostQuantity)  as LostQuantity
,SUM(Inv_Item.UtilizedQuantity)  as UtilizedQuantity
,SUM(Inv_Item.ExtraFieldQuantity1)  as AvailableInStore
,Inv_QuantityUnit.QuantityUnitName
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,ACC_ChartOfAccountLabel4.ExtraField1

--,Supplier.ChartOfAccountLabel4Text as SupplierName
--,Supplier.ACC_ChartOfAccountLabel4ID as SupplierID
--,Supplier.ExtraField2 as SupplierAddress
from
Inv_Item inner join Inv_Purchase on Inv_Purchase.Inv_PurchaseID = Inv_Item.PurchaseID
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID= Inv_Item.RawMaterialID
inner join ACC_ChartOfAccountLabel4 as Supplier on Supplier.ACC_ChartOfAccountLabel4ID= Inv_Purchase.SuppierID
inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID=Inv_Item.QuantityUnitID
 where   Inv_Item.RowStatusID=1 ";
         if (RawmaterialsTypeID != 0)
        {
            sql += " and ACC_ChartOfAccountLabel4.ACC_HeadTypeID =" + RawmaterialsTypeID;
        }
sql += @"  group by Inv_QuantityUnit.QuantityUnitName
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,ACC_ChartOfAccountLabel4.ExtraField1
--,Supplier.ChartOfAccountLabel4Text
--,Supplier.ACC_ChartOfAccountLabel4ID
--,Supplier.ExtraField2
order by 
--Supplier.ChartOfAccountLabel4Text
--,
ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text";

        DataSet ds= CommonManager.SQLExec(sql);


        decimal PurchasedQuantity = 0;
        decimal PurchaseAdjustMent = 0;
        decimal PurchaseReturn = 0;
        decimal IssuedQuantity = 0;
        decimal IssueReturedQuantity = 0;
        decimal WastageQuantity = 0;
        decimal UtilizedQuantity = 0;
        decimal AvailableInStore = 0;
        decimal StoreTotalAmount = 0;
            
        int serialNo = 1;

            string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
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
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                htmlTable += @"<tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + dr["ExtraField1"].ToString() + @"</td>
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
                        </tr></table>";


        lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}