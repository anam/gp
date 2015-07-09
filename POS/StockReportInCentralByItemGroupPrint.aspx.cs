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
        lblStockDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

        //purchase info
        string sql = @"SELECT 
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
--where cast([Pos_Product].ExtraField1 as decimal(10,2))>0
group by ACC_ChartOfAccountLabel4.ChartOfAccountLabel4Text
";

        DataSet ds = CommonManager.SQLExec(sql);
            
        int serialNo = 1;

        string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Product Name</td>
                            <td>Qty</td>
                            <td>Amount</td>
                        </tr>
                            ";

        decimal totalStock = 0;
        decimal totalStockAmount = 0;

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (decimal.Parse(dr["Stock"].ToString()) > 0
                ||
                decimal.Parse(dr["Stock"].ToString()) < 0
                )
            {
                totalStock += decimal.Parse(dr["Stock"].ToString());
                totalStockAmount += decimal.Parse(dr["Amount"].ToString());
                htmlTable += @"<tr  class='itemCss'><td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td><td>" + dr["Product"].ToString()
                    + @"</td><td style='text-align:right;'>" + decimal.Parse(dr["Stock"].ToString()).ToString("0,0")
                    + @"</td><td style='text-align:right;'>" + decimal.Parse(dr["Amount"].ToString()).ToString("0,0.00") + @"</td></tr>";
            }
        }
            htmlTable += @"<tr class='subtotalRow'><td  style='border-left:0px;'></td><td>
                    Total</td><td style='text-align:right;'>" + totalStock.ToString("0,0") 
                    + @"</td><td style='text-align:right;'>" + totalStockAmount.ToString("0,0.00") + @"</td></tr></table>";
            
            lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }


}