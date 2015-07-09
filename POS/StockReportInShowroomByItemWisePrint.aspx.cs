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
        int workStationID = 0;
        try
        {
            workStationID = Int32.Parse(Request.QueryString["WorkStationID"]);
            lblStockDate.Text += "<br/>" + Request.QueryString["WorkStationName"];
        }
        catch (Exception ex)
        {
            workStationID = 0;
        }
        //purchase info
        string sql = @"SELECT 
Pos_Product.StyleCode as Style,Pos_Product.BarCode
,Pos_Size.SizeName as Size,Pos_Color.ColorName as Color
,Pos_Product.ProductName as Product
,Pos_WorkStationStock.[Stock]
,Pos_Product.SalePrice 
      ,Pos_WorkStationStock.[Stock] * Pos_Product.SalePrice as Amount
  FROM [Pos_WorkStationStock]
  inner join Pos_Product on Pos_Product.Pos_ProductID=Pos_WorkStationStock.ProductID
  inner join Pos_Size on Pos_Product.Pos_SizeID=Pos_Size.Pos_SizeID
  inner join Pos_Color on Pos_Product.Pos_ColorID=Pos_Color.Pos_ColorID
 " + (workStationID != 0 ? " where WorkStationID=" + workStationID.ToString() : "") + @"
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