Declare @ProductName nvarchar(256)
Declare @ProductID int
Declare @Pos_ProductID int
Declare @BarCode nvarchar(256)
Declare @ExtraField1 nvarchar(256)

DECLARE product_cursor CURSOR FOR 
    SELECT Pos_ProductID,ProductName,BarCode,ExtraField1
    FROM Pos_Product

    OPEN product_cursor
    FETCH NEXT FROM product_cursor INTO @Pos_ProductID,@ProductName,@BarCode,@ExtraField1
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
		--Add Transaction
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
        
        --update ProductID
        Set @ProductID = (select top 1 ACC_ChartOfAccountLabel4ID from ACC_ChartOfAccountLabel4 where ChartOfAccountLabel4Text=@ProductName and ACC_HeadTypeID=3)
			update Pos_Product set ProductID=@ProductID where Pos_ProductID =@Pos_ProductID
		
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
           ,27
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
        
        FETCH NEXT FROM product_cursor INTO @Pos_ProductID,@ProductName,@BarCode,@ExtraField1
        END

    CLOSE product_cursor
    DEALLOCATE product_cursor