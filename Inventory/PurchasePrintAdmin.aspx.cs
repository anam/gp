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
        int PurchaseID =int.Parse(Request.QueryString["PurchaseID"]);

        //purchase info
        Inv_Purchase purchase = Inv_PurchaseManager.GetInv_PurchaseByID(PurchaseID);
        lblPurchaseDate.Text = purchase.PurchseDate.ToString("dd-MMM-yyyy");
        lblPurchaseID.Text = "<a href='PurchaseModification.aspx?PurchaseID=" + purchase.Inv_PurchaseID.ToString() + "' target='_blank'>" + purchase.Inv_PurchaseID.ToString()+"</a>" + (purchase.RowStatusID == 3 ? "<b style='color:red;'>(Deleted)</b>" : "");
        lblPurchaseID.Text += "&nbsp;&nbsp;&nbsp;<a href='StockReportInCentralItemWisePrint.aspx?PurchaseID=" + purchase.Inv_PurchaseID.ToString() + "' target='_blank'>Check Current Stock</a>";
        lblInvoiceNo.Text = purchase.InvoiceNo;
        lblSupplierName.Text = purchase.SupplierName;
        lblParticulars.Text = purchase.Particulars;

        //Item Info
        List<Inv_Item> items = new List<Inv_Item>();
        items = Inv_ItemManager.GetAllInv_ItemsByPurchaseID(PurchaseID);

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
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + (item.AddedDate <= DateTime.Parse("19 Apr 2013") ? "" : ("&nbsp;<a href='PurchaseDelete.aspx?ItemID=" + item.Inv_ItemID.ToString() + "&PurchaseID=" + item.PurchaseID.ToString() + "' style='color:red;'>X</a>")) + @"</td>
                            <td>" +item.ItemCode+@"</td>
                            <td>"+item.ItemName+@"</td>
                            <td style='text-align:right;'>"+item.PurchasedQuantity.ToString("0,0.00")+@"</td>
                            <td >" + item.QuantityUnitName + @"</td>
                            <td style='text-align:right;'>" + item.PricePerUnit.ToString("0,0.000000") + @"</td>
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
    protected void btnAdjustPriceAll_Click(object sender, EventArgs e)
    {
        string sql = @"
Declare @Inv_ItemID int
Declare @BarCode nvarchar(50)
Declare @OldPrice decimal(20,6)
Declare @Quantity decimal(10,2)

Declare @IncreasedPrice decimal(20,6)

Declare @PurchaseID int
set @PurchaseID=" + Request.QueryString["PurchaseID"] + @"

set @IncreasedPrice="+txtAmount.Text+@"


DECLARE inventoryPurchase_cursor CURSOR FOR 
    SELECT  Inv_Item.Inv_ItemID, Inv_Item.ItemCode, 
		 Inv_ItemTransaction.Quantity, Inv_Item.PricePerUnit
		FROM  Inv_Item INNER JOIN
			Inv_ItemTransaction ON Inv_Item.Inv_ItemID = Inv_ItemTransaction.ItemID INNER JOIN
			Inv_ItemTransactionType ON Inv_ItemTransaction.ItemTrasactionTypeID = Inv_ItemTransactionType.Inv_ItemTransactionTypeID INNER JOIN
			Inv_Purchase ON Inv_ItemTransaction.ReferenceID = Inv_Purchase.Inv_PurchaseID
		where Inv_Purchase.Inv_PurchaseID=@PurchaseID and Inv_ItemTransactionType.Inv_ItemTransactionTypeID=8
    
    OPEN inventoryPurchase_cursor
    FETCH NEXT FROM inventoryPurchase_cursor INTO @Inv_ItemID,@BarCode,@Quantity,@OldPrice

    IF @@FETCH_STATUS <> 0 
        PRINT '         <<None>>'     

    WHILE @@FETCH_STATUS = 0
    BEGIN

		Declare @NewPrice decimal(20,6)
		  Set @NewPrice=@OldPrice+@IncreasedPrice
		  
		  update Inv_Item
		  set PricePerUnit=@NewPrice
		  where  Inv_ItemID = @Inv_ItemID
		  	
		  Declare @JournalMasterID int
		  Set @JournalMasterID =
		  (
		  SELECT [ACC_JournalMasterID]
			FROM [GentleParkHO_Old].[dbo].[ACC_JournalMaster]
		  where Note =('Inventory Purchase-' + CAST (@PurchaseID as nvarchar)) and RowStatusID=1
		  )		  
		  
		  update  [ACC_JournalDetail]
		  set Credit=(@Quantity * @NewPrice)
		  where 
		  JournalMasterID=@JournalMasterID
			and ExtraField1=(CAST (@Inv_ItemID as nvarchar))
			and Debit=0
			
		update  [ACC_JournalDetail]
		  set Debit=(@Quantity * @NewPrice)
		  where 
		  JournalMasterID=@JournalMasterID
			and ExtraField1=(CAST (@Inv_ItemID as nvarchar))
			and Credit=0
        
        FETCH NEXT FROM inventoryPurchase_cursor INTO @Inv_ItemID,@BarCode,@Quantity,@OldPrice
		END

    CLOSE inventoryPurchase_cursor
    DEALLOCATE inventoryPurchase_cursor

";

        CommonManager.SQLExec(sql);
        loadData();
    }
}