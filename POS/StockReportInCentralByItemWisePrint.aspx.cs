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
            
        int serialNo = 1;

        string htmlTable = @" <table id='itemList_tbl' style='border:1px solid black;width:100%;' cellpadding='0' cellspacing='0'>
                        <tr id='tableHeader'>
                            <td  style='border-left:0px;'>S/L</td>
                            <td>Bar Code</td>
                            <td>Description</td>
                            <td>Color</td>
                            <td>Size</td>
                            <td>Quantity</td>
                            <td>Unit Price</td>
                            <td>Amount</td>
                        </tr>
                            ";

        decimal subtotalQty = 0;
        decimal subtotalUnitPrice = 0;
        decimal subtotalStockAmount = 0;

        decimal totalQty = 0;
        decimal totalUnitPrice = 0;
        decimal totalStockAmount = 0;

        decimal GrandtotalQty = 0;
        decimal GrandtotalUnitPrice = 0;
        decimal GrandtotalStockAmount = 0;
        
        string lastProductName = "";
        string lastStyle = "";

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            if (decimal.Parse(dr["Stock"].ToString()) > 0
                ||
                decimal.Parse(dr["Stock"].ToString()) < 0
                )
            {
                if (lastProductName == "")
                {
                    htmlTable += @" <tr id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='7' style='text-align:left;'>Item Name :" + dr["Product"].ToString() + @"</td>
                        </tr>
                            ";
                    lastProductName = dr["Product"].ToString();

                    htmlTable += @" <tr id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='7' style='text-align:left;'>Style :" + dr["Style"].ToString() + @"</td>
                        </tr>
                            ";
                    lastStyle = dr["Style"].ToString();
                }
                else
                    if (lastProductName != dr["Product"].ToString())
                    {
                        htmlTable += @" <tr class='subtotalRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Total</td>
                            <td>" + totalQty.ToString("0,0") + @"</td>
                            <td>" + totalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + totalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                             <tr class='subtotalRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Sub Total</td>
                            <td>" + subtotalQty.ToString("0,0") + @"</td>
                            <td>" + subtotalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + subtotalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                            ";

                        htmlTable += @" <tr  id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='7' style='text-align:left;'>Item Name :" + dr["Product"].ToString() + @"</td>
                        </tr>
                            ";
                        htmlTable += @" <tr  id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='7' style='text-align:left;'>Style :" + dr["Style"].ToString() + @"</td>
                        </tr>
                            ";

                        lastProductName = dr["Product"].ToString();
                        lastStyle = dr["Style"].ToString();

                        subtotalQty = 0;
                        subtotalUnitPrice = 0;
                        subtotalStockAmount = 0;

                        totalQty = 0;
                        totalUnitPrice = 0;
                        totalStockAmount = 0;
                    }
                    else
                        if (lastStyle != dr["Style"].ToString())
                        {
                            htmlTable += @" <tr class='subtotalRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Total</td>
                            <td>" + totalQty.ToString("0,0") + @"</td>
                            <td>" + totalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + totalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                            ";

                            htmlTable += @" <tr id='tableHeader'>
                            <td  style='border-left:0px;'></td>
                            <td colspan='7' style='text-align:left;'>Style :" + dr["Style"].ToString() + @"</td>
                        </tr>
                            ";

                            lastStyle = dr["Style"].ToString();

                            totalQty = 0;
                            totalUnitPrice = 0;
                            totalStockAmount = 0;
                        }

                totalUnitPrice += decimal.Parse(dr["SalePrice"].ToString());
                totalQty += decimal.Parse(dr["Stock"].ToString());
                totalStockAmount += decimal.Parse(dr["Amount"].ToString());

                subtotalUnitPrice += decimal.Parse(dr["SalePrice"].ToString());
                subtotalQty += decimal.Parse(dr["Stock"].ToString());
                subtotalStockAmount += decimal.Parse(dr["Amount"].ToString());

                GrandtotalUnitPrice += decimal.Parse(dr["SalePrice"].ToString());
                GrandtotalQty += decimal.Parse(dr["Stock"].ToString());
                GrandtotalStockAmount += decimal.Parse(dr["Amount"].ToString());



                htmlTable += @" <tr class='itemCss'>
                            <td  style='border-left:0px;'>" + (serialNo++).ToString() + @"</td>
                            <td>" + dr["BarCode"].ToString() + @"</td>
                            <td>" + dr["Product"].ToString() + @"</td>
                            <td>" + dr["Color"].ToString() + @"</td>
                            <td>" + dr["Size"].ToString() + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Stock"].ToString()).ToString("0,0") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["SalePrice"].ToString()).ToString("0,0.00") + @"</td>
                            <td style='text-align:right;'>" + decimal.Parse(dr["Amount"].ToString()).ToString("0,0.00") + @"</td>
                        </tr>
                            ";
            }
        }

        htmlTable += @" <tr id='lastRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Total</td>
                            <td>" + totalQty.ToString("0,0") + @"</td>
                            <td>" + totalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + totalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                             <tr id='lastRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Sub Total</td>
                            <td>" + subtotalQty.ToString("0,0") + @"</td>
                            <td>" + subtotalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + subtotalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                            <tr id='lastRow'>
                            <td  style='border-left:0px;'></td>
                            <td style='text-align:right;' colspan='4'>Grand Total</td>
                            <td>" + GrandtotalQty.ToString("0,0") + @"</td>
                            <td>" + GrandtotalUnitPrice.ToString("0,0.00") + @"</td>
                            <td>" + GrandtotalStockAmount.ToString("0,0.00") + @"</td>
                        </tr>
                           </table>  ";

        lblItemList.Text = htmlTable;
    }

    private void initialLoad()
    {
        lblPrintDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
    }


}