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
set @WorkStationID=34
Declare @Pos_TransactionMasterID int
Set @Pos_TransactionMasterID=88

DECLARE product_cursor CURSOR FOR 
    SELECT ProductName,Barcode,Qty,price,Style,Size
    FROM Table_1
    where cast(Qty as int) >0

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
			if(select Count(Pos_SizeID) from Pos_SizeID where SizeName =@Size) =0
			BEGIN
				set @Pos_SizeID= 46
			END
			ELSE
			BEGIN
				set @Pos_SizeID =(select top 1 Pos_SizeID from Pos_SizeID where SizeName =@Size)
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
           ,80
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
            END;

        OPEN product_cursor
    FETCH NEXT FROM product_cursor INTO @ProductName,@BarCode,@ExtraField1,@Price,@Style,@Size
END
    CLOSE product_cursor
    DEALLOCATE product_cursor