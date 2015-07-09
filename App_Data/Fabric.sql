INSERT INTO [GentlePark].[dbo].[ACC_ChartOfAccountLabel4]
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
     Select ''
           ,2
           ,ItameName
           ,[ItemID]
           ,[ItemcategoryID]
           ,''
           ,39
           ,'2013-02-02 19:25:42.297'
           ,39
           ,'2013-02-02 19:25:42.297'
           ,1
           FROM [StoreInventory].[dbo].[Sis_Item]


