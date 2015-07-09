use GentleParkHO_Old

Declare @Old_PurchaseID as int
set @Old_PurchaseID=2543

Declare @Exsiting_PurchaseID as int
set @Exsiting_PurchaseID=2535

Declare @Exsiting_JournalMasetID as int
set @Exsiting_JournalMasetID=
(select top 1 CAST(ExtraField2 as int) 
	from Inv_Purchase where Inv_PurchaseID = @Exsiting_PurchaseID)
	
	
Declare @Old_JournalMasetID as int
set @Old_JournalMasetID=
(select top 1 CAST(ExtraField2 as int) 
	from Inv_Purchase where Inv_PurchaseID = @Old_PurchaseID)

update Inv_ItemTransaction set ReferenceID=@Exsiting_PurchaseID
where ItemID in (select Inv_ItemID from Inv_Item 
where PurchaseID=@Old_PurchaseID) and ItemTrasactionTypeID=8

update Inv_Item set PurchaseID=@Exsiting_PurchaseID
where PurchaseID=@Old_PurchaseID

update ACC_JournalDetail set JournalMasterID=@Exsiting_JournalMasetID
where JournalMasterID=@Old_JournalMasetID

update ACC_JournalMaster set RowStatusID=3 , Note+=' Item transfered to Inv. Pur.'+ CAST(@Old_PurchaseID as nvarchar)
where ACC_JournalMasterID=@Old_JournalMasetID

update Inv_Purchase set RowStatusID=3 , Particulars='Item transfered to '+ CAST(@Old_PurchaseID as nvarchar)
where Inv_PurchaseID= @Old_PurchaseID




