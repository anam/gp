 Declare @NewPrice decimal(18,2)
  Set @NewPrice=337.51
  
  Declare @ProductID int
  set @ProductID=(select Pos_ProductID FROM [GentleParkHO_Old].[dbo].[Pos_Product]
  where BarCode='010670000000')
  
  Declare @Qty decimal(10,2)
  set @Qty=(SELECT [Quantity]      
  FROM [GentleParkHO_Old].[dbo].[Pos_Transaction]
  where Pos_ProductID=@ProductID and Pos_ProductTrasactionTypeID=25)
  
  update [GentleParkHO_Old].[dbo].[Pos_Product]
  set PurchasePrice=@NewPrice
  where  BarCode='010670000000'
  
  
  update  [GentleParkHO_Old].[dbo].[ACC_JournalDetail]
  set Credit=(@Qty * @NewPrice)
  where 
  JournalMasterID=
	  (
	  select cast(ExtraField10 as int) FROM [GentleParkHO_Old].[dbo].[Pos_Product]
	  where BarCode='010670000000'
	  )
	and ExtraField1=
		(
			select cast(Pos_ProductID as nvarchar) FROM [GentleParkHO_Old].[dbo].[Pos_Product]
			where BarCode='010670000000'
		) 
	and Debit=0
	
update  [GentleParkHO_Old].[dbo].[ACC_JournalDetail]
  set Debit=(@Qty * @NewPrice)
  where 
  JournalMasterID=
	  (
	  select cast(ExtraField10 as int) FROM [GentleParkHO_Old].[dbo].[Pos_Product]
	  where BarCode='010670000000'
	  )
	and ExtraField1=
		(
			select cast(Pos_ProductID as nvarchar) FROM [GentleParkHO_Old].[dbo].[Pos_Product]
			where BarCode='010670000000'
		) 
	and Credit=0