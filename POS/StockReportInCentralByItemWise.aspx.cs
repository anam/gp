using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Inventory_PurchasePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initialLoad();
            loadData();
        }
    }

    private void loadData()
    {
        lblStockDate.Text = DateTime.Today.ToString("dd/MM/yyyy") ;
        //purchase info
        string sql = @"SELECT 
Pos_Product.StyleCode as Style,Pos_Product.BarCode
,Pos_Size.SizeName as Size,Pos_Color.ColorName as Color
,ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text as Product,
cast([Pos_Product].ExtraField1 as Decimal(10,2)) as Stock
,[Pos_Product].[SalePrice] 
      ,(cast([Pos_Product].ExtraField1 as Decimal(10,2)) *[Pos_Product].[SalePrice]) as Amount
      
  FROM [Pos_Product]
  inner join ACC_ChartOfAccountLabel4 on ACC_ChartOfAccountLabel4.ACC_ChartOfAccountLabel4ID=Pos_Product.ProductID
  inner join Inv_QuantityUnit on Inv_QuantityUnit.Inv_QuantityUnitID = Pos_Product.Inv_QuantityUnitID
  inner join Pos_Color on Pos_Color.Pos_ColorID = Pos_Product.Pos_ColorID
  inner join Pos_Size on Pos_Size.Pos_SizeID = Pos_Product.Pos_SizeID
  inner join Pos_ProductType on Pos_ProductType.Pos_ProductTypeID = Pos_Product.Pos_ProductTypeID
  inner join Pos_ProductStatus on Pos_ProductStatus.Pos_ProductStatusID = Pos_Product.ProductStatusID
  inner join Pos_Brand on Pos_Brand.Pos_BrandID = Pos_Product.Pos_BrandID
--where cast([Pos_Product].ExtraField1 as decimal(10,2))>0
order by Pos_Product.StyleCode,Pos_Product.BarCode
";

        DataSet ds = CommonManager.SQLExec(sql);
        gvItem.DataSource = ds.Tables[0];
        gvItem.DataBind();

    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in gvItem.Rows)
        {
            Label lblStock = (Label)gvr.FindControl("lblStock");
            TextBox txtInventory = (TextBox)gvr.FindControl("txtInventory");
            Label lblAdjustment = (Label)gvr.FindControl("lblAdjustment");
            try
            {
                lblAdjustment.Text = (int.Parse(txtInventory.Text) - int.Parse(lblStock.Text)).ToString();
            }
            catch (Exception ex)
            { }
        }
    }
}