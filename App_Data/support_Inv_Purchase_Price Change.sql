Declare @Inv_ItemID int
Declare @BarCode nvarchar(50)
Declare @OldPrice decimal(20,6)
Declare @Quantity decimal(10,2)

Declare @IncreasedPrice decimal(20,6)

Declare @PurchaseID int
set @PurchaseID=2144

set @IncreasedPrice=2.955


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
