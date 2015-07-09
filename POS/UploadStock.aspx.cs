using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Text;

public partial class POS_UploadStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            loadACC_ChartOfAccountLabel4();
        }
    }

    private void loadACC_ChartOfAccountLabel4()
    {

        List<ACC_ChartOfAccountLabel4> aCC_ChartOfAccountLabel4s = new List<ACC_ChartOfAccountLabel4>();
        aCC_ChartOfAccountLabel4s = ACC_ChartOfAccountLabel4Manager.GetAllACC_ChartOfAccountLabel4sForJournalEntry();

        ddlShowRoom.Items.Add(new ListItem("All Show Room", "0"));

        foreach (ACC_ChartOfAccountLabel4 aCC_ChartOfAccountLabel4 in aCC_ChartOfAccountLabel4s)
        {
            ListItem item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_HeadTypeID.ToString() + "@" + aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
        
            if ((aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID == 1 || aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToLower().Contains("show room")) &&
                aCC_ChartOfAccountLabel4.ACC_HeadTypeID == 1)
            {
                item = new ListItem(aCC_ChartOfAccountLabel4.ChartOfAccountLabel4Text.ToString(), aCC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID.ToString());
                ddlShowRoom.Items.Add(item);
            }
        }
    }

    private string uplaodFile()
    {
        if (fuldStock.HasFile)
        {
            #region fileUpload
            string strPath = string.Empty;
            string dirURL = "~/files/file/Stock/";
            if (!Directory.Exists(MapPath(dirURL)))
                Directory.CreateDirectory(MapPath(dirURL));

            getFileName fileName = new getFileName(fuldStock.FileName, MapPath(dirURL));
            strPath = MapPath(dirURL + "/" + fileName.FileName);
            fuldStock.SaveAs(strPath);
            #endregion

            return  fileName.FileName;
        }
        return "";
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {

        string dirURL = "~/files/file/Stock/";
        

        string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + MapPath(dirURL) + uplaodFile() + "';Extended Properties='Excel 12.0;HDR=Yes';";
//        string conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='G:/Anam/Projects/Mavrick/gentlepark/Offline Version/Code/HO/V5/POS/Stock" + uplaodFile() + "';Extended Properties='Excel 12.0;HDR=Yes';";
        OleDbConnection objConn = new OleDbConnection(conn);
        objConn.Open();
        OleDbCommand objCmdSelect = new OleDbCommand("SELECT * FROM [Sheet1$]", objConn);
        OleDbDataAdapter objAdapter = new OleDbDataAdapter();
        objAdapter.SelectCommand = objCmdSelect;
        DataSet objDataset = new DataSet();
        objAdapter.Fill(objDataset);
        objConn.Close();

        GridView1.DataSource = objDataset.Tables[0];
        GridView1.DataBind();
        
        string sql = @"
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Table_1]') AND type in (N'U'))
DROP TABLE [dbo].[Table_1]
CREATE TABLE [dbo].[Table_1](
	[ProductName] [nvarchar](256) NULL,
	[Barcode] [nvarchar](50) NULL,
	[Style] [nvarchar](50) NULL,
	[Size] [nvarchar](50) NULL,
	[Qty] [decimal](18, 2) NULL,
	[Price] [decimal](18, 2) NULL
)
delete Table_1;
        ";
        string ex = "";
      decimal   total = 0;
        CommonManager.SQLExec(sql);
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            try
            {
                if (int.Parse(gvr.Cells[4].Text) > 0)
                {
                    sql = @"
                            INSERT INTO [GentlePark].[dbo].[Table_1]
                                   ([ProductName]
                                   ,[Barcode]
                                   ,[Style]
                                   ,[Size]
                                   ,[Qty]
                                   ,[Price])
                             VALUES
                                   ('" + gvr.Cells[1].Text + @"'--<ProductName, nvarchar(256),>
                                   ,'" + gvr.Cells[0].Text + @"'--<Barcode, nvarchar(50),>
                                   ,'" + gvr.Cells[2].Text + @"'--<Style, nvarchar(50),>
                                   ,'" + gvr.Cells[3].Text + @"'--<Size, nvarchar(50),>
                                   ," + gvr.Cells[4].Text + @"--<Qry, decimal(18,2),>
                                   ," + decimal.Parse(gvr.Cells[5].Text).ToString("0.00") + @"--<price, decimal(18,2),>
                        ); ";
                    total += (decimal.Parse(gvr.Cells[4].Text) * decimal.Parse(gvr.Cells[5].Text));

                    CommonManager.SQLExec(sql);
                }
            }
            catch (Exception exep)
            {
                ex += gvr.Cells[0].Text+",";
            }
        }
        #region Brnach
        try
        {
            sql = "/*" + ex + total + @"*/
USE GentlePark

Declare @ProductName nvarchar(256)
Declare @ProductID int
Declare @Pos_ProductID int
Declare @BarCode nvarchar(256)
Declare @Style nvarchar(256)
Declare @Size nvarchar(256)
Declare @Pos_SizeID int
Declare @ExtraField1 nvarchar(256)
declare @Price decimal(10,2)
Declare @WorkStationID int
set @WorkStationID=" + ddlShowRoom.SelectedValue + @"
Declare @Pos_TransactionMasterID int
Set @Pos_TransactionMasterID=" + int.Parse(ddlShowRoom.SelectedItem.Text.Split('-')[1]).ToString() + @"

DECLARE product_cursor CURSOR FOR 
    SELECT ProductName,Barcode,Qty,Price,Style,Size
    FROM Table_1
    --where cast(Qty as int) >0

    OPEN product_cursor
    FETCH NEXT FROM product_cursor INTO @ProductName,@BarCode,@ExtraField1,@Price,@Style,@Size
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
    
		Set @ProductID = (Select Count(*) from ACC_ChartOfAccountLabel4 where ChartOfAccountLabel4Text=@ProductName and ACC_HeadTypeID=3)
        if @ProductID = 0
        BEGIN
        	--Create Product Name
			INSERT INTO [ACC_ChartOfAccountLabel4]
           ([Code]
           ,[ACC_HeadTypeID]
           ,[ChartOfAccountLabel4Text]
           ,[ExtraField1]
           ,[ExtraField2]
           ,[ExtraField3]
           ,[AddedBy]
           ,[AddedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[RowStatusID])
     VALUES
           (''
           ,3
           ,@ProductName
           ,SUBSTRING(@BarCode,1,5)
           ,''
           ,'@'
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
           ,1)
        END
        Set @ProductID = (select top 1 ACC_ChartOfAccountLabel4ID from ACC_ChartOfAccountLabel4 where ChartOfAccountLabel4Text=@ProductName and ACC_HeadTypeID=3)
		
		--add Product
		Set @Pos_ProductID = (Select Count(*) from Pos_Product where BarCode=@BarCode)
        if @Pos_ProductID = 0
        BEGIN
			if(select Count(Pos_SizeID) from Pos_Size where SizeName =@Size) =0
			BEGIN
				set @Pos_SizeID= 46
			END
			ELSE
			BEGIN
				set @Pos_SizeID =(select top 1 Pos_SizeID from Pos_Size where SizeName =@Size)
			END
			
        
			INSERT INTO [Pos_Product]
           ([ProductID]
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
           ,[RowStatusID])
     VALUES
           (@ProductID
           ,1
           ,1
           ,''
           ,1
           ,@ProductName
           ,''
           ,@Pos_SizeID
           ,1
           ,3
           ,0
           ,0
           ,0
           ,0
           ,0
           ,@Price
           ,0
           ,'Stock entry-BR'
           ,@BarCode
           ,1
           ,1
           ,@Style
           ,''
           ,''
           ,''
           ,0
           ,1
           ,0
           ,0
           ,''
           ,'0'
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
           ,1);
           
		Set @Pos_ProductID = (Select top 1 Pos_ProductID from Pos_Product where BarCode=@BarCode)
		

           
			
			
		END
		Set @Pos_ProductID = (Select top 1 Pos_ProductID from Pos_Product where BarCode=@BarCode)
		
 --Production Transaction
			INSERT INTO [Pos_Transaction]
           ([Pos_ProductID]
           ,[Quantity]
           ,[Pos_ProductTrasactionTypeID]
           ,[Pos_ProductTransactionMasterID]
           ,[WorkStationID]
           ,[ExtraField1]
           ,[ExtraField2]
           ,[ExtraField3]
           ,[ExtraField4]
           ,[ExtraField5]
           ,[AddedBy]
           ,[AddedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[RowStatusID])
     VALUES
           (@Pos_ProductID
           ,Cast(@ExtraField1 as decimal(10,2))
           ,1
           ,21
           ,1
           ,''
           ,''
           ,''
           ,''
           ,''
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
           ,1)
			--accounts need to update

		--Issue Transaction
			INSERT INTO [Pos_Transaction]
           ([Pos_ProductID]
           ,[Quantity]
           ,[Pos_ProductTrasactionTypeID]
           ,[Pos_ProductTransactionMasterID]
           ,[WorkStationID]
           ,[ExtraField1]
           ,[ExtraField2]
           ,[ExtraField3]
           ,[ExtraField4]
           ,[ExtraField5]
           ,[AddedBy]
           ,[AddedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[RowStatusID])
     VALUES
           (@Pos_ProductID
           ,Cast(@ExtraField1 as decimal(10,2))
           ,9
           ,@Pos_TransactionMasterID
           ,@WorkStationID
           ,''
           ,''
           ,''
           ,''
           ,''
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
           ,1)
		Declare @Count int
		set @Count=
            (
            select COUNT(*) from Pos_WorkStationStock
            where ProductID=@Pos_ProductID and WorkStationID=@WorkStationID
            )

            if @Count = 0
            BEGIN
                INSERT INTO [Pos_WorkStationStock]
                       ([WorkStationID]
                       ,[ProductID]
                       ,[Stock])
                       VALUES(@WorkStationID,@Pos_ProductID ,CAST(@ExtraField1 as Decimal(10,2)));
            END
            ELSE
            BEGIN
                Update Pos_WorkStationStock set Stock += CAST(@ExtraField1 as Decimal(10,2)) where ProductID=@Pos_ProductID and WorkStationID=@WorkStationID;
            END
    
        
        FETCH NEXT FROM product_cursor INTO @ProductName,@BarCode,@ExtraField1,@Price,@Style,@Size
    END
    CLOSE product_cursor
    DEALLOCATE product_cursor
";
        }
        catch (Exception ex3)
        { }
        #endregion

        #region Centeral
        string sql_HeadOffice;
        sql_HeadOffice = "/*" + ex + total + @"*/
USE GentlePark

Declare @ProductName nvarchar(256)
Declare @ProductID int
Declare @Pos_ProductID int
Declare @BarCode nvarchar(256)
Declare @Style nvarchar(256)
Declare @Size nvarchar(256)
Declare @Pos_SizeID int
Declare @ExtraField1 nvarchar(256)
declare @Price decimal(10,2)
Declare @WorkStationID int
set @WorkStationID=1
Declare @Pos_TransactionMasterID int
Set @Pos_TransactionMasterID=20

DECLARE product_cursor CURSOR FOR 
    SELECT ProductName,Barcode,Qty,Price,Style,Size
    FROM Table_1
    --where cast(Qty as int) >0

    OPEN product_cursor
    FETCH NEXT FROM product_cursor INTO @ProductName,@BarCode,@ExtraField1,@Price,@Style,@Size
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
    
		Set @ProductID = (Select Count(*) from ACC_ChartOfAccountLabel4 where ChartOfAccountLabel4Text=@ProductName and ACC_HeadTypeID=3)
        if @ProductID = 0
        BEGIN
        	--Create Product Name
			INSERT INTO [ACC_ChartOfAccountLabel4]
           ([Code]
           ,[ACC_HeadTypeID]
           ,[ChartOfAccountLabel4Text]
           ,[ExtraField1]
           ,[ExtraField2]
           ,[ExtraField3]
           ,[AddedBy]
           ,[AddedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[RowStatusID])
     VALUES
           (''
           ,3
           ,@ProductName
           ,SUBSTRING(@BarCode,1,5)
           ,''
           ,'@'
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
           ,1)
        END
        Set @ProductID = (select top 1 ACC_ChartOfAccountLabel4ID from ACC_ChartOfAccountLabel4 where ChartOfAccountLabel4Text=@ProductName and ACC_HeadTypeID=3)
		
		--add Product
		Set @Pos_ProductID = (Select Count(*) from Pos_Product where BarCode=@BarCode)
        if @Pos_ProductID = 0
        BEGIN
			if(select Count(Pos_SizeID) from Pos_Size where SizeName =@Size) =0
			BEGIN
				set @Pos_SizeID= 46
			END
			ELSE
			BEGIN
				set @Pos_SizeID =(select top 1 Pos_SizeID from Pos_Size where SizeName =@Size)
			END
			
        
			INSERT INTO [Pos_Product]
           ([ProductID]
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
           ,[RowStatusID])
     VALUES
           (@ProductID
           ,1
           ,1
           ,''
           ,1
           ,@ProductName
           ,''
           ,@Pos_SizeID
           ,1
           ,3
           ,0
           ,0
           ,0
           ,0
           ,0
           ,@Price
           ,0
           ,'Stock entry-BR'
           ,@BarCode
           ,1
           ,1
           ,@Style
           ,''
           ,''
           ,''
           ,0
           ,1
           ,0
           ,0
           ,''
           ,@ExtraField1
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,''
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
           ,1);
           
		Set @Pos_ProductID = (Select top 1 Pos_ProductID from Pos_Product where BarCode=@BarCode)
		
            --Transaction
			INSERT INTO [Pos_Transaction]
           ([Pos_ProductID]
           ,[Quantity]
           ,[Pos_ProductTrasactionTypeID]
           ,[Pos_ProductTransactionMasterID]
           ,[WorkStationID]
           ,[ExtraField1]
           ,[ExtraField2]
           ,[ExtraField3]
           ,[ExtraField4]
           ,[ExtraField5]
           ,[AddedBy]
           ,[AddedDate]
           ,[UpdatedBy]
           ,[UpdatedDate]
           ,[RowStatusID])
     VALUES
           (@Pos_ProductID
           ,Cast(@ExtraField1 as decimal(10,2))
           ,1
           ,@Pos_TransactionMasterID
           ,1
           ,''
           ,''
           ,''
           ,''
           ,''
           ,1
           ,GETDATE()
           ,1
           ,GETDATE()
           ,1)
			--accounts need to update
			
			
		END
		Set @Pos_ProductID = (Select top 1 Pos_ProductID from Pos_Product where BarCode=@BarCode)
		
        update Pos_Product set ExtraField1=@ExtraField1 where Pos_ProductID=@Pos_ProductID

        FETCH NEXT FROM product_cursor INTO @ProductName,@BarCode,@ExtraField1,@Price,@Style,@Size
    END
    CLOSE product_cursor
    DEALLOCATE product_cursor
";
        #endregion

        txtCursor.Text = sql;
        CommonManager.SQLExec(sql_HeadOffice);
        //CommonManager.SQLExec(sql);

        

        //DataSet ds = MSSQL.SQLExec(sql + "; select Count(*) from Mem_Fees where Comn_PaymentByID=2");
        //Label1.Text = ds.Tables[0].Rows.Count + " Record(s) Added Successfully";
        //btnSave.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql = @"
SELECT [ProductID] into #tmp
  FROM [GentlePark].[dbo].[Pos_WorkStationStock]
  where ProductID<107
  
Select BarCode,SalePrice,Pos_ProductID 
from Pos_Product where Pos_ProductID 
in (select * from #tmp)  and BarCode in (Select distinct Barcode from Table_1)
drop table #tmp
";
        DataSet ds = CommonManager.SQLExec(sql);
        sql = "";
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            sql += @"
    update Pos_Product set SalePrice=(Select top 1 Price from Table_1 where BarCode='" + dr["BarCode"].ToString().Trim() + @"') where Pos_ProductID=" + dr["Pos_ProductID"].ToString().Trim() + @";
";
        }
        CommonManager.SQLExec(sql);
    }
}