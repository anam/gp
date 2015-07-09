SELECT     TOP (200) Inv_ItemID, ItemName, PurchaseID, ItemCode, RawMaterialID, StoreID, QualityUnitID, QualityValue, QuantityUnitID, PricePerUnit, PurchasedQuantity, 
                      IssueReturedQuantity, UtilizedQuantity, LostQuantity, ExtraFieldQuantity1, ExtraFieldQuantity2, ExtraFieldQuantity3, ExtraFieldQuantity4, ExtraFieldQuantity5, 
                      ExtraField1, ExtraField2, ExtraField3, ExtraField4, ExtraField5, ExtraField6, ExtraField7, ExtraField8, ExtraField9, ExtraField10, AddedBy, AddedDate, UpdatedBy, 
                      UpdatedDate, RowStatusID
FROM         Inv_Item
WHERE     (ItemCode = '02011000210')



SELECT     TOP (200) ACC_JournalDetailID, JournalMasterID, ACC_ChartOfAccountLabel4ID, ACC_ChartOfAccountLabel3ID, WorkStation, Debit, Credit, ExtraField3, ExtraField2, 
                      ExtraField1, AddedBy, AddedDate, UpdatedBy, UpdatedDate, RowStatusID
FROM         ACC_JournalDetail
WHERE     (JournalMasterID = 25349) AND (ACC_ChartOfAccountLabel4ID = 248) AND (ExtraField1 = '8694') OR
                    (JournalMasterID = 25349) AND (Credit = 34845.648950)
                      
                      
SELECT     TOP (200) Inv_ItemTransactionID, ItemID, Quantity, ItemTrasactionTypeID, ReferenceID, ExtraField1, ExtraField2, ExtraField3, ExtraField4, ExtraField5, AddedBy, 
                      AddedDate, UpdatedBy, UpdatedDate, RowStatusID
FROM         Inv_ItemTransaction
WHERE     (ItemID = 8694)


SELECT     TOP (200) Inv_IssueDetailID, ItemID, Quantity, ApproximateQuantity, ProductID, AdditionalWithIssueDetailID, ExtraField1, ExtraField2, ExtraField3, ExtraField4, 
                      ExtraField5, AddedBy, AddedDate, UpdatedBy, UpdatedDate, RowStatusID
FROM         Inv_IssueDetail
WHERE     (ItemID = 8694)