update Table_00 set Price= REPLACE(Price,',',''),TotalPrice= REPLACE(TotalPrice,',','')
update Table_10 set Price= REPLACE(Price,',',''),TotalPrice= REPLACE(TotalPrice,',','')
update Table_20 set Price= REPLACE(Price,',',''),TotalPrice= REPLACE(TotalPrice,',','')
update Table_30 set Price= REPLACE(Price,',',''),TotalPrice= REPLACE(TotalPrice,',','')


insert into Pos_Product
(BarCode,StyleCode,ProductName,Pos_SizeID,ExtraField1,SalePrice)
SELECT [BarCode]
      ,[Style]
      ,[Productname]
      ,cast([Size] as int)
      ,[Qty]
      ,cast([Price] as decimal(18,2))
  FROM [Table_00]
where cast(Qty  as int) >0

UPDATE [Pos_Product]
   SET [ReferenceID] = 1--<ReferenceID, int,>
      ,[Pos_ProductTypeID] = 1--<Pos_ProductTypeID, int,>
      ,[Inv_UtilizationDetailsIDs] = '1'--<Inv_UtilizationDetailsIDs, nvarchar(256),>
      ,[ProductStatusID] = 1--<ProductStatusID, int,>
      ,[DesignCode] = StyleCode--<DesignCode, nvarchar(10),>
      ,[Pos_BrandID] = 1--<Pos_BrandID, int,>
      ,[Inv_QuantityUnitID] = 3--<Inv_QuantityUnitID, int,>
      ,[FabricsCost] = (SalePrice/2)--<FabricsCost, decimal(18,2),>
      ,[AccesoriesCost] = 0--<AccesoriesCost, decimal(18,2),>
      ,[Overhead] = 0--<Overhead, decimal(18,2),>
      ,[OthersCost] = 0--<OthersCost, decimal(18,2),>
      ,[PurchasePrice] = 0--<PurchasePrice, decimal(18,2),>
      ,[OldSalePrice] = 0--<OldSalePrice, decimal(18,2),>
      ,[Note] = 'Stock'--<Note, ntext,>
      ,[Pos_ColorID] = 1--<Pos_ColorID, int,>
      ,[Pos_FabricsTypeID] = 1--<Pos_FabricsTypeID, int,>
      ,[Pic1] = ''--<Pic1, ntext,>
      ,[Pic2] = ''--<Pic2, nvarchar(256),>
      ,[Pic3] = ''--<Pic3, nvarchar(256),>
      ,[VatPercentage] = 0--<VatPercentage, decimal(5,2),>
      ,[IsVatExclusive] = 1--<IsVatExclusive, bit,>
      ,[DiscountPercentage] = 0--<DiscountPercentage, decimal(5,2),>
      ,[DiscountAmount] = 0--<DiscountAmount, decimal(18,2),>
      ,[FabricsNo] = ''--<FabricsNo, nvarchar(50),>
      ,[ExtraField2] = ''--<ExtraField2, nvarchar(256),>
      ,[ExtraField3] = ''--<ExtraField3, nvarchar(256),>
      ,[ExtraField4] = ''--<ExtraField4, nvarchar(256),>
      ,[ExtraField5] = ''--<ExtraField5, nvarchar(256),>
      ,[ExtraField6] = ''--<ExtraField6, nvarchar(256),>
      ,[ExtraField7] = ''--<ExtraField7, nvarchar(256),>
      ,[ExtraField8] = ''--<ExtraField8, nvarchar(256),>
      ,[ExtraField9] = ''--<ExtraField9, nvarchar(256),>
      ,[ExtraField10] = ''--<ExtraField10, nvarchar(256),>
      ,[AddedBy] = 1--<AddedBy, int,>
      ,[AddedDate] = GETDATE()--<AddedDate, datetime,>
      ,[UpdatedBy] = 1--<UpdatedBy, int,>
      ,[UpdatedDate] = GETDATE()--<UpdatedDate, datetime,>
      ,[RowStatusID] = 1--<RowStatusID, int,>







