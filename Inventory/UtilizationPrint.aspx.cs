using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                            <td>Apx Qty</td>
                            <td>Prod Qty</td>
                        </tr>";
        int lastRawMaterialID = 0;
        decimal Total = 0;
        decimal Subtotal = 0;
        decimal TotalAmount = 0;
        decimal SubtotalAmount = 0;

        decimal approximateQuantityTotal = 0;
        decimal approximateQuantitySubTotla = 0;
        decimal ProductionQuantityTotal = 0;
        decimal ProductionQuantitySubTotla = 0;

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
                            <td>" + approximateQuantitySubTotla.ToString("0,0.00") + @"</td>
                            <td>" + ProductionQuantitySubTotla.ToString("0,0.00") + @"</td>
                        </tr>";

                Subtotal = 0;
                SubtotalAmount = 0;
                approximateQuantitySubTotla = 0;
                ProductionQuantitySubTotla = 0;
            }

            if (lastRawMaterialID != item.RawMaterialID)
            {
                lastRawMaterialID = item.RawMaterialID;
                htmlTable += @"<tr>
                            <td colspan='9' style='padding-left:50px; border-top:1px solid black;border-bottom:1px solid black;font-weight:bold;'>
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
                            <td style='text-align:right;'>" + decimal.Parse(item.ExtraField4).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(item.ExtraField3).ToString("0,0.00") + @"</td>
                        </tr>";

            Subtotal += item.PurchasedQuantity;
            SubtotalAmount += item.PurchasedQuantityPrice;

            Total += item.PurchasedQuantity;
            TotalAmount += item.PurchasedQuantityPrice;



            approximateQuantitySubTotla += decimal.Parse(item.ExtraField4);
            ProductionQuantitySubTotla += decimal.Parse(item.ExtraField3);

            approximateQuantityTotal += decimal.Parse(item.ExtraField4);
            ProductionQuantityTotal += decimal.Parse(item.ExtraField3);
        }

        htmlTable += @"<tr class='subtotalRow'>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Sub Total</td>
                        <td>" + Subtotal.ToString("0,0.00") + @"</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>" + SubtotalAmount.ToString("0,0.00") + @"</td>
                            <td>" + approximateQuantitySubTotla.ToString("0,0.00") + @"</td>
                            <td>" + ProductionQuantitySubTotla.ToString("0,0.00") + @"</td>
                    </tr>";

        htmlTable += @"<tr id='lastRow'>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Grand Total</td>
                        <td>" + Total.ToString("0,0.00") + @"</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>" + TotalAmount.ToString("0,0.00") + @"</td>
                        <td>" + approximateQuantityTotal.ToString("0,0.00") + @"</td>
                        <td>" + ProductionQuantityTotal.ToString("0,0.00") + @"</td>
                    </tr></table>";

        lblItemList.Text = htmlTable;
        
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }
}