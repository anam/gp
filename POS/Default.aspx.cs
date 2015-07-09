using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class POS_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.Redirect("../Visitor/Default.aspx");
        //rbtnReport_SelectedIndexChanged(this, new EventArgs());
    }
    protected void rbtnReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sql = "";

        if (rbtnReport.SelectedValue == "1")
        { 
        sql = @"SELECT 
ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as Product,
SUM(Pos_WorkStationStock.[Stock]) as Stock
      ,SUM(Pos_WorkStationStock.[Stock] * Pos_Product.SalePrice) as Amount
      
  FROM [Pos_WorkStationStock]
  inner join Pos_Product on Pos_Product.Pos_ProductID=Pos_WorkStationStock.ProductID
  inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID
  =Pos_Product.ProductID
  where WorkStationID=3
  group by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text";
        }
        else
            if (rbtnReport.SelectedValue == "2")
            {
                sql = @"SELECT Pos_Product.StyleCode as [Style]
,Pos_Product.BarCode as [Product Code]
,Pos_Product.ProductName as [Description]
,Pos_Size.SizeName as Size
,Pos_Color.ColorName as Color
,Pos_WorkStationStock.[Stock] as [Closing Stock]
,Pos_Product.SalePrice as [Unit Price]
      ,Pos_WorkStationStock.[Stock] * Pos_Product.SalePrice as Amount
  FROM [Pos_WorkStationStock]
  inner join Pos_Product on Pos_Product.Pos_ProductID=Pos_WorkStationStock.ProductID
  inner join Pos_Size on Pos_Product.Pos_SizeID=Pos_Size.Pos_SizeID
  inner join Pos_Color on Pos_Product.Pos_ColorID=Pos_Color.Pos_ColorID
  where WorkStationID=3";
            }
            else
                if (rbtnReport.SelectedValue == "3")
                {
                    sql = @"SELECT 
ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as Product,
Sum(cast([Pos_Product].ExtraField1 as Decimal(10,2))) as Stock
      ,SUM((cast([Pos_Product].ExtraField1 as Decimal(10,2)) *[Pos_Product].[SalePrice])) as Amount
      
  FROM [Pos_Product]
  inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=Pos_Product.ProductID
  inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID = Pos_Product.Inv_QuantityUnitID
  inner join Pos_Color on Pos_Color.Pos_ColorID = Pos_Product.Pos_ColorID
  inner join Pos_Size on Pos_Size.Pos_SizeID = Pos_Product.Pos_SizeID
  inner join Pos_ProductType on Pos_ProductType.Pos_ProductTypeID = Pos_Product.Pos_ProductTypeID
  inner join Pos_ProductStatus on Pos_ProductStatus.Pos_ProductStatusID = Pos_Product.ProductStatusID
  inner join Pos_Brand on Pos_Brand.Pos_BrandID = Pos_Product.Pos_BrandID
where cast([Pos_Product].ExtraField1 as decimal(10,2))>0
group by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text";
                }
        else
                    if (rbtnReport.SelectedValue == "4")
                    {
                        sql = @"SELECT 
Pos_Product.StyleCode as Style,Pos_Product.BarCode as [Product Code]
,Pos_Size.SizeName as Size,Pos_Color.ColorName as Colora
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as Product,
cast([Pos_Product].ExtraField1 as Decimal(10,2)) as Stock
,[Pos_Product].[SalePrice] as [Unit Price]
      ,(cast([Pos_Product].ExtraField1 as Decimal(10,2)) *[Pos_Product].[SalePrice]) as Amount
      
  FROM [Pos_Product]
  inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=Pos_Product.ProductID
  inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID = Pos_Product.Inv_QuantityUnitID
  inner join Pos_Color on Pos_Color.Pos_ColorID = Pos_Product.Pos_ColorID
  inner join Pos_Size on Pos_Size.Pos_SizeID = Pos_Product.Pos_SizeID
  inner join Pos_ProductType on Pos_ProductType.Pos_ProductTypeID = Pos_Product.Pos_ProductTypeID
  inner join Pos_ProductStatus on Pos_ProductStatus.Pos_ProductStatusID = Pos_Product.ProductStatusID
  inner join Pos_Brand on Pos_Brand.Pos_BrandID = Pos_Product.Pos_BrandID
where cast([Pos_Product].ExtraField1 as decimal(10,2))>0
order by Pos_Product.StyleCode,Pos_Product.BarCode

";
                    }

        DataSet ds = CommonManager.SQLExec(sql);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
    }
}