/*
Delete Pos_Product where Pos_ProductID >=672 and Pos_ProductID<=678
ALTER TABLE Pos_Product
ALTER COLUMN PurchasePrice decimal(20,6)

Alter Table ACC_JournalDetail
ALTER COLUMN Debit decimal(20,6)

Alter Table ACC_JournalDetail
ALTER COLUMN Credit decimal(20,6)

*/
Declare @BarCode nvarchar(50)
Declare @OldPrice decimal(20,6)

DECLARE product_cursor CURSOR FOR 
    Select Pos_Product.BarCode,Pos_Product.PurchasePrice from Pos_Product
	inner join Pos_Transaction on Pos_Transaction.Pos_ProductID=Pos_Product.Pos_ProductID
	inner join Pos_TransactionMaster on Pos_Transaction.Pos_ProductTransactionMasterID=Pos_TransactionMaster.Pos_TransactionMasterID
	where Pos_TransactionMaster.TransactionID=16
	and Pos_TransactionMaster.Pos_TransactionTypeID=25

    OPEN product_cursor
    FETCH NEXT FROM product_cursor INTO @BarCode,@OldPrice

    IF @@FETCH_STATUS <> 0 
        PRINT '         <<None>>'     

    WHILE @@FETCH_STATUS = 0
    BEGIN

         Declare @NewPrice decimal(20,6)
		  Set @NewPrice=@OldPrice+356.053
		  
		  Declare @ProductID int
		  set @ProductID=(select top 1 Pos_ProductID FROM [Pos_Product]
		  where BarCode = @BarCode)
		  
		  Declare @Qty decimal(20,6)
		  set @Qty=(SELECT  top 1 [Quantity]      
		  FROM [Pos_Transaction]
		  where Pos_ProductID=@ProductID and Pos_ProductTrasactionTypeID=25)
		  
		  update [Pos_Product]
		  set PurchasePrice=@NewPrice
		  where  BarCode = @BarCode
		  
		  
		  update  [ACC_JournalDetail]
		  set Credit=(@Qty * @NewPrice)
		  where 
		  JournalMasterID=
			  (
			  select cast(ExtraField10 as int) FROM [Pos_Product]
			  where BarCode = @BarCode
			  )
			and ExtraField1=
				(
					select cast(Pos_ProductID as nvarchar) FROM [Pos_Product]
					where BarCode = @BarCode
				) 
			and Debit=0
			
		update  [ACC_JournalDetail]
		  set Debit=(@Qty * @NewPrice)
		  where 
		  JournalMasterID=
			  (
			  select cast(ExtraField10 as int) FROM [Pos_Product]
			  where BarCode = @BarCode
			  )
			and ExtraField1=
				(
					select cast(Pos_ProductID as nvarchar) FROM [Pos_Product]
					where BarCode = @BarCode
				) 
			and Credit=0
        
        FETCH NEXT FROM product_cursor INTO @BarCode,@OldPrice
		END

    CLOSE product_cursor
    DEALLOCATE product_cursor
    
 
Select Pos_Product.BarCode,Pos_Product.PurchasePrice from Pos_Product
	inner join Pos_Transaction on Pos_Transaction.Pos_ProductID=Pos_Product.Pos_ProductID
	inner join Pos_TransactionMaster on Pos_Transaction.Pos_ProductTransactionMasterID=Pos_TransactionMaster.Pos_TransactionMasterID
	where Pos_TransactionMaster.TransactionID=16
	and Pos_TransactionMaster.Pos_TransactionTypeID=25