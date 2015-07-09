using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class POS_ProductModification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadStatus();
        }
    }

    private void loadStatus()
    {
        ddlStatus.Items.Clear();
        ddlStatus.Items.Add(new ListItem("Select Status", "0"));

        List<Pos_ProductStatus> status = new List<Pos_ProductStatus>();
        status = Pos_ProductStatusManager.GetAllPos_ProductStatuss();
        foreach (Pos_ProductStatus item in status)
        {
            ddlStatus.Items.Add(new ListItem(item.ProductStatusName,item.Pos_ProductStatusID.ToString()));
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string sql = @"
            SELECT [Pos_ProductID]
                  ,[ProductID]
                  ,[ReferenceID]
                  ,[Pos_ProductTypeID]
                  ,[Inv_UtilizationDetailsIDs]
                  ,[ProductStatusID]
                  ,[ProductName]
                  ,[DesignCode]
                  ,[Pos_SizeID]
                  ,[Pos_BrandID]
                  ,[Inv_QuantityUnitID]
                  ,[FabricsCost]
                  ,[AccesoriesCost]
                  ,[Overhead]
                  ,[OthersCost]
                  ,[PurchasePrice]
                  ,[SalePrice]
                  ,[OldSalePrice]
                  ,[Note]
                  ,[BarCode]
                  ,[Pos_ColorID]
                  ,[Pos_FabricsTypeID]
                  ,[StyleCode]
                  ,[Pic1]
                  ,[Pic2]
                  ,[Pic3]
                  ,[VatPercentage]
                  ,[IsVatExclusive]
                  ,[DiscountPercentage]
                  ,[DiscountAmount]
                  ,[FabricsNo]
                  ,[ExtraField1]
                  ,[ExtraField2]
                  ,[ExtraField3]
                  ,[ExtraField4]
                  ,[ExtraField5]
                  ,[ExtraField6]
                  ,[ExtraField7]
                  ,[ExtraField8]
                  ,[ExtraField9]
                  ,[ExtraField10]
                  ,[AddedBy]
                  ,[AddedDate]
                  ,[UpdatedBy]
                  ,[UpdatedDate]
                  ,[RowStatusID]
              FROM [Pos_Product] 
            where BarCode ='"+txtBarCode.Text+@"'
            ";
        DataSet ds = CommonManager.SQLExec(sql);
        ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["ProductStatusID"].ToString();
        txtUnitPrice.Text = ds.Tables[0].Rows[0]["PurchasePrice"].ToString();
        txtCentralStock.Text = ds.Tables[0].Rows[0]["ExtraField1"].ToString();
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string sql = @"
            update  [Pos_Product] set ProductStatusID="+ddlStatus.SelectedValue+@"
            where BarCode ='" + txtBarCode.Text + @"'
            ";
        CommonManager.SQLExec(sql);
    }
    protected void btnPriceUpdate_Click(object sender, EventArgs e)
    {
        string sql = @"
             Declare @BarCode nvarchar(50)
 set @BarCode='" + txtBarCode.Text + @"' 
 Declare @NewPrice decimal(20,6)
  Set @NewPrice="+(decimal.Parse(txtUnitPrice.Text)+decimal.Parse(txtUnitPriceAdjustment.Text))+@"
  
  Declare @ProductID int
  set @ProductID=(select Pos_ProductID FROM [GentleParkHO_Old].[dbo].[Pos_Product]
  where BarCode = @BarCode)
  
  Declare @Qty decimal(20,6)
  set @Qty=(SELECT [Quantity]      
  FROM [GentleParkHO_Old].[dbo].[Pos_Transaction]
  where Pos_ProductID=@ProductID and Pos_ProductTrasactionTypeID=25)
  
  update [GentleParkHO_Old].[dbo].[Pos_Product]
  set PurchasePrice=@NewPrice
  where  BarCode = @BarCode
  
  
  update  [GentleParkHO_Old].[dbo].[ACC_JournalDetail]
  set Credit=(@Qty * @NewPrice)
  where 
  JournalMasterID=
	  (
	  select cast(ExtraField10 as int) FROM [GentleParkHO_Old].[dbo].[Pos_Product]
	  where BarCode = @BarCode
	  )
	and ExtraField1=
		(
			select cast(Pos_ProductID as nvarchar) FROM [GentleParkHO_Old].[dbo].[Pos_Product]
			where BarCode = @BarCode
		) 
	and Debit=0
	
update  [GentleParkHO_Old].[dbo].[ACC_JournalDetail]
  set Debit=(@Qty * @NewPrice)
  where 
  JournalMasterID=
	  (
	  select cast(ExtraField10 as int) FROM [GentleParkHO_Old].[dbo].[Pos_Product]
	  where BarCode = @BarCode
	  )
	and ExtraField1=
		(
			select cast(Pos_ProductID as nvarchar) FROM [GentleParkHO_Old].[dbo].[Pos_Product]
			where BarCode = @BarCode
		) 
	and Credit=0
            ";
        CommonManager.SQLExec(sql);
    }
}