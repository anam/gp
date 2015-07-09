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

        int workSationID = 0;
        try
        {
            workSationID = Int32.Parse(Request.QueryString["WorkStationID"]);
        }
        catch (Exception ex)
        {
            workSationID = 0;
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
        string sql = @"
 update Inv_ItemTransaction set ExtraField4='0' where ExtraField4='' and ItemTrasactionTypeID=4
  update Inv_ItemTransaction set ExtraField5='0' where ExtraField5='' and ItemTrasactionTypeID=4

select SUM(Inv_ItemTransaction.Quantity) as Quantity,Inv_QuantityUnit.QuantityUnitName
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
,ACC_ChartOfAccountLabel4.ExtraField1
,SUM(Inv_ItemTransaction.Quantity * Inv_Item.PricePerUnit) as TotalAmount  
,SUM(cast(Inv_ItemTransaction.ExtraField1 as decimal(10,2))) as ProductionQty
,SUM(cast(Inv_ItemTransaction.ExtraField4 as decimal(10,2))) as ProductionQtyFresh
,SUM(cast(Inv_ItemTransaction.ExtraField5 as decimal(10,2))) as ProductionQtyReject
,SUM(cast(Inv_ItemTransaction.ExtraField2 as decimal(10,2))) as ApproximateQty
from
Inv_Item inner join Inv_Purchase on Inv_Purchase.Inv_PurchaseID = Inv_Item.PurchaseID
inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID= Inv_Item.RawMaterialID
inner join ACC_ChartOfAccountLabel4 as Supplier on Supplier.ACC_ChartOfAccountLabel4ID= Inv_Purchase.SuppierID
inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID=Inv_Item.QuantityUnitID
inner join Inv_ItemTransaction on Inv_ItemTransaction.ItemID =Inv_Item.Inv_ItemID
inner join Inv_Utilization on Inv_Utilization.Inv_UtilizationID = Inv_ItemTransaction.ReferenceID
where   Inv_ItemTransaction.ItemTrasactionTypeID =4 and  (Inv_Utilization.UtilizationDate between '" + fromDate + "' and '" + toDate + "')";
       
        if (workSationID != 0)
        {
            sql += " and Inv_Utilization.WorkSatationID =" + workSationID;
        }

        

        sql += @" group by Inv_QuantityUnit.QuantityUnitName
                ,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
                ,ACC_ChartOfAccountLabel4.ExtraField1
                order by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text;";


        if (workSationID != 0)
        {
            sql += "select ChartOfAccountLabel4Text,ACC_ChartOfAccountLabel4ID,ExtraField2 from ACC_ChartOfAccountLabel4 where ACC_ChartOfAccountLabel4ID =" + workSationID;
        }
        DataSet ds= CommonManager.SQLExec(sql);
        
        
        string lastSupplierID = "0";
        decimal Total = 0;
        decimal Subtotal = 0;
        decimal TotalAmount = 0;
        decimal SubtotalAmount = 0;

        decimal approximateQuantityTotal = 0;
        decimal approximateQuantitySubTotla = 0;
        decimal ProductionQuantityTotal = 0;
        decimal ProductionQuantitySubTotla = 0;
        decimal ProductionQuantityFreshTotal = 0;
        decimal ProductionQuantityFreshSubTotla = 0;
        decimal ProductionQuantityRejectTotal = 0;
        decimal ProductionQuantityRejectSubTotla = 0;

        int serialNo = 1;

        string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>";
        if (workSationID != 0)
        {
            htmlTable += @"<tr>
                        <td colspan='8' style='padding-left:50px;border-top:1px solid black;'>
                            <b>Factory:</b> " + ds.Tables[1].Rows[0]["ChartOfAccountLabel4Text"].ToString() + @"
                        </td>
                        <td colspan='2' style=' border-top:1px solid black;'>
                        </td>
                    </tr>
                    <tr>
                        <td colspan='10' style='padding-left:50px;'>
                        </td>
                    </tr>
                    <tr>
                        <td colspan='10' style='padding-left:50px; border-bottom:1px solid black;'>
                            <b>Transaction Date:</b> " + DateTime.Parse(fromDate).ToString("dd MMM yyyy") + " To " + DateTime.Parse(toDate).ToString("dd MMM yyyy") + @"
                        </td>
                    </tr>";
        }
        else
        {
            htmlTable += @"<tr>
                        <td colspan='10' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;'>
                            All Factory
                        </td>
                    </tr>";
        }
        
        htmlTable += @"<tr id='tableHeader'>
                        <td  style='border-left:0px;'>S/L</td>
                        <td>Item Code</td>
                        <td>Item Name</td>
                        <td>Quantity</td>
                        <td>Unit</td>
                        <td>Amount</td>
                        <td>Apx Qty</td>
                        <td>Prod Qty(F)</td>
                        <td>Prod Qty(R)</td>
                        <td>Prod Qty(T)</td>
                    </tr>
                    ";


        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            htmlTable += @"<tr class='itemCss'>
                        <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                        <td>" + dr["ExtraField1"].ToString() + @"</td>
                        <td>" + dr["ChartOfAccountLabel4Text"].ToString() + @"</td>
                        <td style='text-align:right;'>" + decimal.Parse(dr["Quantity"].ToString()).ToString("0,0.00") + @"</td>
                        <td >" + dr["QuantityUnitName"].ToString() + @"</td>
                        <td style='text-align:right;'>" + decimal.Parse(dr["TotalAmount"].ToString()).ToString("0,0.00") + @"</td>
                        <td style='text-align:right;'>" + decimal.Parse(dr["ApproximateQty"].ToString()).ToString("0,0.00") + @"</td>
                        <td style='text-align:right;'>" + decimal.Parse(dr["ProductionQtyFresh"].ToString()).ToString("0,0.00") + @"</td>
                        <td style='text-align:right;'>" + decimal.Parse(dr["ProductionQtyReject"].ToString()).ToString("0,0.00") + @"</td>
                        <td style='text-align:right;'>" + decimal.Parse(dr["ProductionQty"].ToString()).ToString("0,0.00") + @"</td>
                    </tr>";

            Subtotal += decimal.Parse(dr["Quantity"].ToString());
            SubtotalAmount += decimal.Parse(dr["TotalAmount"].ToString());

            Total += decimal.Parse(dr["Quantity"].ToString());
            TotalAmount += decimal.Parse(dr["TotalAmount"].ToString());

            approximateQuantitySubTotla += decimal.Parse(dr["ApproximateQty"].ToString());
            ProductionQuantitySubTotla += decimal.Parse(dr["ProductionQty"].ToString());

            approximateQuantityTotal += decimal.Parse(dr["ApproximateQty"].ToString());
            ProductionQuantityTotal += decimal.Parse(dr["ProductionQty"].ToString());

            ProductionQuantityFreshSubTotla += decimal.Parse(dr["ProductionQtyFresh"].ToString());
            ProductionQuantityFreshTotal += decimal.Parse(dr["ProductionQtyFresh"].ToString());
            ProductionQuantityRejectSubTotla += decimal.Parse(dr["ProductionQtyReject"].ToString());
            ProductionQuantityRejectTotal += decimal.Parse(dr["ProductionQtyReject"].ToString());

        }

        htmlTable += @"<tr id='lastRow'>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>Total</td>
                    <td>" + Total.ToString("0,0.00") + @"</td>
                    <td>&nbsp;</td>
                    <td>" + TotalAmount.ToString("0,0.00") + @"</td>
                    <td>" + approximateQuantityTotal.ToString("0,0.00") + @"</td>
                    <td>" + ProductionQuantityFreshTotal.ToString("0,0.00") + @"</td>
                    <td>" + ProductionQuantityRejectTotal.ToString("0,0.00") + @"</td>
                    <td>" + ProductionQuantityTotal.ToString("0,0.00") + @"</td>
                </tr></table>";

        lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}